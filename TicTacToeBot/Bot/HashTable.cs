namespace TicTac;

/**
* <summary>
* Хэш таблица из трех измерений [кол-во строк, кол-во столбцов, кол-во пользователей]
* </summary>
*/
internal class HashTable
{
    private readonly int[,,] _table = new int[19, 19, 2];

    public HashTable()
    {
        var rand = Random.Shared;
        for (var x = 0; x < _table.GetLength(0); x++)
        for (var y = 0; y < _table.GetLength(1); y++)
        for (var z = 0; z < _table.GetLength(2); z++)
        {
            _table[x, y, z] = rand.Next();
        }
    }

    /**
 * Полное хеширование доски
 */
    public int Hash(char[,] board)
    {
        var hash = 0;
        var player = 0;
        for (var x = 0; x < 19; x++)
        for (var y = 0; y < 19; y++)
        {
            var state = board[x, y];
            switch (state)
            {
                case '_':
                    continue;
                case 'x':
                    player = 1;
                    break;
                case 'o':
                    player = 0;
                    break;
            }

            hash ^= _table[x, y, player];
        }

        return hash;
    }

    /**
 * Обновление хеша
 */
    public int UpdateHash(int hash, char figure, int row, int col)
    {
        var player = figure switch
        {
            'x' => 1,
            'o' => 0,
            _ => throw new Exception("")
        };

        hash ^= _table[row, col, player];
        return hash;
    }
}