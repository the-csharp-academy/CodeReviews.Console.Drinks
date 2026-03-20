using DrinksInfo.Application.DrinkInfoApi.GetDrinkDetailsById;
using DrinksInfo.Application.DrinkInfoApi.GetDrinkImage;
using DrinksInfo.Application.DrinkInfoApi.GetDrinksSummaryByCategoryName;
using DrinksInfo.Application.Favorites.AddFavoriteDrink;
using DrinksInfo.Application.Favorites.DeleteFavoriteDrink;
using DrinksInfo.Application.Favorites.ExistsById;
using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Helpers;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Views;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.ConsoleUI.Services;

public class DrinkDetailService
{
    private readonly GetDrinkDetailsByIdHandler _getDrinkDetailsHandler;
    private readonly GetDrinkImageHandler _getDrinkImageHandler;
    private readonly AddFavoriteDrinkHandler _addFavoriteHandler;
    private readonly DeleteFavoriteDrinkByIdHandler _deleteFavoriteHandler;
    private readonly FavoriteExistsByIdHandler _favoriteExists;
    private readonly GetViewCountService _viewCountService;

    private readonly DrinkDetailsView _drinkDetails;
    private readonly DrinkImageView _drinkImage;
    private readonly Messages _output;


    public DrinkDetailService(GetDrinkDetailsByIdHandler getDrinkDetailsHandler, GetDrinkImageHandler getDrinkImageHandler,
                                AddFavoriteDrinkHandler addFavoriteHandler, DeleteFavoriteDrinkByIdHandler deleteFavoriteHandler,
                                DrinkDetailsView drinkDetails, FavoriteExistsByIdHandler favoriteExists, GetViewCountService viewCountService,
                                DrinkImageView drinkImage, Messages output)
    {
        _getDrinkDetailsHandler = getDrinkDetailsHandler;
        _getDrinkImageHandler = getDrinkImageHandler;
        _addFavoriteHandler = addFavoriteHandler;
        _deleteFavoriteHandler = deleteFavoriteHandler;
        _viewCountService = viewCountService;
        _drinkDetails = drinkDetails;
        _favoriteExists = favoriteExists;

        _drinkImage = drinkImage;
        _output = output;
    }

    public async Task<ExitCode> RunAsync(DrinkDetailEntryMode entryMode, DrinkSummaryResponse drinkSelection)
    {
        var exitCode = ExitCode.None;
        bool returnToPreviousMenu = false;
        bool isFavorite = (await _favoriteExists.HandleAsync(drinkSelection.Id)).IsSuccess;

        var viewCountText = GenerateViewCountText(await _viewCountService.RunAsync(drinkSelection.Id));

        while (returnToPreviousMenu == false)
        {
            Console.Clear();
            var drinkDetailResult = await ConsoleStatusHelper.ShowStatusAsync($"Fetching {drinkSelection.Name} details...", () =>
                                            _getDrinkDetailsHandler.HandleAsync(drinkSelection.Id));

            if (drinkDetailResult.IsSuccess && drinkDetailResult.Value is not null)
            {
                _drinkDetails.Render(drinkDetailResult.Value, isFavorite, viewCountText);

                Console.WriteLine();
                Console.WriteLine();

                _output.RenderDrinkDetailKeyOptions(entryMode, isFavorite);

                var keyInfo = Console.ReadKey(true);

                if (entryMode == DrinkDetailEntryMode.Category)
                    (exitCode, isFavorite) = await ManageKeyPressMenuFromCategory(keyInfo, drinkDetailResult.Value, isFavorite);
                else
                    (exitCode, isFavorite) = await ManageKeyPressMenuFromFavorite(keyInfo, drinkDetailResult.Value, isFavorite);
            }
            else
            {
                _output.OutputErrorMessage(drinkDetailResult.Errors);
                break;
            }

            if (exitCode == ExitCode.DrinkSelection || exitCode == ExitCode.CategorySelection ||
                exitCode == ExitCode.MainMenu || exitCode == ExitCode.FavoriteList)
                returnToPreviousMenu = true;
            else
                returnToPreviousMenu = false;
        }

        return exitCode;
    }

