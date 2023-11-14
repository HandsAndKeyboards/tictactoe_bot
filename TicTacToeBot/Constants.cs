namespace TicTacToeBot;

public static class Constants
{
    /// <summary>
    /// Количество клеток в игровом поле
    /// </summary>
    public const int GameFieldSize = 361;

    /// <summary>
    /// Путь к файлу с предвычисленными стратегиями
    /// </summary>
    public static readonly string PrecalculatedStrategiesFilePath = Path.Combine(Path.GetTempPath(), "TicTacToeBot", "precalculated-strategies.bin");
}