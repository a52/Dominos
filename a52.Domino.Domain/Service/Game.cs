﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Service
{
    /*
     * 
Game
	- Board: the board where is playing
	- Player[]: array of four player
	
	+ Deal(): iniciate a game
	+ Play(player, tab, direction): make a movement in the board

     * */
    public class Game
    {

        /// <summary>
        /// - Player[]: array of four player
        /// </summary>
        public List<Model.Player> Players { get; set; }

        public Model.Player currentPlayer { get; private set; }


        List<Model.Token> tabs;

        /// <summary>
        /// - Board: the board where is playing
        /// </summary>  
        public Model.Board Board { get; set; }

        /// <summary>
        /// Start the game
        /// The token are generated 
        /// </summary>
        public Game()
        {
            /// Generar Fichas
            tabs = new List<Model.Token>();
            for (int up = 0; up <= 6; up++) for (int down = 0; down <= 6; down++)
                    if (up <= down) tabs.Add(new Model.Token() { Up_Value = up, Down_Value = down, IsOnBoard = false });

            /// Creacion de tablero
            Board = new Model.Board();


            /// Creacion de jugadores
            this.Players = new List<Model.Player>();
            for (int i = 1; i < 5; i++)
                this.Players.Add(new Model.Player() { PlayerName = string.Format("Jugador {0}", i), IsActive = true, IsMachine = true, Index = i });
        }



        /// <summary>
        /// Distributes the token between the four players
        /// + Deal(): iniciate a game
        /// </summary>
        public void Deal()
        {
            this.Board = new Model.Board();


            foreach (var tab in tabs) tab.IsAssigned = false;
            var q = from p in tabs where p.IsAssigned == false orderby Guid.NewGuid() select p;
            int iCount = 0;
            /// Asignar fichas al primer jugador
            foreach (var player in this.Players)
            {
                player.Tabs = new List<Model.Token>();
                foreach (var tab in q.Take(7))
                {
                    player.Tabs.Add(tab);
                    tab.IsAssigned = true;
                }
                iCount++;

            }

            currentPlayer = Players.FirstOrDefault();

        }


        /// <summary>
        /// + Play(player, tab, direction): make a movement in the board
        /// </summary>
        /// <param name="player"></param>
        /// <param name="token"></param>
        /// <param name="direction"></param>
        public void Play(Model.Player player, Model.Token token, Model.Direction direction = Model.Direction.Auto)
        {
            /// validate token
            if (token.IsOnBoard)
                throw new Exception("the token had been played");
            /// validate player
            if (player != this.currentPlayer)
                throw new Exception($"It isn't the turn of {player.PlayerName}. The player who have to play is {this.currentPlayer.PlayerName}.");
            /// validate token in the player
            if (!player.Tabs.Contains(token))
                throw new Exception($"The token {token} isn't for the player {player}. Please review");

            /// create movement
            var m = new Model.Movement();
            m.CurrentDirection = direction;
            m.CurrentPlayer = player;
            m.CurrentToken = token;
            m.Date = DateTime.Now;
            m.Index = this.Board.MoveCount + 1;

            /// add movement
            Board.Move(m);

            /// Assign the next player
            this.currentPlayer = this.GetNextPlayer();
        }

        /// <summary>
        /// validate is the player is current player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsRightPlayer(Model.Player player)
        {
            var result = false;
            /// Validar si el board tiene fichas
            if (this.Board.MoveCount.Equals(0))
            {
                foreach (var item in player.Tabs)
                    if (item.Up_Value.Equals(6))
                        if (item.Down_Value.Equals(6))
                        {
                            result = true;
                            break;
                        }
            }
            else
                result = currentPlayer == player;
            return result;
        }

        /// <summary>
        ///  Identify the next player
        /// </summary>
        /// <returns></returns>
        private Model.Player GetNextPlayer()
        {
            Model.Player result = null;
            int nextIndex = 0;
            nextIndex = 4 - this.currentPlayer.Index;
            result = this.Players.Where(x => x.Index.Equals(nextIndex)).FirstOrDefault();

            return result;
        }



    }
}