    private async Task<(ExitCode, bool)> ManageKeyPressMenuFromCategory(ConsoleKeyInfo keyInfo, DrinkDetailResponse drinkDetailResponse,
                                                                    bool isFavorite)
    {
        ExitCode exitCode = ExitCode.None;
        switch (keyInfo.Key)
        {
            case ConsoleKey.V:
                await ManageViewImage(drinkDetailResponse.ImageUrl);
                break;
            case ConsoleKey.F:
                if (isFavorite == false)
                {
                    await ManageAddFavorite(drinkDetailResponse);
                    isFavorite = true;
                }
                break;
            case ConsoleKey.X:
                if (isFavorite)
                {
                    await ManageDeleteFavorite(drinkDetailResponse);
                    isFavorite = false;
                }
                break;
            case ConsoleKey.D:
                exitCode = ExitCode.DrinkSelection;
                break;
            case ConsoleKey.C:
                exitCode = ExitCode.CategorySelection;
                break;
            case ConsoleKey.M:
                exitCode = ExitCode.MainMenu;
                break;
            default:
                _output.InputErrorMessage();
                exitCode = ExitCode.None;
                break;
        }

        return (exitCode, isFavorite);
    }
    private async Task<(ExitCode, bool)> ManageKeyPressMenuFromFavorite(ConsoleKeyInfo keyInfo, DrinkDetailResponse drinkDetailResponse,
                                                                    bool isFavorite)
    {
        ExitCode exitCode = ExitCode.None;
        switch (keyInfo.Key)
        {
            case ConsoleKey.V:
                await ManageViewImage(drinkDetailResponse.ImageUrl);
                break;
            case ConsoleKey.X:
                if (isFavorite)
                {
                    await ManageDeleteFavorite(drinkDetailResponse);
                    isFavorite = false;
                }
                break;
            case ConsoleKey.L:
                exitCode = ExitCode.FavoriteList;
                break;
            case ConsoleKey.M:
                exitCode = ExitCode.MainMenu;
                break;
            default:
                _output.InputErrorMessage();
                exitCode = ExitCode.None;
                break;
        }

        return (exitCode, isFavorite);
    }
    private async Task ManageViewImage(string url)
    {
        Console.Clear();
        var imageResult = await _getDrinkImageHandler.HandleAsync(url);

        if (imageResult.IsSuccess && imageResult.Value is not null)
        {
            Console.Clear();
            _drinkImage.Render(imageResult.Value);
            _output.PressAnyKeyToContinue();
        }
        else
        {
            _output.OutputErrorMessage(imageResult.Errors);
        }
    }
    private async Task ManageAddFavorite(DrinkDetailResponse drinkDetails)
    {
        var addResult = await _addFavoriteHandler.HandleAsync(new(drinkDetails.Id, drinkDetails.Name, drinkDetails.Category));

        if (addResult.IsSuccess)
            _output.PrintAddFavoriteSuccessMessage(drinkDetails.Name);
        else
            _output.OutputErrorMessage(addResult.Errors);
    }
    private async Task ManageDeleteFavorite(DrinkDetailResponse drinkDetails)
    {
        var deleteResult = await _deleteFavoriteHandler.HandleAsync(drinkDetails.Id);

        if (deleteResult.IsSuccess)
            _output.PrintDeleteFavoriteSuccessMessage(drinkDetails.Name);
        else
            _output.OutputErrorMessage(deleteResult.Errors);
    }

    private string GenerateViewCountText(Result<int> viewCountResult)
    {
        if (viewCountResult.IsSuccess && viewCountResult?.Value is not null)
            return viewCountResult.Value.ToString();

        return "<empty>";
    }
}
