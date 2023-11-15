using TicTacToeBot.Models.Strategy;

namespace TicTacToeBot.Data;

public interface IStrategiesFileStorage
{
    public void WriteToFile(Strategies strategies, string path);
    public Strategies ReadFromFile(string path);
}