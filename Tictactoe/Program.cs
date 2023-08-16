using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tictactoe
{
    internal class Program
    {
        static char[] board = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int turn = 1;
        static int num;
        static int flag = 0;

        static void Main(string[] args)
        {
            while (flag != -1 && flag != 1)
            {
                Console.Clear();
                Console.WriteLine("플레이어 1: X 와 플레이어 2: O");
                Console.WriteLine("\n");

                if (turn % 2 == 0)
                {
                    Console.WriteLine("플레이어 2의 차례");
                }
                else
                {
                    Console.WriteLine("플레이어 1의 차례");
                }

                Console.WriteLine("\n");
                PrintBoard();

                InputNumber();

                while (board[num] == 'X' || board[num] == 'O')
                {
                    Console.WriteLine("죄송합니다. {0} 행은 이미 {1}로 표시되어 있습니다.", num, board[num]);
                    InputNumber();
                }

                if (turn % 2 == 0) board[num] = 'O';
                else board[num] = 'X';
                turn++;

                flag = Check();
            }

            if (flag == 1)
            {
                Console.WriteLine("플레이어 {0}이(가) 이겼습니다.", (turn % 2) + 1);
            }
            else
            {
                Console.WriteLine("무승부");
            }
        }

        static void InputNumber()
        {
            Console.WriteLine("숫자를 입력해주세요 :");
            string line = Console.ReadLine();

            while (!int.TryParse(line, out num))
            {
                Console.WriteLine("숫자만 입력 가능합니다 :");
                line = Console.ReadLine();
            }
        }

        static void PrintBoard()
        {
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", board[1], board[2], board[3]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", board[4], board[5], board[6]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", board[7], board[8], board[9]);
            Console.WriteLine("     |     |     ");
        }

        static int Check()
        {
            // 가로 승리
            if (board[1] == board[2] && board[2] == board[3])
            {
                return 1;
            }
            else if (board[4] == board[5] && board[5] == board[6])
            {
                return 1;
            }
            else if (board[7] == board[8] && board[8] == board[9])
            {
                return 1;
            }

            // 세로 승리
            else if (board[1] == board[4] && board[4] == board[7])
            {
                return 1;
            }
            else if (board[2] == board[5] && board[5] == board[8])
            {
                return 1;
            }
            else if (board[3] == board[6] && board[6] == board[9])
            {
                return 1;
            }

            // 대각선
            else if (board[1] == board[5] && board[5] == board[9])
            {
                return 1;
            }
            else if (board[3] == board[5] && board[5] == board[7])
            {
                return 1;
            }

            // 무승부
            else if (board[1] != '1' && board[2] != '2' && board[3] != '3' && board[4] != '4' && board[5] != '5' && board[6] != '6' &&
                board[7] != '7' && board[8] != '8' && board[9] != '9')
            {
                return -1;
            }
            else { return 0; }
        }
    }
}
