using System;
using System.Runtime.InteropServices;

namespace Artefact
{
    class Program
    {
        #region Maximize Variables

        /********************************************************************
        *  Title: Maximizing console window - C#
        *  Author: Châu .N
        *  Authored: 2 Jan. 2016
        *  Online: Stack Overflow
        *  Link: https://stackoverflow.com/questions/22053112/maximizing-console-window-c-sharp/22053200
        *  Accessed: 15 Nov. 2021
        ********************************************************************/

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr UtilsConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;

        static void Fullscreen()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(UtilsConsole, MAXIMIZE);
        }

        #endregion

        public static string shopPromt = @"  _______ _     _                    _____ _                 
 |__   __| |   (_)                  / ____| |                
    | |  | |__  _ _ __   __ _ ___  | (___ | |_ ___  _ __ ___ 
    | |  | '_ \| | '_ \ / _` / __|  \___ \| __/ _ \| '__/ _ \
    | |  | | | | | | | | (_| \__ \  ____) | || (_) | | |  __/
    |_|  |_| |_|_|_| |_|\__, |___/ |_____/ \__\___/|_|  \___|
                         __/ |                               
                        |___/                                

";

        static void Main(string[] args)
        {

            Fullscreen();

            while (true)
            {
                string prompt = shopPromt;
                string[] options = { "Shop", "Player", "Exit" };
                int selectedIndex = Menu.Display(prompt, options);

                switch (selectedIndex)
                {
                    case 0:
                        StartShopping();
                        break;
                    case 1:
                        DisplayPlayerStats();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        static void DisplayPlayerStats()
        {
            Console.Clear();
            Utils.WriteLineAdvanced(Player.DisplayInventory(), true, false);
            Console.ReadLine();
        }

        static void StartShopping()
        {
            string prompt = shopPromt;
            string[] options = { "Buy", "Sell", "Back" };
            int selectedIndex = Menu.Display(prompt, options);

            switch (selectedIndex)
            {
                case 0:
                    Shop.BrowseShop();
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }

        }
    }
}
