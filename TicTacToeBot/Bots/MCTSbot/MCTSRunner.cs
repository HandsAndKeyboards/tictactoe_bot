using TicTacToeBot.Bots.MCTSbot.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeBot.Bots.MCTSbot.interfaces;

namespace TicTacToeBot.Bots.MCTSbot;

// Wrapper for running MCTS by providing board as string
public class MCTSRunner
{
    public IState generate_bitboard(string board_state)
    {
        BoardState _root_state = new BoardState();
        int count_x = 0;
        int count_o = 0;
        uint[] _x_board = Enumerable.Repeat((uint)0b0000000000000000000, 19).ToArray();
        uint[] _o_board = Enumerable.Repeat((uint)0b0000000000000000000, 19).ToArray();

        int c = 0;
        //for(int i = 0; i < board_state.Length; i++)
        Parallel.For(0, 361, i =>     
        {
            if (i % 19 == 18)
            {
                string line = board_state.Substring(i - 18, 19);
                //Console.WriteLine(line);
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == 'x')
                    {
                        _x_board[c] |= _x_board[c] | (uint)(1 << j);
                        count_x++;
                    }
                    else if (line[j] == 'o')
                    {
                        _o_board[c] |= _o_board[c] | (uint)(1 << j);
                        count_o++;
                    }
                }
                c++;
            }
        });

        // Кто ходит
        bool _x_to_move = false;
        if (count_x > count_o)
            _x_to_move = true;

        _root_state = new BoardState
        {
            x_to_move = _x_to_move,
            x_board = _x_board,
            o_board = _o_board
        };

        return _root_state;
    }

    public string Run(string board_state)
    {
        var _searcher = new MCTSSearcher();
        var _root_state = generate_bitboard(board_state);

        var root_node = new Node();
        var best_move = _searcher.get_best_move(root_node, _root_state, 8);


        _root_state.play(best_move);

        return _root_state.ToString();
    }
}
