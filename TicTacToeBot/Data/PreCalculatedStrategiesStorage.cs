using TicTacToeBot.Models.Strategy;

namespace TicTacToeBot.Data;

public interface IPreCalculatedStrategiesFileStorage
{
    public void WriteToFile(Strategy strategy, string filename);
    public Strategy ReadFromFile(string filename);
}