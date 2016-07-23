using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class GameManager
    {

        public GameManager()
        {
        }


        public Player AskPlayersForThrow(Dice dice, Player player1, Player player2)
        {
            Player activePlayer = WhoseTurn(player1, player2);
            activePlayer.ThrowDices(dice);
            return activePlayer;
        }


        public bool AskPlayersForMove(GameBoard gameBoard, Dice dice, Player player1, Player player2, int[] fromTo)
        {
            Player activePlayer = WhoseTurn(player1, player2);

            //if home not full
            if (activePlayer.CheckersAtHome < 15 && activePlayer.MoveFromHome == false)
            {
                if (activePlayer.IsCanMove(AvailableMoves(gameBoard, dice, activePlayer), fromTo))
                {
                    gameBoard.MakeMove(fromTo[0], fromTo[1], activePlayer, gameBoard);
                    ResetDices(activePlayer, dice, fromTo);
                    activePlayer.Moves--;
                    UpdateCheckersAtHome(gameBoard, activePlayer);
                    if (activePlayer.Moves == 0)
                    {
                        ChangePlayersTurn(player1, player2);
                    }
                    return true;
                }
                else
                {
                    if (activePlayer.Moves == 0)
                    {
                        ChangePlayersTurn(player1, player2);
                    }
                }
            }

            //if home full
            else
            {
                activePlayer.MoveFromHome = true;
                if (WinnerPlayer(gameBoard, player1, player2) == null)
                {
                    if (activePlayer.IsCanMove(AvailableMovesFromHome(gameBoard, dice, activePlayer), fromTo))
                    {
                        gameBoard.MakeMove(fromTo[0], fromTo[1], activePlayer, gameBoard);
                        ResetDicesAtHome(activePlayer, dice, fromTo);
                        activePlayer.Moves--;
                        UpdateCheckersAtHome(gameBoard, activePlayer);
                        if (activePlayer.Moves == 0)
                        {
                            ChangePlayersTurn(player1, player2);
                        }
                        return true;
                    }
                }
            }
            return false;
        }


        public void ResetDices(Player player, Dice dice, int[] moves)
        {
            if (player.Color.Equals("White"))
            {
                if (moves[1] - moves[0] == dice.FirstCube && player.Moves < 3)
                {
                    dice.ResetFirstCube();
                }
                else if (moves[1] - moves[0] == dice.SecondCube && player.Moves < 3)
                {
                    dice.ResetSecondCube();
                }
            }
            else
            {
                if (moves[0] - moves[1] == dice.FirstCube && player.Moves < 3)
                {
                    dice.ResetFirstCube();
                }
                else if (moves[0] - moves[1] == dice.SecondCube && player.Moves < 3)
                {
                    dice.ResetSecondCube();
                }
            }
        }


        public void ResetDicesAtHome(Player player, Dice dice, int[] moves)
        {
            if (player.Color.Equals("White"))
            {
                if (moves[1] - moves[0] == dice.FirstCube && player.Moves < 3)
                {
                    dice.ResetFirstCube();
                }
                else if (moves[1] - moves[0] == dice.SecondCube && player.Moves < 3)
                {
                    dice.ResetSecondCube();
                }
                else
                {
                    if (!dice.RolledDouble)
                    {
                        if (dice.FirstCube > dice.SecondCube)
                        {
                            dice.ResetFirstCube();
                        }
                        else
                        {
                            dice.ResetSecondCube();
                        }
                    }
                }
            }
            else
            {
                if (moves[0] - moves[1] == dice.FirstCube && player.Moves < 3)
                {
                    dice.ResetFirstCube();
                }
                else if (moves[0] - moves[1] == dice.SecondCube && player.Moves < 3)
                {
                    dice.ResetSecondCube();
                }
            }
        }

        public void HumanPlayerMove(Player player)
        {

        }


        public void AIPlayerMove(Player player)
        {

        }


        public Dice SetPlayersColor(Dice dice, Player player1, Player player2)
        {
            do
            {
                dice.Throw();
            } while (dice.RolledDouble);

            if (dice.FirstCube > dice.SecondCube)
            {
                player1.Color = "White";
                player2.Color = "Red";
                player1.ChangeTurn();
            }
            else
            {
                player1.Color = "Red";
                player2.Color = "White";
                player2.ChangeTurn();
            }
            return dice;
        }


        public Player WinnerPlayer(GameBoard gameboard, Player player1, Player player2)
        {
            if (gameboard.Fields[0].RedCheckers == 15)
            {
                return player1;
            }
            else if (gameboard.Fields[25].WhiteCheckers == 15)
            {
                return player2;
            }
            else
            {
                return null;
            }
        }


        public bool isAvailableMoves(GameBoard gameBoard, Dice dice, Player player1, Player player2)
        {
            Player tempPlayer = WhoseTurn(player1, player2);
            List<KeyValuePair<int, int>> ListOfMoves = AvailableMoves(gameBoard, dice, tempPlayer) as List<KeyValuePair<int, int>>;
            ListOfMoves.AddRange(AvailableMovesFromCenter(gameBoard, dice, tempPlayer));
            if (ListOfMoves.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public IEnumerable<KeyValuePair<int, int>> AvailableMoves(GameBoard gameBoard, Dice dice, Player player)
        {
            List<KeyValuePair<int, int>> ListOfMoves = new List<KeyValuePair<int, int>>();
            if (player.Color.Equals("White"))
            {
                if (gameBoard.Fields[26].WhiteCheckers == 0)
                {
                    for (int i = 1; i < gameBoard.Fields.Length; i++)
                    {
                        if (!dice.RolledDouble)
                        {
                            if (i + dice.FirstCube <= 24 && dice.FirstCube != 0)
                            {
                                if (IsPlayerCanMove(gameBoard, i, player) && IsFieldAvailable(gameBoard, i, i + dice.FirstCube, dice.FirstCube, player))
                                {
                                    ListOfMoves.Add(new KeyValuePair<int, int>(i, i + dice.FirstCube));
                                }
                            }
                        }
                        if (i + dice.SecondCube <= 24 && dice.SecondCube != 0)
                        {
                            if (IsPlayerCanMove(gameBoard, i, player) && IsFieldAvailable(gameBoard, i, i + dice.SecondCube, dice.SecondCube, player))
                            {
                                ListOfMoves.Add(new KeyValuePair<int, int>(i, i + dice.SecondCube));
                            }
                        }
                    }
                    return ListOfMoves;
                }
                else
                {
                    return AvailableMovesFromCenter(gameBoard, dice, player);
                }
            }
            else
            {
                if (gameBoard.Fields[26].RedCheckers == 0)
                {
                    for (int i = 1; i < gameBoard.Fields.Length; i++)
                    {
                        if (!dice.RolledDouble)
                        {
                            if (i - dice.FirstCube >= 1 && dice.FirstCube != 0)
                            {
                                if (IsPlayerCanMove(gameBoard, i, player) && IsFieldAvailable(gameBoard, i, i - dice.FirstCube, dice.FirstCube, player))
                                {
                                    ListOfMoves.Add(new KeyValuePair<int, int>(i, i - dice.FirstCube));
                                }
                            }
                        }
                        if (i - dice.SecondCube >= 1 && dice.SecondCube != 0)
                        {
                            if (IsPlayerCanMove(gameBoard, i, player) && IsFieldAvailable(gameBoard, i, i - dice.SecondCube, dice.SecondCube, player))
                            {
                                ListOfMoves.Add(new KeyValuePair<int, int>(i, i - dice.SecondCube));
                            }
                        }
                    }
                    return ListOfMoves;
                }
                else
                {
                    return AvailableMovesFromCenter(gameBoard, dice, player);
                }
            }
        }


        public bool IsPlayerCanMove(GameBoard gameBoard, int index, Player player)
        {
            if (player.Color.Equals("White"))
            {
                if (gameBoard.Fields[index].WhiteCheckers > 0 && gameBoard.Fields[26].WhiteCheckers == 0)
                {
                    return true;
                }
            }
            if (player.Color.Equals("Red"))
            {
                if (gameBoard.Fields[index].RedCheckers > 0 && gameBoard.Fields[26].RedCheckers == 0)
                {
                    return true;
                }
            }
            return false;
        }



        public bool IsFieldAvailable(GameBoard gameBoard, int fromIndex, int toIndex, int cubeNumber, Player player)// check if came from bar? -1...
        {
            if (player.Color.Equals("White"))
            {
                if (toIndex - fromIndex == cubeNumber)
                {
                    if (gameBoard.Fields[toIndex].RedCheckers < 2)
                    {
                        return true;
                    }
                }
            }
            if (player.Color.Equals("Red"))
            {
                if (fromIndex - toIndex == cubeNumber)
                {
                    if (gameBoard.Fields[toIndex].WhiteCheckers < 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        public IEnumerable<KeyValuePair<int, int>> AvailableMovesFromCenter(GameBoard gameBoard, Dice gameDice, Player player)
        {
            List<KeyValuePair<int, int>> ListOfMoves = new List<KeyValuePair<int, int>>();

            if (player.Color.Equals("White"))
            {
                if (gameDice.FirstCube != 0)
                {
                    if (IsFieldAvailable(gameBoard, 0, 0 + gameDice.FirstCube, gameDice.FirstCube, player))
                    {
                        ListOfMoves.Add(new KeyValuePair<int, int>(26, 0 + gameDice.FirstCube));
                    }
                }

                if (gameDice.SecondCube != 0)
                {
                    if (IsFieldAvailable(gameBoard, 0, 0 + gameDice.SecondCube, gameDice.SecondCube, player))
                    {
                        ListOfMoves.Add(new KeyValuePair<int, int>(26, 0 + gameDice.SecondCube));
                    }
                }
            }
            else
            {
                if (gameDice.FirstCube != 0)
                {
                    if (IsFieldAvailable(gameBoard, 25, 25 - gameDice.FirstCube, gameDice.FirstCube, player))
                    {
                        ListOfMoves.Add(new KeyValuePair<int, int>(26, 25 - gameDice.FirstCube));
                    }
                }
                if (gameDice.SecondCube != 0)
                {
                    if (IsFieldAvailable(gameBoard, 25, 25 - gameDice.SecondCube, gameDice.SecondCube, player))
                    {
                        ListOfMoves.Add(new KeyValuePair<int, int>(26, 25 - gameDice.SecondCube));
                    }
                }
            }
            return ListOfMoves;
        }


        public IEnumerable<KeyValuePair<int, int>> AvailableMovesFromHome(GameBoard gameBoard, Dice gameDice, Player player)
        {
            List<KeyValuePair<int, int>> AvailableMovesList = new List<KeyValuePair<int, int>>();

            if (player.Color.Equals("White"))
            {
                for (int i = 19; i <= 24; i++)
                {
                    if (!gameDice.RolledDouble)
                    {
                        if (gameDice.FirstCube != 0)
                        {
                            if (IsPlayerCanMove(gameBoard, i, player) && IsLegalPlayerBearOffMove(i, gameDice.FirstCube, player, gameBoard))
                            {
                                AvailableMovesList.Add(new KeyValuePair<int, int>(i, 25));
                            }
                        }
                    }

                    if (gameDice.SecondCube != 0)
                    {
                        if (IsPlayerCanMove(gameBoard, i, player) && IsLegalPlayerBearOffMove(i, gameDice.SecondCube, player, gameBoard))
                        {
                            AvailableMovesList.Add(new KeyValuePair<int, int>(i, 25));
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i <= 6; i++)
                {
                    if (!gameDice.RolledDouble)
                    {
                        if (gameDice.FirstCube != 0)
                        {
                            if (IsPlayerCanMove(gameBoard, i, player) && IsLegalPlayerBearOffMove(i, gameDice.FirstCube, player, gameBoard))
                            {
                                AvailableMovesList.Add(new KeyValuePair<int, int>(i, 0));
                            }
                        }
                    }

                    if (gameDice.SecondCube != 0)
                    {
                        if (IsPlayerCanMove(gameBoard, i, player) && IsLegalPlayerBearOffMove(i, gameDice.SecondCube, player, gameBoard))
                        {
                            AvailableMovesList.Add(new KeyValuePair<int, int>(i, 0));
                        }
                    }
                }
            }
            AvailableMovesList.AddRange(AvailableMoves(gameBoard, gameDice, player));
            return AvailableMovesList;
        }



        public bool IsLegalPlayerBearOffMove(int fromIndex, int cubeNumber, Player player, GameBoard board)
        {
            if (player.Color.Equals("White"))
            {
                if (fromIndex + cubeNumber == 25)
                {
                    return true;
                }
                if (IsCheckerLeft(board, player, fromIndex, cubeNumber))
                {
                    return true;
                }
            }
            else
            {
                if (fromIndex - cubeNumber == 0)
                {
                    return true;
                }
                if (IsCheckerLeft(board, player, fromIndex, cubeNumber))
                {
                    return true;
                }
            }
            return false;
        }


        public bool IsCheckerLeft(GameBoard board, Player player, int from, int cubeNumber)
        {
            if (player.Color.Equals("White"))
            {
                for (int i = 19; i < from; i++)
                {
                    if (board.Fields[i].WhiteCheckers > 0)
                    {
                        return false;
                    }
                }

                if (from + cubeNumber >= 25)
                {
                    return true;
                }
            }
            else
            {
                for (int i = 6; i > from; i--)
                {
                    if (board.Fields[i].RedCheckers > 0)
                    {
                        return false;
                    }
                }

                if (from - cubeNumber <= 0)
                {
                    return true;
                }
            }
            return false;
        }


        public void UpdateCheckersAtHome(GameBoard gameBoard, Player player)
        {
            player.CheckersAtHome = 0;

            if (player.Color.Equals("White"))
            {
                for (int i = 19; i <= 24; i++)
                {
                    player.CheckersAtHome += gameBoard.Fields[i].WhiteCheckers;
                }
            }
            else
            {
                for (int i = 1; i <= 6; i++)
                {
                    player.CheckersAtHome += gameBoard.Fields[i].RedCheckers;
                }
            }
        }


        public void ChangePlayersTurn(Player player1, Player player2)
        {
            player1.ChangeTurn();
            player2.ChangeTurn();
        }


        public Player WhoseTurn(Player player1, Player player2)
        {
            if (player1.IsMyTurn)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }


        public void UpdateMoves(Dice dice, Player player)
        {
            if (dice.RolledDouble == true)
            {
                player.Moves = 4;
            }
            else
            {
                player.Moves = 2;
            }
        }

    }
}
