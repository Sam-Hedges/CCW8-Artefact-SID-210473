using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact
{
    public static class Shop
    {
        private static List<Item> sellableItems = new List<Item>
        {
            new Item("Salt", "Used to season food", 2, 10),
            new Item("Sugar", "", 3, 10),
            new Item("Water", "", 2, 20),
            new Item("Oil", "", 3, 5),
            new Item("Milk", "", 4, 3),
            new Item("Cereal", "", 2, 5),
            new Item("Flour", "", 1, 5),
            new Item("Onion", "", 3, 10),
            new Item("Garlic", "", 2, 5),
            new Item("Eggs", "", 5, 12),
            new Item("Pepper", "", 1, 10),
            new Item("Vinegar", "", 3, 5),
            new Item("Spices", "", 2, 20),
            new Item("Mustard", "", 4, 1),
            new Item("Ketchup", "", 4, 1)
        };
        private static Inventory stock;
        public static Inventory playerBasket;
        private static bool stockInitialised = false;

        public static void BrowseShop()
        {
            if (!stockInitialised) { InitialiseStock(); }

            int src = stock.record.Count;
            string[] options = new string[src]; 

            for (int i = 0; i < src; i++)
            {
                options[i] = 
                    @$"Name: {stock.record[i].name} Quantity: {stock.record[i].quantity} 
Value: {stock.record[i].value}\nDescription: {stock.record[i].description}\n\n";
            }

            Menu.Display("Shop", options);
        }

        private static void InitialiseStock()
        {
            stockInitialised = true;

            Random rnd = new Random();
            int stockAmount = rnd.Next(5, sellableItems.Count);

            for (int i = 0; i <= stockAmount; i++)
            {
                int tempItemIndex = rnd.Next(0, sellableItems.Count);

                Item currentItem = sellableItems[tempItemIndex];
                int addQuantity = rnd.Next(1, currentItem.maxStackQuantity);

                stock.AddItem(currentItem, addQuantity);
            }
        }

        private static float BasketValue()
        {
            float tempVal = 0;

            foreach (Item item in playerBasket.record)
            {
                tempVal += item.value;
            }

            return tempVal;
        }

        private static void GivePlayerPurchasedItems()
        {
            foreach(Item item in playerBasket.record)
            {
                Player.inventory.AddItem(item, item.quantity);
            }
        }

        public static bool Checkout()
        {
            if (Player.balance >= BasketValue())
            {
                stockInitialised = false;
                GivePlayerPurchasedItems();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
