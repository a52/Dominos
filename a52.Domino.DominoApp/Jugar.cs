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

            Domain.Model.Player player = new Domain.Model.Player();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Clear();


            ConsoleKeyInfo cki;
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;

            // Console.WriteLine("Press any combination of CTL, ALT, and SHIFT, and a console key.");
            Console.WriteLine("Press the Escape (Esc) key to quit: \n");
            do
            {
                ShowMenu();

                cki = Console.ReadKey();
                Console.Clear();

                switch (cki.Key)
                {
                    case ConsoleKey.F1:
                        if (player.IsActive)
                            DibujarMisFichas(player);
                        else Console.WriteLine("Favor seleccionar usuario ");
                        break;

                    case ConsoleKey.F2:
                        if (player.IsActive)
                            DibujarTablero();
                        else Console.WriteLine("Favor seleccionar usuario ");
                        break;


                    case ConsoleKey.F4:
                        player = SelectPlayer();
                        break;


                    default:
                        break;
                }

            } while (cki.Key != ConsoleKey.Escape);
        }


        public void DibujarTablero()
        {
            int icount = 0;
            foreach (var tab in game.Board.Tabs)
            {
                this.DisplayFicha(tab, icount * 7, 11);
                icount++;
            }
        }

        public void DibujarMisFichas(Domain.Model.Player player)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("-".PadRight(80, '-'));
            Console.SetCursorPosition(0, 9);
            Console.WriteLine("{0}{1}{0}", "".PadLeft(40 - (player.PlayerName.Length / 2)), player.PlayerName);
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("-".PadRight(80, '-'));


            int icount = 0;
            foreach (var tab in player.Tabs)
            {

                this.DisplayFicha(tab, icount * 7, 11);

                Console.SetCursorPosition(icount * 7, 15);
                if (tab.IsOnBoard)
                    Console.Write("  *  ");


                icount++;
            }

            Console.SetCursorPosition(0, 26);
            Console.WriteLine("-".PadRight(80, '-'));

        }

        private void DisplayFicha(Domain.Model.Tab tab, int left, int top)
        {

            Console.SetCursorPosition(left, top);
            Console.Write("_____");
            Console.SetCursorPosition(left, top + 1);
            Console.Write("| {0} |", tab.Up_Value);
            Console.SetCursorPosition(left, top + 2);
            Console.Write("| {0} |", tab.Down_Value);
            Console.SetCursorPosition(left, top + 3);
            Console.Write("|___|");

        }

        private void ShowMenu(int opt = 0)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("-".PadRight(80, '-'));

            Console.WriteLine(" -> F1 - Listar Mis fichas");
            Console.WriteLine(" -> F2 - Ver Tablero");
            Console.WriteLine(" -> F3 - Lista de jugadas");
            Console.WriteLine(" -> Esc - Salir");
            Console.WriteLine("-".PadRight(80, '-'));

        }


        private Domain.Model.Player SelectPlayer()
        {
            Console.WriteLine("Seleccione Usuario: ");
            for (int i = 0; i < game.Players.Count; i++)
                Console.WriteLine("{0} - {1}", i, game.Players[i].PlayerName);


            Domain.Model.Player pl = new Domain.Model.Player();

            do
            {
                ConsoleKeyInfo cki = Console.ReadKey();
                if (char.IsNumber(cki.KeyChar))
                    if (int.Parse(cki.KeyChar.ToString()) < game.Players.Count)
                        pl = game.Players[int.Parse(cki.KeyChar.ToString())];

                if (string.IsNullOrEmpty(pl.PlayerName))
                    Console.WriteLine("Usuario especificado es incorrecto.");

            } while (string.IsNullOrEmpty(pl.PlayerName));



            return pl;

        }


    }
}
