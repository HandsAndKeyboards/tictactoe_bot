using System.Data;
using Google.Protobuf;
using TestDotnet.Data.Entities;
using TicTacToeBot.Models.Strategy;
using static TicTacToeBot.Constants;

namespace TicTacToeBot.Data;

public class ProtobufFileStorage : IStrategiesFileStorage
{
    public void WriteToFile(Strategy strategy, string path)
    {
        CreateDirectoryIfNotExists(path);
        
        var rootEntity = SerializeStrategy(strategy.RootNode);
        var strategyEntity = new StrategyEntity { RootNode = rootEntity };

        using var file = File.Create(path);
        using var outStream = new CodedOutputStream(file);
        strategyEntity.WriteTo(outStream);
    }

    public Strategy ReadFromFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Файл {path} не существует");
        
        using var file = File.OpenRead(path);
        using var inStream = CodedInputStream.CreateWithLimits(file, int.MaxValue, GameFieldSize * 2 + 1);

        var strategyEntity = StrategyEntity.Parser.ParseFrom(inStream);
        var rootEntity = strategyEntity.RootNode;

        var root = DeserializeStrategy(rootEntity);
        var strategy = new Strategy(root);
        return strategy;
    }



    private static void CreateDirectoryIfNotExists(string filepath)
    {
        var parentDirectory = new FileInfo(filepath).Directory;
        if (parentDirectory is null)
            throw new NoNullAllowedException($"{nameof(parentDirectory)} не может быть null");

        if (!parentDirectory.Exists) 
            TryCreateDirectoryOrThrow(parentDirectory);
    }

    private static void TryCreateDirectoryOrThrow(DirectoryInfo directory)
    {
        try
        {
            directory.Create();
        }
        catch (IOException err)
        {
            throw new InvalidOperationException($"Невозможно создать директорию {directory.FullName}", err);
        }
    }
    
    private static StrategyNodeEntity SerializeStrategy(StrategyNode current)
    {
        var entity = new StrategyNodeEntity { Content = current.Content, IsKey = current.IsKey };

        foreach (var (content, node) in current.ChildNodes)
            entity.ChildNodes[content] = SerializeStrategy(node);

        return entity;
    }

    private static StrategyNode DeserializeStrategy(StrategyNodeEntity current)
    {
        var node = new StrategyNode((char)current.Content, current.IsKey);

        foreach (var (content, entity) in current.ChildNodes)
            node.ChildNodes[(char)content] = DeserializeStrategy(entity);

        return node;
    }
}