namespace TicTacToeBot.Bots.MDTFbot;

public class Bot
{
    private Move _bestMove;

    private readonly Dictionary<int, CacheNode> _cache = new();

    private readonly Dictionary<int, int> _stateCache = new();

    private int _maximumDepth;

    private const int MaxScore = int.MinValue + 1;

    private readonly HashTable _table = new();

    private const int ClosedOne = 1;
    private const int ClosedTwo = 10;
    private const int ClosedThree = 100;
    private const int ClosedFour = 1000;
    private const int OpenOne = 10;
    private const int OpenTwo = 100;
    private const int OpenThree = 1000;
    private const int OpenFour = 10000;
    private const int Five = 100000;

    private const int Rows = 19;
    private const int Columns = 19;

    private readonly char _figure;
    private readonly char _enemyFigure;

    private bool _isFirst;

    private const int DepthIterator = 6;

    public Bot(char figure)
    {
        _figure = figure;
        _enemyFigure = figure == 'x' ? 'o' : 'x';
        _isFirst = _figure == 'x';
    }

    public string Turn(string request)
    {
        var str = request.ToCharArray();

        var board = ConvertorFunc(str);


        var point = _isFirst ? GenerateFirstSeed() : MakeNextTurn(board);
        var index = point.ToPlainPoint();
        str[index] = _figure;
        return string.Concat(str);
    }

    Point GenerateFirstSeed()
    {
        const int half = 19 / 2;
        _isFirst = false;
        return new Point(half, half);
    }

    Point MakeNextTurn(char[,] board)
    {
        var move = IterativeMtdf(DepthIterator, board);
        return new Point(move.Column, move.Row);
    }

    char[,] ConvertorFunc(IList<char> board)
    {
        var arr = new char[19, 19];
        for (var j = 0; j < 19; j++)
            for (var k = 0; k < 19; k++)
                arr[j, k] = board[19 * j + k];

        return arr;
    }

    /**
 * <summary>
 * Оценивает состояние последовательности
 * </summary>
 * <returns>
 * Сумма очков
 * </returns>
 */
    int EvaluateBlock(int blocks, int pieces)
    {
        return blocks switch
        {
            0 => pieces switch
            {
                1 => OpenOne,
                2 => OpenTwo,
                3 => OpenThree,
                4 => OpenFour,
                _ => Five
            },
            1 => pieces switch
            {
                1 => ClosedOne,
                2 => ClosedTwo,
                3 => ClosedThree,
                4 => ClosedFour,
                _ => Five
            },
            _ => pieces >= 5 ? Five : 0
        };
    }

    /**
 * Ищем последовательность из 5 одинаковых фигур в направлении
 */
    bool CheckDirections(int[,] arr, int index)
    {
        const int size = 9;
        for (var i = 0; i < size - 4; i++)
        {
            if (arr[index, i] == '_') continue;
            // Если один из элементов находится за границей доски
            if (arr[index, i] == 2 || arr[index, i + 1] == 2 || arr[index, i + 2] == 2 || arr[index, i + 3] == 2 ||
                arr[index, i + 4] == 2)
                return false;
            // Если последовательность найдена
            if (arr[index, i] == arr[index, i + 1] && arr[index, i] == arr[index, i + 2] &&
                arr[index, i] == arr[index, i + 3] && arr[index, i] == arr[index, i + 4])
                return true;
        }

        return false;
    }


    /**
 * Проверяет на победу в последовательностях
 */
    bool CheckWin(char[,] board, int x, int y)
    {
        var directions = GetDirections(board, x, y);
        for (var i = 0; i < 4; i++)
            if (CheckDirections(directions, i))
                return true;
        return false;
    }

    /**
 * <summary>
 * Обновления прямоугольник вхождения ходов
 * </summary>
 * <param name="restrictions">старый прямоугольник вхождения ходов</param>
 * <param name="row">индекс строки</param>
 * <param name="column">индекс столбца</param>
 * <returns>
 * Новый массив из четырех элементов [минимальная строка,
 * минимальный столбец, максимальная строка,
 * максимальный столбец]
 * </returns>
 */
    int[] ChangeRestrictions(IReadOnlyList<int> restrictions, int row, int column)
    {
        var minRow = restrictions[0];
        var minColumn = restrictions[1];
        var maxRow = restrictions[2];
        var maxColumn = restrictions[3];

        if (row < minRow)
            minRow = row;
        else if (row > maxRow)
            maxRow = row;

        if (column < minColumn)
            minColumn = column;
        else if (column > maxColumn)
            maxColumn = column;

        if (minRow - 2 < 0) minRow = 2;
        if (minColumn - 2 < 0) minColumn = 2;
        if (maxRow + 2 >= Rows)
            maxRow = Rows - 3;
        if (maxColumn + 2 >= Columns)
            maxColumn = Columns - 3;
        return new[] { minRow, minColumn, maxRow, maxColumn };
    }

