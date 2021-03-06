using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact
{
    public static class Player
    {
        public static float balance = 100f;
        public static Inventory inventory = new Inventory();
        
        public static string DisplayInventory()
        {
            string tempStr = "Player Inventory:\n\n";

            foreach(Item item in inventory.record)
            {
                tempStr += $"{item.name} \n£{item.value} x {item.quantity}\n{item.description}\n\n";
            }
            
            return tempStr;
        }
    }
}
