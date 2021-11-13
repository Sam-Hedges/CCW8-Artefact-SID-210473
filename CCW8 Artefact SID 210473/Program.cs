using System;

namespace Artefact
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string prompt = "Hello World!";
                string[] options = { "Shop", "Player", "Exit" };
                int selectedIndex = Menu.Display(prompt, options);

                switch (selectedIndex)
                {
                    case 0:
                        Shop.BrowseShop();
                        break;
                    case 1:
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
