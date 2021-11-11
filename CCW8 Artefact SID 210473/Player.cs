using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact
{
    public static class Player
    {
        public static float balance = 100f;
        public static Inventory inventory;
        
        public static string DisplayInventory()
        {
            string tempStr = string.Empty;

            foreach(Item item in inventory.record)
            {
                tempStr += $"Name: {item.name} Quantity: {item.quantity} Value: {item.value}\nDescription: {item.description}\n\n";
            }
            
            return tempStr;
        }
    }
}
