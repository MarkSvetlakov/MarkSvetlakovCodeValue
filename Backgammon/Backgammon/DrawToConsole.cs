using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class DrawToConsole
    {
        Stopwatch stopwatch = new Stopwatch();
        private Field[] _fieldsToDraw;

        public int DrawStartMenu()
        {
            Console.Clear();
            int result = 0;
            bool legalNumber = false;
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("Choose game mode:");
            strBuilder.AppendLine("1. Player vs Player");
            strBuilder.AppendLine("2. Exit");
            Console.WriteLine(strBuilder);

            while (!legalNumber)
            {
                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Console.WriteLine("Must be a number!");
                }
                else if(result < 1 || result > 2)
                {
                    Console.WriteLine("Wrong option!");
                }
                else
                {
                    legalNumber = true;
                }
            }
            return result;
        }


        public string DrawAskHumanName(string numOfPlayer)
        {
            bool legalName = false;
            string name = "";
            Console.Clear();
            Console.WriteLine($"{numOfPlayer}, Enter your Nickname:");


            while (!legalName)
            {
                name = Console.ReadLine();
                if (name.Length < 3)
                {
                    Console.WriteLine("Name must be no less than 3 letters!");
                }
                else
                {
                    legalName = true;
                }
            }
            return name;
        }


        public void DrawFirstThrow(Dice dice, Player player1, Player player2)
        {
            StringBuilder strBuilder = new StringBuilder();
            Console.Clear();
            strBuilder.AppendLine($"{player1.Name} throw: {dice.FirstCube}, {player2.Name} throw: {dice.SecondCube}");
            strBuilder.AppendLine("");
            strBuilder.AppendLine($"{player1.Name} color: {player1.Color}, {player2.Name} color: {player2.Color}");
            strBuilder.AppendLine("Enter to continue...");

            Console.WriteLine(strBuilder);
            Console.ReadLine();
        }

        public void DrawAskToThrow(Player player, Dice dice, GameBoard board)
        {
            DrawBoard(board);
            Console.WriteLine($"{player.Color}: {player.Name} your turn. Press enter to throw...");
            Console.ReadLine();

            if (dice.RolledDouble)
            {
                Console.WriteLine("Double!");
                Console.WriteLine($"Throw result: {dice.FirstCube} {dice.SecondCube}");
            }
            else
            {
                Console.WriteLine($"Throw result: {dice.FirstCube} {dice.SecondCube}");
            }
        }


        public void DrawWrongMove(Player player, GameBoard board)
        {
            DrawBoard(board);
            Console.WriteLine($"{player.Name} you can't move there!");
        }


        public void DrawNoAvailableMoves(Player player, GameBoard board, Dice dice)
        {
            DrawBoard(board);
            Console.WriteLine($"Throw result: {dice.FirstCube} {dice.SecondCube}");
            Console.WriteLine($"{player.Name} you dont have available moves!");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }


        public int[] DrawAskToMove(Player player, Dice dice, GameBoard board)
        {
            DrawBoard(board);
            bool legalMoveFrom = false;
            bool legalMoveTo = false;
            int[] fromTo = new int[2];

            if (dice.RolledDouble)
            {
                Console.WriteLine("Double!");
                Console.WriteLine($"Throw result: {dice.FirstCube} {dice.SecondCube}");
            }
            else
            {
                Console.WriteLine($"Throw result: {dice.FirstCube} {dice.SecondCube}");
            }

            Console.WriteLine($"{player.Color}: {player.Name} Make your move");
            Console.WriteLine($"Moves left: {player.Moves}");

            if (player.Color.Equals("White") && board.Fields[26].WhiteCheckers > 0 && player.IsMyTurn)
            {
                Console.WriteLine("Move from bar!");
            }
            else if (player.Color.Equals("Red") && board.Fields[26].RedCheckers > 0 && player.IsMyTurn)
            {
                Console.WriteLine("Move from bar!");
                fromTo[0] = 25;
            }
            else
            {
                Console.WriteLine("From:");
                while (!legalMoveFrom)
                {
                    if (!int.TryParse(Console.ReadLine(), out fromTo[0]))
                    {
                        Console.WriteLine("Must be a number!");
                    }
                    else if (fromTo[0] < 0 || fromTo[0] > 25)
                    {
                        Console.WriteLine("Field does not exist!");
                    }
                    else
                    {
                        legalMoveFrom = true;
                    }
                }
            }
            

            Console.WriteLine("To:");
            while (!legalMoveTo)
            {
                if (!int.TryParse(Console.ReadLine(), out fromTo[1]))
                {
                    Console.WriteLine("Must be a number!");
                }
                else if (fromTo[1] < 0 || fromTo[1] > 25)
                {
                    Console.WriteLine("Field does not exist!");
                }
                else
                {
                    legalMoveTo = true;
                }
            }
            return fromTo;
        }



        public int DrawWinner(Player player)
        {
            int result = 0;
            bool legalNumber = false;
            Console.Clear();
            Console.WriteLine($"Player {player.Name} is WIN!");
            Console.WriteLine("Start new game? {1} Yes {0} No");

            while (!legalNumber)
            {
                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Console.WriteLine("Must be a number!");
                }
                else if (result < 0 || result > 1)
                {
                    Console.WriteLine("Wrong option!");
                }
                else
                {
                    legalNumber = true;
                }
            }
            return result;
        }


        public void DrawGoodBye()
        {
            Console.Clear();
            Console.WriteLine("Thanks for playing my game!");
            Console.WriteLine("Regards, Mark Svetlakov!");
        }


        public void DrawBoard(GameBoard board)
        {
            bool center = true;
            bool outZone = true;
            Console.Clear();
            _fieldsToDraw = new Field[board.Fields.Length];

            for (int i = 0; i < board.Fields.Length; i++)
            {
                if (board.Fields[i].RedCheckers > 0)
                {
                    _fieldsToDraw[i] = new Field();
                    _fieldsToDraw[i].RedCheckers = board.Fields[i].RedCheckers;
                }
                else if (board.Fields[i].WhiteCheckers > 0)
                {
                    _fieldsToDraw[i] = new Field();
                    _fieldsToDraw[i].WhiteCheckers = board.Fields[i].WhiteCheckers;
                }
                else
                {
                    _fieldsToDraw[i] = new Field();
                }
            }

            // Header
            for (int i = 13; i < 26; i++)
            {
                if (center && i == 19)
                {
                    Console.Write("| " + i + " ");
                    center = false;
                }
                else if (outZone && i == 25)
                {
                    Console.Write("| ");
                    Console.Write("25 | ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Checkers = " + board.Fields[i].WhiteCheckers + " ");
                    Console.ResetColor();
                    outZone = false;

                }
                else
                {
                    Console.Write(i + " ");
                }
            }

            //Upper field
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine("");
                center = true;
                outZone = true;
                for (int j = 13; j < 26; j++)
                {
                    if (center && j == 19)
                    {
                        Console.Write("| ");
                        center = false;
                    }

                    if (outZone && j == 25)
                    {
                        Console.Write("| ");
                        outZone = false;
                    }

                    if (_fieldsToDraw[j].WhiteCheckers > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("x");
                        Console.ResetColor();
                        _fieldsToDraw[j].WhiteCheckers--;
                    }
                    else if (_fieldsToDraw[j].RedCheckers > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("o");
                        Console.ResetColor();
                        _fieldsToDraw[j].RedCheckers--;
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.Write("  ");
                }
            }
            Console.WriteLine("");

            //lower field
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine("");
                center = true;
                outZone = true;

                for (int j = 12; j >= 0; j--)
                {
                    if (center && j == 6)
                    {
                        Console.Write("| ");
                        center = false;
                    }
                    if (outZone && j == 0)
                    {
                        Console.Write("| ");
                        outZone = false;
                    }
                    if (_fieldsToDraw[j].WhiteCheckers >= i)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("x");
                        Console.ResetColor();
                        _fieldsToDraw[j].WhiteCheckers--;
                    }
                    else if (_fieldsToDraw[j].RedCheckers >= i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("o");
                        Console.ResetColor();
                        _fieldsToDraw[j].RedCheckers--;
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.Write("  ");
                }
            }
            Console.WriteLine("");

            //footer
            center = true;
            outZone = true;
            for (int i = 12; i >= 0; i--)
            {
                if (center && i == 6)
                {
                    Console.Write("| ");
                    center = false;
                }
                else if (outZone && i == 0)
                {
                    Console.Write("| ");
                    Console.Write("00 | ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Checkers = "+board.Fields[i].RedCheckers + " ");
                    Console.ResetColor();
                    outZone = false;
                }
                if (i < 10 && i != 0)
                {
                    Console.Write("0" + i + " ");

                }
                else
                {
                    if (i != 0)
                    {
                        Console.Write(i + " ");
                    }
                }
            }
            Console.WriteLine("");


            if (board.Fields[26].WhiteCheckers > 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("White ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Checkers in Bar: {board.Fields[26].WhiteCheckers}");
                Console.ResetColor();
            }

            if (board.Fields[26].RedCheckers > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Red ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Checkers in Bar: {board.Fields[26].RedCheckers}");
                Console.ResetColor();
            }
        }
    }
}
