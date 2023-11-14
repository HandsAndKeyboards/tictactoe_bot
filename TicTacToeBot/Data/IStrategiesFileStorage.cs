using TicTacToeBot.Models.Strategy;

namespace TicTacToeBot.Data;

public interface IStrategiesFileStorage
{
    public void WriteToFile(Strategy strategy, string path);
    public Strategy ReadFromFile(string path);
}