    /**
 * <summary>
 * Создает массив направлений
 * </summary>
 * <returns>
 * int[4][9]
 * </returns>
 */
    int[,] GetDirections(char[,] board, int x, int y)
    {
        var directions = new int[4, 9];
        var aI = 0;
        var bI = 0;
        var cI = 0;
        var dI = 0;

        for (var i = -4; i < 5; i++)
        {
            if (x + i >= 0 && x + i <= Rows - 1)
            {
                directions[0, aI] = board[x + i, y];
                aI++;
                if (y + i >= 0 && y + i <= Columns - 1)
                {
                    directions[1, bI] = board[x + i, y + i];
                    bI++;
                }
            }

            if (y + i < 0 || y + i > Columns - 1) continue;
            directions[2, cI] = board[x, y + i];
            cI++;

            if (x - i < 0 || x - i > Rows - 1) continue;
            directions[3, dI] = board[x - i, y + i];
            dI++;
        }

        // Если один из элементов находился за границей доски
        if (aI != 9)
            directions[0, aI] = 2;

        if (bI != 9)
            directions[1, bI] = 2;

        if (cI != 9)
            directions[2, cI] = 2;

        if (dI != 9)
            directions[3, dI] = 2;

        return directions;
    }

    int EvaluateState(char[,] board, char player, int hash, IReadOnlyList<int> restrictions)
    {
        var blackScore = EvalBoard(board, 'x', restrictions);
        var whiteScore = EvalBoard(board, 'o', restrictions);
        var score = player switch
        {
            'x' => blackScore - whiteScore,
            'o' => whiteScore - blackScore,
            _ => throw new Exception("")
        };
        _stateCache[hash] = score;
        return score;
    }
    /**
     * <summary>
     * Оценивает ход игрока
     * </summary>
     * <returns>
     * Возвращает количество очков
     * </returns>
     */
    int EvaluateMove(char[,] board, int x, int y, char player)
    {
        var score = 0;
        var directions = GetDirections(board, x, y);
        for (var i = 0; i < 4; i++)
        {
            var tempScore = EvaluateDirection(directions, i, player);
            if (tempScore == MaxScore)
                return MaxScore;

            score += tempScore;
        }

        return score;
    }

    List<Move> BoardGenerator(IReadOnlyList<int> restrictions, char[,] board, char player)
    {
        var availSpotsScore = new List<Move>(10); //c is j  r is i;
        var minRow = restrictions[0];
        var minColumn = restrictions[1];
        var maxRow = restrictions[2];
        var maxColumn = restrictions[3];

        for (var row = minRow - 2; row <= maxRow + 2; row++)
            for (var column = minColumn - 2; column <= maxColumn + 2; column++)
            {
                if (board[row, column] != '_' || RemoteCell(board, row, column)) continue;
                var move = new Move
                {
                    Row = row,
                    Column = column,
                    Score = EvaluateMove(board, row, column, player)
                };
                if (move.Score == MaxScore)
                    return new List<Move> { move };

                availSpotsScore.Add(move);
            }

        return availSpotsScore.OrderByDescending(x => x.Score).Take(10).ToList();
    }

