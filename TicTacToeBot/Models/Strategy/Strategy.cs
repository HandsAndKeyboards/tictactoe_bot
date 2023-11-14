using static TicTacToeBot.Models.Strategy.NodeContent;

namespace TicTacToeBot.Models.Strategy;

/// <summary>
/// Стратегия игры в крестики-нолики. Реализована в виде префиксного дереве, в котором хранятся
/// </summary>
public class Strategy
{
    public StrategyNode RootNode { get; }

    public Strategy(StrategyNode rootNode)
    {
        if (rootNode.Content is not RootNodeContent)
            throw new ArgumentException(
                $"Контент корневого узла должен быть {RootNodeContent}, текущий: {rootNode.Content}");

        RootNode = rootNode;
    }

    public Strategy() : this(new StrategyNode(RootNodeContent))
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

    public ICollection<string> PrefixSearch(string prefix)
    {
        var results = new List<string>();

        return results;
    }
}