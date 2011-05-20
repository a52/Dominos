using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.DominoApp
{

    class Jugar
    {

        Domain.Service.Game game = new Domain.Service.Game();

        public Jugar()
        {
            game.Deal();

        }

        public void Execute()
        {
            DibujarMisFichas(game.Players.First());
        }


        public void DibujarTablero()
        {
            foreach (var tab in game.Board.Tabs)
            {

            }
        }

        public void DibujarMisFichas(Domain.Model.Player player)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 19);
            Console.WriteLine("-".PadRight(80, '-'));
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("{0}{1}{0}", "".PadLeft(40 - (player.PlayerName.Length / 2)), player.PlayerName);
            Console.SetCursorPosition(0, 21);
            Console.WriteLine("-".PadRight(80, '-'));


            int icount = 0;
            foreach (var tab in player.Tabs)
            {
                Console.SetCursorPosition(icount * 7, 22);
                Console.Write("_____");
                Console.SetCursorPosition(icount * 7, 23);
                Console.Write("| {0} |", tab.Up_Value);
                Console.SetCursorPosition(icount * 7, 24);
                Console.Write("| {0} |", tab.Down_Value);
                Console.SetCursorPosition(icount * 7, 25);
                Console.Write("|___|");
                
                icount++;
            }

            Console.SetCursorPosition(0, 26);
            Console.WriteLine("-".PadRight(80, '-'));

        }



    }
}
