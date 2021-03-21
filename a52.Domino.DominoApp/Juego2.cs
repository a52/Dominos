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
	- F3 - Show Movement: show the state of the board. Movements, score, and current player
	- F4 - Show player's tokens: show all the tokens of the user
	- F5 - Make Movement: take a token of the current user and put it in the board.
	- F6 - Draw board: Show the board with all the positions 

    */
    class Juego2
    {
        Domain.Service.Game _game;
        Domain.Model.Player _player;

        public Juego2()
        {
            this._game = new Domain.Service.Game();
            // _game.PlayerWon += game_onPlayerWon;

            // _game.TokenPlayed += this.game_OnTokenPlayed;
        }

        public void Execute()
        {
            ConsoleKeyInfo ckf;
            do
            {
                ShowCurrentPlayer();
                ShowMenu();

                ckf = Console.ReadKey();
                try
                {

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


                        //- F3 - Show Movement: show the state of the board. Movements, score, and current player
                        case ConsoleKey.F3:
                            this.ShowMovements();

                            break;
                        //- F4 - Show player's tokens: show all the tokens of the user
                        case ConsoleKey.F4:
                            this.ShowPlayerTokens();
                            break;


                        //- F5 - Make Movement: take a token of the current user and put it in the board
                        case ConsoleKey.F5:
                            //this.MakeMovement();
                            this.MakeMovement2();
                            break;

                        // - F6 - Draw board: Show the board with all the positions 
                        case ConsoleKey.F6:
                            this.DrawBoard();
                            break;

                        default:
                            break;
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("".PadRight(30, '*'));
                    Console.WriteLine("Message: {0}", ex.Message);
                    Console.WriteLine("".PadRight(30, '*'));
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (ckf.Key != ConsoleKey.Escape);


            Console.WriteLine("Thank you for play. \nGood bye");
        }

        #region Private funtions

        /// <summary>
        /// - F1 - Start Game: deal and clean board
        /// </summary>
        private void StartGame()
        {
            Console.WriteLine("Starting new game.");
            _game = new Domain.Service.Game();
            _game.TokenPlayed += this.game_OnTokenPlayed;
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
                Console.WriteLine("List of players:");
                for (int i = 0; i < _game.Players.Count; i++)
                {
                    Console.WriteLine($"\t({i}) -> {_game.Players[i].PlayerName} ");
                }
                Console.Write("choose your player: ");
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

        /// <summary>
        /// //- F3 - Show Board: show the state of the board.Movements, score, and current player
        /// </summary>
        private void ShowMovements()
        {
            if (this._game.Board.MoveCount == 0)
                Console.WriteLine("There are not movement to show.");
            else
            {
                Console.WriteLine("List of movements");
                foreach (var item in this._game.Board.Movements)
                {
                    Console.WriteLine($"date: {item.Date} -> ({item.Index}) p: {item.CurrentPlayer} -> t:{item.CurrentToken} -> d:{item.CurrentDirection}");
                }


            }
        }

        /// <summary>
        /// //- F4 - Show player's tokens: show all the tokens of the user
        /// </summary>
        private void ShowPlayerTokens()
        {
            Console.WriteLine("Show token of the current player");

            ShowCurrentPlayer();

            foreach (var item in this._player.Tokens)
            {
                if (item.IsOnBoard)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"|{item.Up}|{item.Down}| -> On Board: {item.IsOnBoard}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// //- F5 - Make Movement: take a token of the current user and put it in the board
        /// </summary>
        private void MakeMovement()
        {
            var done = false;
            do
            {
                Console.WriteLine("Make a move");

                ShowCurrentPlayer();

                var haveTokenToPlay = false;
                for (int i = 0; i < this._player.Tokens.Count; i++)
                {
                    var item = this._player.Tokens[i];

                    if (item.IsOnBoard)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (this._game.TokenCanBePlayed(item))
                            haveTokenToPlay = true;
                    }

                    Console.WriteLine($"\t({i}) -> |{item.Up}|{item.Down}| -> On Board: {item.IsOnBoard} ");
                }

                Console.ForegroundColor = ConsoleColor.White;

                if (!haveTokenToPlay)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("You do not have token to be played. ");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.WriteLine("\t(8) PASS");
                Console.WriteLine("\t(9) Go back to menu");

                Console.Write("Choose the token to play:");
                var opt = Console.ReadLine();

                switch (opt)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                        var token = this._player.Tokens[int.Parse(opt)];
                        this._game.Play(this._player, token);
                        done = true;
                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine($"Token {token} was played");
                        Console.ForegroundColor = ConsoleColor.White;

                        break;

                    case "8":
                        if (haveTokenToPlay)
                            Console.WriteLine("YOU HAVE TOKEN THAT CAN BE PLAYED.");
                        else
                        {
                            this._game.SetTheNextPlayer();
                            Console.WriteLine("A new player was set.");
                            done = true;
                        }

                        break;


                    case "9":
                        done = true;
                        break;

                    default:
                        Console.WriteLine("Wrong selection");
                        break;
                }

            } while (!done);


        }

        private void MakeMovement2()
        {
            this.MakeMovement();
            this._player = this._game.currentPlayer;
            this.ShowMovements();
            this.DrawBoard();
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
            Console.WriteLine(" -F3 - Show Movements");
            Console.WriteLine(" -F4 - Show player's tokens");
            Console.WriteLine(" -F5 - Make Movement");
            Console.WriteLine(" -F6 - Draw Board");
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
                Console.WriteLine($"Board Values -> up: {_game.Board.UpValue} - down: {_game.Board.DownValue} ");
            }

            if (_game.Winner != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The Winner is {_game.Winner}. With a Score of {_game.Winner.Score}");
                Console.ForegroundColor = ConsoleColor.White;
            }


            Console.WriteLine("");
        }

        private void DrawBoard()
        {
            var b = this._game.Board;

            int line = 0;

            Console.Write($"\tline: {line:00} -> ");
            for (int c = 0; c < b.LenghtY; c++)
                Console.Write($"    {c + 1}    -");
            Console.WriteLine("");

            for (int x = 0; x < b.LenghtX; x++)
            {
                line++;
                Console.Write($"\tline: {line:00} -> ");

                for (int y = 0; y < b.LenghtY; y++)
                {
                    var p = b.Positions[x, y];

                    //Console.Write($" {x:000},{y:000} -");
                    if (p is null)
                        Console.Write($"    :    -");
                    else
                    {
                        var t = p.GetToken();
                        // Console.Write($"  {p.X:00}:{p.Y:00}  -");
                        Console.Write($"  {t.Up:00}:{t.Down:00}  -");
                    }
                }

                Console.WriteLine("");
            }


        }


        void game_onPlayerWon(EventArgs e)
        {
            Console.WriteLine("\n\t\tTENEMOS UN GANADOR\n");
        }

        void game_OnTokenPlayed(object source, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("...UNA FICHA FUE MOVIDA...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion
    }
}
