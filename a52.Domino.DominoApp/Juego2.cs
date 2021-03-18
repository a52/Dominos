using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.DominoApp
{

    /*
        Console application
        - F1 - Start Game: deal and clean board
        - F2 - Choose Player: select player to make movement
        - F3 - Show Board: show the state of the board.Movements, score, and current player
        - F4 - Show player's tokens: show all the tokens of the user
        - F5 - Make Movement: take a token of the current user and put it in the board
    */
    class Juego2
    {
        Domain.Service.Game _game;
        Domain.Model.Player _player;

        public Juego2()
        {
            this._game = new Domain.Service.Game();

        }

        public void Execute()
        {
            ConsoleKeyInfo ckf;
            do
            {
                ShowCurrentPlayer();
                ShowMenu();

                ckf = Console.ReadKey();

                Console.WriteLine("");
                #region Opciones seleccionadas
                switch (ckf.Key)
                {
                    //-F1 - Start Game: deal and clean board
                    case ConsoleKey.F1:
                        StartGame();
                        break;

                    // -F2 - Choose Player: select player to make movement
                    case ConsoleKey.F2:
                        this.ChoosePlayer();

                        break;



                    //- F3 - Show Board: show the state of the board.Movements, score, and current player
                    //- F4 - Show player's tokens: show all the tokens of the user
                    //- F5 - Make Movement: take a token of the current user and put it in the board

                    default:
                        break;
                }

                #endregion


            } while (ckf.Key != ConsoleKey.Escape);


            Console.WriteLine("gracias por jugar. \nAdios");
        }

        #region Private funtions

        /// <summary>
        /// - F1 - Start Game: deal and clean board
        /// </summary>
        private void StartGame()
        {
            Console.WriteLine("Starting new game.");
            _game = new Domain.Service.Game();
            _game.Deal();
            _player = null;
            Console.WriteLine("Game was dealed. now you can start.");

        }

        /// <summary>
        /// - F2 - Choose Player: select player to make movement
        /// </summary>
        private void ChoosePlayer()
        {
            var playerSelected = false;
            do
            {
                /// Show Current player
                this.ShowCurrentPlayer();
                /// Show all player by name
                Console.WriteLine("Listado de jugadores:");
                for (int i = 0; i < _game.Players.Count; i++)
                {
                    Console.WriteLine($"\t({i}) -> {_game.Players[i].PlayerName} ");
                }
                Console.Write("Seleccion: ");
                var opt = Console.ReadLine();
                switch (opt)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                        _player = _game.Players[int.Parse(opt)];
                        Console.WriteLine($"\n{_player} selected.");
                        playerSelected = true;
                        break;

                    default:
                        Console.WriteLine("Wrong selection.\n");
                        break;
                }
            } while (!playerSelected);
            /// choose player
            /// show player selected
        }


        #endregion

        #region internal functions

        /// <summary>
        /// Options of the game
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("".PadRight(30, '*'));
            Console.WriteLine(" -F1 - Start Game ");
            Console.WriteLine(" -F2 - Choose Player");
            Console.WriteLine(" -F3 - Show Board");
            Console.WriteLine(" -F4 - Show player's tokens");
            Console.WriteLine(" -F5 - Make Movement");
            Console.WriteLine(" -ESC - SALIR ");
            Console.WriteLine("".PadRight(30, '*'));

        }

        /// <summary>
        /// Show info of current players
        /// </summary>
        private void ShowCurrentPlayer()
        {
            Console.WriteLine("");

            if (_player == null)
                Console.WriteLine("No player selected");
            else
            {
                Console.WriteLine($"You are: {_player}");
            }
            if (_game.currentPlayer == null)
                Console.WriteLine("No active player in the game");
            else
            {
                Console.WriteLine($"The active player is: {_game.currentPlayer}");
            }
            Console.WriteLine("");
        }


        #endregion
    }
}
