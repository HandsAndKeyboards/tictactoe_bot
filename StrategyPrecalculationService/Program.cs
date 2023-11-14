using TicTacToeBot;
using TicTacToeBot.Data;
using TicTacToeBot.Models.Strategy;

var path = Constants.PrecalculatedStrategiesFilePath;

while (true)
{
    Console.Out.WriteLine("Что делаем?\n" +
                          "1. Вычисляем стратегии\n" +
                          "2. Удаляем вычисленные стратегии\n" +
                          "3. Завершаем программу");
    var action = Console.ReadLine();

    switch (action)
    {
        case "1":
            Precalculate();
            break;
        case "2":
            DeletePrecalculated();
            break;
        case "3":
            return;
        default:
            Console.Out.WriteLine("Некорректный ввод");
            break;
    }

    Console.Out.WriteLine("----------------------------------------------\n");
}



void Precalculate()
{
    var storage = new ProtobufFileStorage();
    var steps = InputPrecalculationStepsCount();

    var fileExists = File.Exists(path);
    var isConfirmed = false;
    if (fileExists)
        isConfirmed = IsRewriteConfirmed();

    if (fileExists && !isConfirmed)
    {
        Console.Out.WriteLine("Файл не был перезаписан");
        return;
    }
    
    var strategy = CalculateStrategies(steps);
    storage.WriteToFile(strategy, path);

    Console.Out.WriteLine("Готово");
}

Strategy CalculateStrategies(int steps)
{
    var precalculated = new Strategy();
    precalculated.Add("xox");
    return precalculated;
}

int InputPrecalculationStepsCount()
{
    Console.Out.WriteLine("Введите количество шагов для вычисления: ");
    
    uint steps;
    while (!uint.TryParse(Console.ReadLine(), out steps)) 
        Console.Out.WriteLine("Некорректный ввод");

    Console.Out.WriteLine($"шагов: {steps}");
    return (int)steps;
}

bool IsRewriteConfirmed()
{
    Console.Out.Write($"Файл {path} уже существует.\n" +
                          $"Файл будет перезаписан. Продолжить? ");
    return IsConfirmed();
}

void DeletePrecalculated()
{
    Console.Out.Write($"Расположение файла с вычисленными стратегиями: {path}\n" +
                          $"Файл будет удален. Продолжить? ");
    
    var isConfirmed = IsConfirmed();
    if (isConfirmed)
    {
        var fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
            fileInfo.Directory?.Delete();
            Console.Out.WriteLine("Файл с вычислениями был удален");
        }
        else
        {
            Console.Out.WriteLine($"Файл {path} не существует");
        }
    }
    else
    {
        Console.Out.WriteLine("Файл не был удален");
    }
}

bool IsConfirmed()
{
    string? action;
    do
    {
        Console.Out.Write("[д / н]: ");
        action = Console.ReadLine()?.Trim();
    } while (string.IsNullOrWhiteSpace(action) || !"дн".Contains(action));

    return action == "д";
}