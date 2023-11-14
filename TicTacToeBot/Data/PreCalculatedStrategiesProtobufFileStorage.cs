using Google.Protobuf;
using TestDotnet.Data.Entities;
using TicTacToeBot.Models.Strategy;
using static TicTacToeBot.Constants;

namespace TicTacToeBot.Data;

public class PreCalculatedStrategiesProtobufFileStorage : IPreCalculatedStrategiesFileStorage
{
    public void WriteToFile(Strategy strategy, string filename)
    {
        var rootEntity = SerializeStrategy(strategy.RootNode);
        var strategyEntity = new StrategyEntity { RootNode = rootEntity };

        using var file = File.Create(filename);
        using var outStream = new CodedOutputStream(file);
        strategyEntity.WriteTo(outStream);
    }

    public Strategy ReadFromFile(string filename)
    {
        using var file = File.OpenRead(filename);
        using var inStream = CodedInputStream.CreateWithLimits(file, int.MaxValue, GameFieldSize * 2 + 1);

        var strategyEntity = StrategyEntity.Parser.ParseFrom(inStream);
        var rootEntity = strategyEntity.RootNode;

        var root = DeserializeStrategy(rootEntity);
        var strategy = new Strategy(root);
        return strategy;
    }



    private StrategyNodeEntity SerializeStrategy(StrategyNode current)
    {
        var entity = new StrategyNodeEntity { Content = current.Content, IsKey = current.IsKey };

        foreach (var (content, node) in current.ChildNodes)
            entity.ChildNodes[content] = SerializeStrategy(node);

        return entity;
    }

    private StrategyNode DeserializeStrategy(StrategyNodeEntity current)
    {
        var node = new StrategyNode((char)current.Content, current.IsKey);

        foreach (var (content, entity) in current.ChildNodes)
            node.ChildNodes[(char)content] = DeserializeStrategy(entity);

        return node;
    }
}