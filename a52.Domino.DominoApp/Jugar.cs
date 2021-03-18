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
                // Console.Clear();

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


                    case ConsoleKey.F3:
                        player = SelectPlayer();
                        break;

                        
                    case ConsoleKey.F4: /// do a move
                        DoAMove(player);
                        break;

                    case ConsoleKey.F12:
                        testPlayers(player);
                        break;

                    default:
                        break;
                }

            } while (cki.Key != ConsoleKey.Escape);
        }

        public void DibujarTablero()
        {
            int icount = 0;
            foreach (var tab in game.Board.Movements)
            {
                this.DisplayFicha(tab.CurrentToken, icount * 7, 11);
                icount++;
            }
        }

        public void DibujarMisFichas(Domain.Model.Player player)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.WriteLine("-".PadRight(80, '-'));
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.WriteLine("{0}{1}{0}", "".PadLeft(40 - (player.PlayerName.Length / 2)), player.PlayerName);
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.WriteLine("-".PadRight(80, '-'));

            int currentTop = Console.CursorTop;
            int icount = 0;
            foreach (var tab in player.Tabs)
            {

                this.DisplayFicha(tab, icount * 7, currentTop);

                Console.SetCursorPosition(icount * 7, currentTop + 5);
                if (tab.IsOnBoard)
                    Console.Write("  *  ");
                else Console.WriteLine($" [{icount}]  ");

                icount++;
            }

            Console.SetCursorPosition(0, currentTop + 5);
            Console.WriteLine("-".PadRight(80, '-'));

        }

        private void DisplayFicha(Domain.Model.Token tab, int left, int top)
        {

            Console.SetCursorPosition(left, top);
            Console.Write("_____");
            Console.SetCursorPosition(left, top + 1);
            Console.Write("| {0} |", tab.Up_Value);
            Console.SetCursorPosition(left, top + 2);
            Console.Write("|---|");
            Console.SetCursorPosition(left, top + 3);
            Console.Write("| {0} |", tab.Down_Value);
            Console.SetCursorPosition(left, top + 4);
            Console.Write("|___|");


        }

        private void ShowMenu(int opt = 0)
        {
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.WriteLine("-".PadRight(80, '-'));
            Console.WriteLine(" -> F1 - Listar Mis fichas");
            Console.WriteLine(" -> F2 - Ver Tablero");
            Console.WriteLine(" -> F3 - Lista de jugadas");
            Console.WriteLine(" -> F4 - Jugar ");
            Console.WriteLine(" -> Esc - Salir");
            Console.WriteLine("-".PadRight(80, '-'));

        }

        #region Opciones

        public void DoAMove(Domain.Model.Player player)
        {
            /// Display tabs
            this.DibujarTablero();
            this.DibujarMisFichas(player);

            /// Display options to choose
            
            
            /// Chose one
            /// Play

        }

        #endregion


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

        #region test functions

        private void testPlayers(Domain.Model.Player player)
        {
            /// Show users
            Console.WriteLine(player.PlayerName);


            /// Show menu
            /// Mostrar tablero
            /// Probar usuario
            Console.WriteLine("is the right Player: {0}", game.IsRightPlayer(player));


        }

        #endregion

    }
}
