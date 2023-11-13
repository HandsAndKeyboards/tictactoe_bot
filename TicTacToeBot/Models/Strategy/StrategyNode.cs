using static TicTacToeBot.Models.Strategy.NodeContent;

namespace TicTacToeBot.Models.Strategy;

/// <summary>
/// Узел стратегии. Содержит 
/// </summary>
public class StrategyNode
{
    /// <summary>
    /// Символ, содержащийся в узле
    /// </summary>
    public char Content { get; init; }

    /// <summary>
    /// Содержит ли текуший узел корректный ключ
    /// </summary>
    public bool IsKey { get; set; }

    /// <summary>
    /// Дочерние узлы
    /// </summary>
    public StrategyNodes ChildNodes { get; } = new();



    /// <summary>
    /// Конструктор узла
    /// </summary>
    /// <param name="content">Символ, содержащийся в узле</param>
    /// <param name="isKey">Содержит ли текуший узел корректный ключ</param>
    /// <exception cref="ArgumentException">Входящий символ не принимает значение из списка: <see cref="NodeContent"/></exception>
    public StrategyNode(char content, bool isKey = false)
    {
        if (!IsContentAllowed(content))
            throw new ArgumentException($"Невалидное значение: {content}");

        Content = content;
        IsKey = isKey;
    }



    public bool HasChild(char c) =>
        ChildNodes.ContainsKey(c);

    public StrategyNode GetChild(char c) =>
        ChildNodes[c];

    public void AddChild(char c, StrategyNode child)
    {
        if (child is null)
            throw new ArgumentNullException(nameof(child));
        if (HasChild(c))
            throw new InvalidOperationException("Узел уже содержит дочерний с заданным символом");

        ChildNodes[c] = child;
    }



    private static bool IsContentAllowed(char c) =>
        c is X or O or Whitespace or RootNodeContent;
}