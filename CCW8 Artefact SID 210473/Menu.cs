using System;

namespace Artefact
{
    public static class Menu
    {
        private static int SelectedIndex;
        private static string[] Options;
        private static string Prompt;

        private static void DisplayOptions()
        {
            Utils.WriteLineAdvanced(Prompt, true, false);

            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string[] prefix = new string[] { " ", " " };

                if (i == SelectedIndex)
                {
                    prefix[0] = "---|>";
                    prefix[1] = "<|---";
                    Console.ForegroundColor = ConsoleColor.Green;
                    //BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix[0] = " ";
                    prefix[1] = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    //BackgroundColor = ConsoleColor.Black;
                }

                Utils.WriteLineAdvanced($"{currentOption}", true, false);
            }
            Console.ResetColor();
        }
        public static int Display(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;

            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                //Update SelectedIndex based on arrow keys
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
