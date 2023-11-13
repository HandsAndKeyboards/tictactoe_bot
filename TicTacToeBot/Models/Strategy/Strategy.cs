using static TicTacToeBot.Models.Strategy.NodeContent; 

namespace TicTacToeBot.Models.Strategy;

/// <summary>
/// Стратегия игры в крестики-нолики. Реализована в виде префиксного дереве, в котором хранятся
/// </summary>
public class Strategy
{
    private readonly StrategyNode _rootNode = new(RootNodeContent);



    public void Add(string strategy)
    {
        var currentNode = _rootNode;
        
        foreach (var c in strategy)
        {
            if (!currentNode.HasChild(c)) 
                currentNode.AddChild(c, new StrategyNode(c));

            currentNode = currentNode.GetChild(c);
        }

        currentNode.IsKey = true;
    }
}