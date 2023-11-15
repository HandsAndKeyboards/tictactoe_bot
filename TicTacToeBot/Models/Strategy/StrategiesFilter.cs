namespace TicTacToeBot.Models.Strategy;

public class StrategiesFilter
{
    private readonly Queue<BfsNode> _bfsNodes = new();
    
    private StrategyNode _currentNode;
    private char _currentNodeContent;
    private BfsNode _currentBfsNode = null!;
    private int _currentLevel;

    private char _content;
    private int _requiredLevel;



    public StrategiesFilter(StrategyNode rootNode)
    {
        _currentNode = rootNode;
    }
    
    
    
    /// <summary>
    /// Метод удаляет все стратегии, в которых на заданной позиции присутствует символ, отличный от переданного
    /// </summary>
    /// <param name="position">Позиция символа в строковом представлении игрового поля</param>
    /// <param name="filteringContent">Переданный символ, по которому будет производиться фильтрация</param>
    public void Filter(int position, StrategyNodeContent filteringContent)
    {
        InitFields(position, filteringContent);
        
        EnqueueRootNode();
        while (AnyNodesToProcess())
        {
            DequeNextNode();

            if (IsCurrentLevelLessRequired())
                EnqueueCurrentNodeChildren();
            else if (IsLevelMatchingRequired() && IsContentDifferentFromRequired()) 
                RemoveBadStrategy();
        }
    }



    private void InitFields(int position, StrategyNodeContent nodeContent)
    {
        _content = nodeContent.Value;

        // +1 потому, что в корне дерева лежит дополнительный символ - '\0'
        _requiredLevel = position + 1;
    }

    private void EnqueueRootNode() =>
        // родитель корня никогда не будет использоваться
        _bfsNodes.Enqueue(new BfsNode(_currentNode, default!, 0));
    
    private bool AnyNodesToProcess() => 
        _bfsNodes.Any();

    private void DequeNextNode()
    {
        _currentBfsNode = _bfsNodes.Dequeue();
        _currentNode = _currentBfsNode.Node;
        _currentNodeContent = _currentNode.Content;
        _currentLevel = _currentBfsNode.Level;
    }
    
    private bool IsCurrentLevelLessRequired() => 
        _currentLevel < _requiredLevel;
    
    private void EnqueueCurrentNodeChildren()
    {
        foreach (var (_, node) in _currentNode.ChildNodes)
            _bfsNodes.Enqueue(new BfsNode(node, _currentBfsNode, _currentLevel + 1));
    }
    
    private bool IsContentDifferentFromRequired() => 
        _currentNodeContent != _content;

    private bool IsLevelMatchingRequired() => 
        _currentLevel == _requiredLevel;
    
    private void RemoveBadStrategy()
    {
        /*
         * если мы достигли заданного уровня и у текущего узла контент отличаетсяот трубуемого, значит,
         * контент, скорее всего, будет найден на одном из других узлов этого уровня - инициируем
         * процесс удаления неподходящий стратегий
         */

        /*
         * 1. ищем ближайший сверху узел, имеющий несколко дочерних элементов
         * 2. записываем потомка, из которого пришли
         * 2. удаляем из дочерних узлов найденного узла записанного потомка
         */

        var parentBfsNode = _currentBfsNode.ParentBfsNode;
        var parent = parentBfsNode.Node;
        var child = _currentBfsNode.Node;

        while (parent.ChildNodes.Count < 2)
        {
            child = parentBfsNode.Node;
            parentBfsNode = parentBfsNode.ParentBfsNode;
            parent = parentBfsNode.Node;
        }

        parent.ChildNodes.Remove(child.Content);
    }



    private record BfsNode(StrategyNode Node, BfsNode ParentBfsNode, int Level);
}