using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Service
{
    public class Game
    {

        public List<Model.Player> Players { get; set; }

        private Model.Player currentPlayer;

        List<Model.Token> tabs;
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
                this.Players.Add(new Model.Player() { PlayerName = string.Format("Jugador {0}", i), IsActive = true, IsMachine = true });
        }

        /// <summary>
        /// Distributes the token between the four players
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

        public bool PlayTab(Model.Player player, Model.Token tab, Model.Direction direction = Model.Direction.Auto)
        {
            var result = false;

            if (!tab.IsOnBoard)
                /// validar si es el jugar correcto
                if (this.IsRightPlayer(player))
                {
                    /// validar si la ficha pertenece al jugador
                    if (player.Tabs.Contains(tab))
                    {
                        this.Board.AddTab(tab, direction);
                        /// validar si la ficha tiene los valores de lugar
                        tab.IsOnBoard = true;
                    }
                    else System.Diagnostics.Debug.WriteLine($"The tab {tab.ToString()} is not from the player {player.PlayerName}");
                }


            return result;
        }


        public bool IsRightPlayer(Model.Player player)
        {
            var result = false;
            /// Validar si el board tiene fichas
            if (this.Board.Items.Count.Equals(0))
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







    }
}
