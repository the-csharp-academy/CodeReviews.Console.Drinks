using Dapper;
using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace DrinksInfo.Infrastructure.Repositories;

public class FavoriteDrinkRepository : IFavoriteDrinkRepository
{
    private readonly ISqliteConnectionFactory _connection;
    public FavoriteDrinkRepository(ISqliteConnectionFactory connection)
    {
        _connection = connection;
    }
    public async Task<Result> ExistsByIdAsync(int id)
    {
        string sql = $"Select count(1) from FavoriteDrink where DrinkId = @Id";

        try
        {
            using var connection = _connection.CreateConnection();

            var response = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

            if (response > 0)
                return Result.Success();
            else
                return Result.Failure(Errors.NoRecordById);
        }
        catch (SqliteException ex)
        {
            return Result.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Unknown", ex.Message));
        }
    }
    public async Task<Result<List<FavoriteDrink>>> GetAllAsync()
    {
        string sql = "Select * from FavoriteDrink";

        try
        {
            using var connection = _connection.CreateConnection();

            var response = await connection.QueryAsync<FavoriteDrink>(sql);

            if (!response.Any())
                return Result<List<FavoriteDrink>>.Failure(Errors.EmptyDbResponse);

            return Result<List<FavoriteDrink>>.Success(response.ToList());
        }
        catch (SqliteException ex)
        {
            return Result<List<FavoriteDrink>>.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
        catch (Exception ex)
        {
            return Result<List<FavoriteDrink>>.Failure(new Error("Unknown", ex.Message));
        }
    }

    public async Task<Result> AddAsync(FavoriteDrink drink)
    {
        string sql = "insert into FavoriteDrink (DrinkId, Name, Category) values (@DrinkId, @Name, @Category)";

        try
        {
            using var connection = _connection.CreateConnection();
            var rowsAffected = connection.Execute(sql, drink);

            if (rowsAffected > 0)
                return Result.Success();
            else
                return Result.Failure(Errors.AddFailed);
        }
        catch (SqliteException ex)
        {
            return Result.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Unknown", ex.Message));
        }
    }

    public async Task<Result> DeleteByIdAsync(int id)
    {
        string sql = $"delete from FavoriteDrink where DrinkId = @Id";

        try
        {
            using var connection = _connection.CreateConnection();
            var rowsAffected = connection.Execute(sql, new { Id = id });

            if (rowsAffected > 0)
                return Result.Success();
            else
                return Result.Failure(Errors.DeleteFailed);
        }
        catch (SqliteException ex)
        {
            return Result.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Unknown", ex.Message));
        }
    }
}
