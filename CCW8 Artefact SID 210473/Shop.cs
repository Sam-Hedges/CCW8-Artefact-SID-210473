using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact
{
    public static class Shop
    {
        private static List<Item> sellableItems = new List<Item>
        {
            new Item("Salt", "Your average gamer.", 2, 10),
            new Item("Sugar", "America's national mascot.", 3, 10),
            new Item("Water", "The opposite of fire.", 2, 20),
            new Item("Oil", "Used in massage.", 3, 5),
            new Item("Milk", "Drink what your mama gave you.", 4, 3),
            new Item("Cereal", "A type of soup.", 2, 5),
            new Item("Flour", "They sometimes grow.", 1, 5),
            new Item("Onion", "Instructions unclear, now crying.", 3, 10),
            new Item("Garlic", "Vampires are missing out.", 2, 5),
            new Item("Eggs", "What came first?", 5, 12),
            new Item("Pepper", "A world renowned doctor.", 1, 10),
            new Item("Vinegar", "Stop it! I’m pickle-ish.", 3, 5),
            new Item("Spices", "Beyond seasonable doubt.", 2, 20),
            new Item("Mustard", "Don't inhale in gaseous form.", 4, 1),
            new Item("Ketchup", "In Heinzsight, don't apply to eyes.", 4, 1)
        };
        private static Inventory stock = new Inventory();
        public static Inventory playerBasket = new Inventory();
        private static bool stockInitialised = false;

        public static void BrowseShop()
        {
            if (!stockInitialised) { InitialiseStock(); }

            while (true)
            {
                int src = stock.record.Count;
                string[] options = new string[src + 2];
                options[0] = "Checkout\n\n";
                options[1] = "Edit Basket\n\n";

                string prompt = PopulateBasketDisplay();          

                for (int i = 2; i < src + 2; i++)
                {
                    Item item = stock.record[i - 2];
                    options[i] = $"{item.Name} \n£{item.Value} x {item.Quantity}\n{item.Description}\n\n";
                }

                int selectedItemIndex = Menu.Display(prompt, options, false);

                switch (selectedItemIndex)
                {
                    case 0:
                        if (!Checkout())
                        {
                            Console.Clear();
                            Utils.WriteLineAdvanced("You do not have sufficient funds to purchase all the items in your basket.\nPlease remove something in order to checkout.");
                            Console.ReadLine();
                            break;
                        }
                        return;
                    case 1:
                        BrowseBasket();
                        break;
                    default:
                        AddToBasket(selectedItemIndex - 2);
                        break;
                }
            }
        }

        private static void BrowseBasket()
        {
            while (true)
            {
                int prc = playerBasket.record.Count;
                string[] options = new string[prc + 1];
                options[0] = "Return to store\n\n";

                string prompt = Program.shopPromt + $"Balance: £{Player.balance}\n\n" + $"Basket: £{BasketValue()}\n\n";

                for (int i = 1; i < prc + 1; i++)
                {
                    Item item = playerBasket.record[i - 1];
                    int itemQuantity = item.Quantity < 1 ? 1 : item.Quantity;
                    options[i] = $"{item.Name} x {item.Quantity} - £{itemQuantity * item.Value}\n";
                }

                int selectedItemIndex = Menu.Display(prompt, options, false);

                switch (selectedItemIndex)
                {
                    case 0:
                        return;
                    default:
                        RemoveFromBasket(selectedItemIndex - 1);
                        break;
                }
            }
        }

        private static string PopulateBasketDisplay()
        {
            string prompt = Program.shopPromt + $"Balance: £{Player.balance}\n\n" + $"Basket: £{BasketValue()}\n\n";
            foreach (Item item in playerBasket.record)
            {
                int itemQuantity = item.Quantity < 1 ? 1 : item.Quantity;
                prompt += $"{item.Name} x {item.Quantity} - £{itemQuantity * item.Value}\n";
            }

            return prompt;
        }

        private static void AddToBasket(int selectedItemIndex)
        {
            Item currentItem = stock.record[selectedItemIndex];

            playerBasket.AddItem(currentItem, 1);
            stock.RemoveItem(currentItem, 1);
        }

        private static void RemoveFromBasket(int selectedItemIndex)
        {
            Item currentItem = playerBasket.record[selectedItemIndex];

            stock.AddItem(currentItem, 1);
            playerBasket.RemoveItem(currentItem, 1);      
        }

        private static void InitialiseStock()
        {
            stockInitialised = true;

            Random rnd = new Random();
            int stockAmount = rnd.Next(5, 10);

            for (int i = 0; i <= stockAmount; i++)
            {
                int tempItemIndex;

                do
                {
                    tempItemIndex = rnd.Next(0, sellableItems.Count);
                }
                while (stock.record.Contains(sellableItems[tempItemIndex]));

                Item currentItem = sellableItems[tempItemIndex];
                int addQuantity = rnd.Next(1, currentItem.MaxStackQuantity);

                stock.AddItem(currentItem, addQuantity);
            }
        }

        private static float BasketValue()
        {
            float tempVal = 0;

            foreach (Item item in playerBasket.record)
            {
                tempVal += item.Value * item.Quantity;
            }

            return tempVal;
        }

        private static void GivePlayerPurchasedItems()
        {
            foreach(Item item in playerBasket.record)
            {
                Player.inventory.AddItem(item, item.Quantity);
            }
        }

        private static bool Checkout()
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
