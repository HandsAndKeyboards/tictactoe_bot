namespace TicTacToeBot.Config;

public class BotConfig
{
    public required string SessionId { get; init; }
    public required string BotUrl { get; init; }
    public required string MediatorUrl { get; init; }
    public required string BotId { get; init; }
    public required string Password { get; init; }
}