    int Negamax(char[,] board, char figure, int depth, int a, int b, int hash, int[] restrictions, int lastI,
        int lastJ)
    {
        var alphaOrig = a;
        if (_cache.TryGetValue(hash, out var value) && value.Depth >= depth)
        {
            var score = _cache[hash].Score;
            switch (_cache[hash].Flag)
            {
                case 0:
                    return score;
                case -1:
                    a = Math.Max(a, score);
                    break;
                case 1:
                    b = Math.Min(b, score);
                    break;
            }

            if (a >= b)
                return score;
        }

        if (CheckWin(board, lastI, lastJ))
            return -2000000 + (_maximumDepth - depth);

        if (depth == 0)
            return !_stateCache.ContainsKey(hash)
                ? EvaluateState(board, figure, hash, restrictions)
                : _stateCache[hash];

        var availSpots = BoardGenerator(restrictions, board, figure);

        var availSpotsSize = availSpots.Count;

        if (availSpotsSize == 0) return 0;


        var bestScore = int.MinValue + 1;
        for (var y = 0; y < availSpotsSize; y++)
        {
            var row = availSpots[y].Row;
            var column = availSpots[y].Column;

            var newHash = _table.UpdateHash(hash, figure, row, column);
            board[row, column] = figure;
            var newRestrictions = ChangeRestrictions(restrictions, row, column);
            var score = -Negamax(board, figure == _figure ? _enemyFigure : _figure,
                depth - 1, -b, -a, newHash, newRestrictions, row, column);

            board[row, column] = '_';

            if (score > bestScore)
            {
                bestScore = score;
                if (depth == _maximumDepth)
                {
                    _bestMove = new Move
                    {
                        Row = row,
                        Column = column,
                        Score = score
                    };
                }
            }

            a = Math.Max(a, score);
            if (a >= b)
                break;
        }

        CacheNode cacheNode;

        cacheNode.Score = bestScore;
        cacheNode.Depth = depth;
        if (bestScore <= alphaOrig)
            cacheNode.Flag = 1;
        else if (bestScore >= b)
            cacheNode.Flag = -1;
        else
            cacheNode.Flag = 0;

        _cache[hash] = cacheNode;
        return bestScore;
    }

    int EvaluateDirection(int[,] direction, int index, char figure)
    {
        var score = 0;
        for (var i = 0; i + 4 < 9; i++)
        {
            var you = 0;
            var enemy = 0;
            if (direction[index, i] == 2)
                return score;

            for (var j = 0; j <= 4; j++)
            {
                if (direction[index, i + j] == 2)
                    return score;

                if (direction[index, i + j] == figure)
                    you++;
                else if (direction[index, i + j] == (figure == _figure ? _enemyFigure : _figure))
                    enemy++;
            }

            score += EvaluateFF(GetSequence(you, enemy));
            if (score >= 800000)
                return MaxScore;
        }

        return score;
    }

    int GetSequence(int mySequence, int enemySequence)
    {
        if (mySequence + enemySequence == 0)
            return 0;
        if (mySequence != 0 && enemySequence == 0)
            return mySequence;
        if (mySequence == 0 && enemySequence != 0)
            return -enemySequence;
        if (mySequence != 0 && enemySequence != 0)
            return 17;

        throw new Exception("");
    }

    int EvaluateFF(int sequence)
    {
        return sequence switch
        {
            0 => 7,
            1 => 35,
            2 => 800,
            3 => 15000,
            4 => 800000,
            -1 => 15,
            -2 => 400,
            -3 => 1800,
            -4 => 100000,
            17 => 0,
            _ => throw new Exception("")
        };
    }

    bool RemoteCell(char[,] board, int row, int column)
    {
        for (var i = row - 2; i <= row + 2; i++)
        {
            if (i is < 0 or >= Rows) continue;
            for (var j = column - 2; j <= column + 2; j++)
            {
                if (j is < 0 or >= Columns) continue;
                if (board[i, j] != '_') return false;
            }
        }

        return true;
    }

