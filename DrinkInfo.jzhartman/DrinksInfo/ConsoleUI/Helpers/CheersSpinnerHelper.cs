using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Helpers;

public sealed class CheersSpinnerHelper : Spinner
{
    public override TimeSpan Interval => TimeSpan.FromMilliseconds(100);

    public override IReadOnlyList<string> Frames => new[]
    {
        "🍺        🍺", " 🍺      🍺 ", "  🍺    🍺  ", "   🍺  🍺   ", "    🍺🍺    "
    };

    public override bool IsUnicode => true;
}