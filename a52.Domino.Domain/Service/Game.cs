using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Service
{
    public class Game
    {

        public List<Model.Player> Players { get; set; }

        List<Model.Tab> tabs;
        public Model.Board Board { get; set; }

        public Game()
        {
            /// Generar Fichas
            tabs = new List<Model.Tab>();
            for (int up = 0; up <= 6; up++) for (int down = 0; down <= 6; down++)
                    if (up <= down) tabs.Add(new Model.Tab() { Up_Value = up, Down_Value = down, IsOnBoard = false, Position = 0 });

            /// Creacion de tablero
            Board = new Model.Board();


            /// Creacion de jugadores
            this.Players = new List<Model.Player>();
            for (int i = 1; i < 5; i++)
                this.Players.Add(new Model.Player() { PlayerName = string.Format("Jugador {0}", i), IsActive = true, IsMachine = true });
        }

        public void Deal()
        {
            this.Board = new Model.Board();
            

            foreach (var tab in tabs) tab.IsAssigned = false;
            var q = from p in tabs where p.IsAssigned == false orderby Guid.NewGuid() select p;
            int iCount = 0;
            /// Asignar fichas al primer jugador
            foreach (var player in this.Players)
            {
                player.Tabs = new List<Model.Tab>();
                foreach (var tab in q.Take(7))
                {
                    player.Tabs.Add(tab);
                    tab.IsAssigned = true;
                }
                iCount++;

            }

        }
        






    }
}