    int EvalBoard(char[,] board, char figure, IReadOnlyList<int> restrictions)
    {
        var score = 0;
        var minRow = restrictions[0];
        var minColumn = restrictions[1];
        var maxRow = restrictions[2];
        var maxColumn = restrictions[3];

        for (var row = minRow; row < maxRow + 1; row++)
            for (var column = minColumn; column < maxColumn + 1; column++)
            {
                if (board[row, column] != figure) continue;
                var block = 0;
                var piece = 1;
                // left
                if (column == 0 || board[row, column - 1] != '_') block++;
                // pieceNum
                for (column++; column < Columns && board[row, column] == figure; column++) piece++;
                // right
                if (column == Columns || board[row, column] != '_')
                    block++;
                score += EvaluateBlock(block, piece);
            }

        for (var column = minColumn; column < maxColumn + 1; column++)
            for (var row = minRow; row < maxRow + 1; row++)
            {
                if (board[row, column] != figure) continue;
                var block = 0;
                var piece = 1;
                // left
                if (row == 0 || board[row - 1, column] != '_') block++;
                // pieceNum
                for (row++; row < Rows && board[row, column] == figure; row++) piece++;
                // right
                if (row == Rows || board[row, column] != '_') block++;
                score += EvaluateBlock(block, piece);
            }

        for (var n = minRow; n < maxColumn - minColumn + maxRow; n++)
        {
            var row = n;
            var column = minColumn;
            while (row >= minRow && column <= maxColumn)
            {
                if (row <= maxRow)
                    if (board[row, column] == figure)
                    {
                        var block = 0;
                        var piece = 1;
                        // left
                        if (column == 0 || row == Rows - 1 || board[row + 1, column - 1] != '_') block++;
                        // pieceNum
                        row--;
                        column++;
                        for (; row >= 0 && column < Columns && board[row, column] == figure; row--)
                        {
                            piece++;
                            column++;
                        }

                        // right
                        if (row < 0 || column == Columns || board[row, column] != '_') block++;
                        score += EvaluateBlock(block, piece);
                    }

                row -= 1;
                column += 1;
            }
        }

        for (var n = minRow - (maxColumn - minColumn); n <= maxRow; n++)
        {
            var row = n;
            var column = minColumn;
            while (row <= maxRow && column <= maxColumn)
            {
                if (row >= minRow && row <= maxRow)
                {
                    if (board[row, column] == figure)
                    {
                        var block = 0;
                        var piece = 1;
                        // left
                        if (column == 0 || row == 0 || board[row - 1, column - 1] != '_') block++;
                        // pieceNum
                        row++;
                        column++;
                        for (; row < Rows && column < Columns && board[row, column] == figure; row++)
                        {
                            piece++;
                            column++;
                        }

                        // right
                        if (row == Rows || column == Columns || board[row, column] != '_') block++;
                        score += EvaluateBlock(block, piece);
                    }
                }

                row += 1;
                column += 1;
            }
        }

        return score;
    }

    Move Mtdf(char[,] board, int f, int depth, int[] restrictions)
    {
        var g = f;
        var upperBound = int.MaxValue - 1;
        var lowerBound = int.MinValue + 1;
        Move lastSuccessful;
        do
        {
            var b = g == lowerBound ? g + 1 : g;

            var result = Negamax(board, _figure, depth, b - 1, b, _table.Hash(board), restrictions, 0, 0);
            g = result;
            lastSuccessful = _bestMove;

            if (g < b)
                upperBound = g;
            else
                lowerBound = g;
        } while (lowerBound < upperBound);

        return lastSuccessful;
    }

    Move IterativeMtdf(int depth, char[,] gameBoard)
    {
        _bestMove = new Move();
        var i = 2;
        var restrictions = new[] { 0, 0, Rows - 1, Columns - 1 };
        var score = EvaluateState(gameBoard, _figure, _table.Hash(gameBoard), restrictions);
        while (i != depth + 2)
        {
            _maximumDepth = i;
            score = Mtdf(gameBoard, score, i, GetRestrictions(gameBoard)).Score;
            if (score > 1999900)
                break;
            i += 2;
        }

        if (gameBoard[_bestMove.Row, _bestMove.Column] != '_')
            Console.WriteLine("error");
        return _bestMove;
    }

    /**
 * <summary>
 * Создает прямоугольник в который входят все сделанные ходы
 * </summary>
 * <param name="board">массив из 19 строк и 19 столбцов из элементов char</param>>
 * <returns>
 * Массив из четырех элементов [минимальная строка,
 * минимальный столбец, максимальная строка,
 * максимальный столбец]
 * </returns>
 */
    int[] GetRestrictions(char[,] board)
    {
        var minRow = int.MaxValue - 1;
        var minColumn = int.MaxValue - 1;
        var maxRow = int.MinValue + 1;
        var maxColumn = int.MinValue + 1;

        for (var row = 0; row < Rows; row++)
            for (var column = 0; column < Columns; column++)
            {
                if (board[row, column] == '_') continue;
                minRow = Math.Min(minRow, row);
                minColumn = Math.Min(minColumn, column);
                maxRow = Math.Max(maxRow, row);
                maxColumn = Math.Max(maxColumn, column);
            }

        if (minRow - 2 < 0) minRow = 2;

        if (minColumn - 2 < 0) minColumn = 2;

        if (maxRow + 2 >= Rows)
            maxRow = Rows - 3;

        if (maxColumn + 2 >= Columns)
            maxColumn = Columns - 3;

        var restrictions = new[] { minRow, minColumn, maxRow, maxColumn };
        return restrictions;
    }
}