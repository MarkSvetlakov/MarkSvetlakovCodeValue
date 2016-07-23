using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class Game
    {
        public bool IsNewGame { get; private set; }
        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }
        public GameManager GManager { get; private set; }
        public DrawToConsole ConsoleDraw { get; private set; }
        public Dice GameDice { get; private set; }
        public GameBoard Board { get; private set; }
        public bool isEndOfGame = false;

        public Game()
        {
            IsNewGame = true;
        }


        public void StartGame()
        {
            IsNewGame = true;
            GManager = new GameManager();
            ConsoleDraw = new DrawToConsole();
            GameDice = new Dice();
            Board = new GameBoard();

            if (IsNewGame)
            {
                InitializePlayers();
            }
            if (!isEndOfGame)
            {
                GameCycle();
            }
        }
        
        public void InitializePlayers()
        {
            int option = ConsoleDraw.DrawStartMenu();

            if (option == 1)
            {
                FirstPlayer = new Player(ConsoleDraw.DrawAskHumanName("Player 1"), true);
                SecondPlayer = new Player(ConsoleDraw.DrawAskHumanName("Player 2"), true);
                GManager.SetPlayersColor(GameDice, FirstPlayer, SecondPlayer);
                ConsoleDraw.DrawFirstThrow(GameDice, FirstPlayer, SecondPlayer);
                IsNewGame = false;
            }
            else
            {
                isEndOfGame = true;
            }
            
        }
        
        
        public void GameCycle()
        {
            bool isPlayerCanMove = true;
            int option = 0;
            while (!isWinner())
            {
                Player ActivePlayer = GManager.WhoseTurn(FirstPlayer, SecondPlayer);

                ConsoleDraw.DrawAskToThrow(GManager.AskPlayersForThrow(GameDice, FirstPlayer, SecondPlayer),
                    GameDice, Board);

                GManager.UpdateMoves(GameDice, ActivePlayer);
                isPlayerCanMove = GManager.isAvailableMoves(Board, GameDice, FirstPlayer, SecondPlayer);

                if (isPlayerCanMove)
                {
                    while (ActivePlayer.Moves > 0)
                    {
                        if (isWinner())
                        {
                            break;
                        }
                        else
                        {
                            GManager.AskPlayersForMove(Board, GameDice, FirstPlayer, SecondPlayer, 
                                ConsoleDraw.DrawAskToMove(GManager.WhoseTurn(FirstPlayer, SecondPlayer), 
                                GameDice, Board));
                        }
                    }
                }
                else
                {
                    ConsoleDraw.DrawNoAvailableMoves(GManager.WhoseTurn(FirstPlayer, SecondPlayer), Board, GameDice);
                    GManager.ChangePlayersTurn(FirstPlayer, SecondPlayer);
                } 
            }

            option = ConsoleDraw.DrawWinner(GManager.WinnerPlayer(Board, FirstPlayer, SecondPlayer));
            if (option == 1)
            {
                StartGame();
            }
            else
            {
                ConsoleDraw.DrawGoodBye();
            }
        } 


        private bool isWinner()
        {
            if (GManager.WinnerPlayer(Board, FirstPlayer, SecondPlayer) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
