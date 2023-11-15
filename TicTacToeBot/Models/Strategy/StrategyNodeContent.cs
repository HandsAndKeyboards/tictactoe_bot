using static TicTacToeBot.Models.Strategy.NodeAllowedContent;

namespace TicTacToeBot.Models.Strategy;

public class StrategyNodeContent
{
    public char Value { get; }

    public StrategyNodeContent(char value)
    {
        if (!IsContentAllowed(value))
            throw new ArgumentException($"Невалидное значение: {value}");

        Value = value;
    }
    
    
    
    private static bool IsContentAllowed(char c) =>
        c is X or O or Whitespace or RootNodeContent;
}