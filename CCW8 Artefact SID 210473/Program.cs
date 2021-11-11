using System;

namespace Artefact
{
    class Program
    {
        static void Main(string[] args)
        {
            string prompt = "Hello World!";
            string[] options = { "Shop", "Player", "Exit" };
            int selectedIndex = Menu.Display(prompt, options);

        }
    }
}
