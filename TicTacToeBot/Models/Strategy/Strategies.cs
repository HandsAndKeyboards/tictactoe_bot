using static TicTacToeBot.Models.Strategy.NodeAllowedContent;

namespace TicTacToeBot.Models.Strategy;

/// <summary>
/// Стратегия игры в крестики-нолики. Реализована в виде префиксного дереве, в котором хранятся
/// </summary>
public class Strategies
{
    public StrategyNode RootNode { get; }
    
    

    public Strategies(StrategyNode rootNode)
    {
        if (rootNode.Content is not RootNodeContent)
            throw new ArgumentException(
                $"Контент корневого узла должен быть {RootNodeContent}, текущий: {rootNode.Content}");

        RootNode = rootNode;
    }

    public Strategies() : this(new StrategyNode(RootNodeContent))
    {
    }



    public void Add(string strategy)
    {
        var currentNode = RootNode;

        foreach (var c in strategy)
        {
            if (!currentNode.HasChild(c))
                currentNode.AddChild(c, new StrategyNode(c));

            currentNode = currentNode.GetChild(c);
        }

        currentNode.IsKey = true;
    }

    /// <summary>
    /// Метод удаляет все стратегии, в которых на заданной позиции присутствует символ, отличный от переданного
    /// </summary>
    /// <param name="position">Позиция символа в строковом представлении игрового поля</param>
    /// <param name="filteringContent">Переданный символ, по которому будет производиться фильтрация</param>
    public void FilterStrategies(int position, StrategyNodeContent filteringContent)
    {
        var filter = new StrategiesFilter(RootNode);
        filter.Filter(position, filteringContent);
    }
}

