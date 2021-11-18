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
            new Item("Spices", "If you wanna be my lover.", 2, 20),
            new Item("Mustard", "Don't inhale in gaseous form.", 4, 1),
            new Item("Ketchup", "In Heinzsight, don't apply to eyes.", 4, 1)
        };
        private static Inventory stock = new Inventory();
        public static Inventory playerBasket = new Inventory();
        private static bool stockInitialised = false;

        public static void BrowseSale()
        {
            while (true)
            {
                int pirc = Player.inventory.record.Count;
                string[] options = new string[pirc + 2];
                options[0] = "Checkout\n\n";
                options[1] = "Edit Basket\n\n";

                string prompt = Program.shopPromt + $"Balance: £{Player.balance}\n\n" + $"Sale Basket: +£{(float)Math.Round(BasketValue() * 0.9f, 2)}\n\n";

                for (int i = 2; i < pirc + 2; i++)
                {
                    Item item = Player.inventory.record[i - 2];
                    int itemQuantity = item.quantity < 1 ? 1 : item.quantity;
                    float price = itemQuantity * item.value;
                    float resalePrice = (float)Math.Round(price * 0.9f, 2);
                    options[i] = $"{item.name} x {item.quantity}\nRetail Price: £{price}\nResale Price: £{resalePrice}\n\n";
                }

                int selectedItemIndex = Menu.Display(prompt, options, false);

                switch (selectedItemIndex)
                {
                    case 0:
                        Player.balance += (float)Math.Round(BasketValue() * 0.9f, 2);

                        CheckoutItems(playerBasket, stock);
                        return;
                    case 1:
                        BrowseBasket(false);
                        break;
                    default:
                        AddToBasket(playerBasket, Player.inventory, selectedItemIndex - 2);
                        break;
                }
            }
        }

        public static void BrowseShop()
        {
            if (!stockInitialised) { InitialiseStock(); }

            while (true)
            {
                int src = stock.record.Count;
                string[] options = new string[src + 2];
                options[0] = "Checkout\n\n";
                options[1] = "Edit Basket\n\n";

                string prompt = Program.shopPromt + $"Balance: £{Player.balance}\n\n" + $"Basket: £{BasketValue()}\n\n" + PopulateBasketDisplay();          

                for (int i = 2; i < src + 2; i++)
                {
                    Item item = stock.record[i - 2];
                    options[i] = $"{item.name} \n£{item.value} x {item.quantity}\n{item.description}\n\n";
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
                        AddToBasket(playerBasket, stock, selectedItemIndex - 2);
                        break;
                }
            }
        }

        private static void BrowseBasket(bool buying = true)
        {
            while (true)
            {
                int prc = playerBasket.record.Count;
                string[] options = new string[prc + 2];
                options[0] = "Return to store\n\n";
                options[1] = "Reset basket\n\n";

                string prompt = Program.shopPromt + $"Balance: £{Player.balance}\n\n" + $"Basket: £{BasketValue()}\n\n";

                for (int i = 2; i < prc + 2; i++)
                {
                    Item item = playerBasket.record[i - 2];
                    int itemQuantity = item.quantity < 1 ? 1 : item.quantity;
                    float price = itemQuantity * item.value;
                    float resalePrice = (float)Math.Round(price * 0.9f, 2);
                    if (buying) { options[i] = $"{item.name} x {item.quantity} - £{price}\n"; }
                    else { options[i] = $"{item.name} x {item.quantity}\nRetail Price: £{price}\nResale Price: £{resalePrice}\n\n"; }
                }

                int selectedItemIndex = Menu.Display(prompt, options, false);

                switch (selectedItemIndex)
                {
                    case 0:
                        return;
                    case 1:
                        int invSize = playerBasket.record.Count;
                        for (int i = 0; i < invSize; i++)
                        {
                            Item item = playerBasket.record[0];

                            if (buying) { RemoveFromBasket(stock, playerBasket, 0, item.quantity); }
                            else { RemoveFromBasket(Player.inventory, playerBasket, 0, item.quantity); }
                        }
                        return;
                    default:
                        if (buying) { RemoveFromBasket(stock, playerBasket, selectedItemIndex - 2); }
                        else { RemoveFromBasket(Player.inventory, playerBasket, selectedItemIndex - 2); }
                        break;
                }
            }
        }

        private static string PopulateBasketDisplay()
        {
            string prompt = string.Empty;
            foreach (Item item in playerBasket.record)
            {
                int itemQuantity = item.quantity < 1 ? 1 : item.quantity;
                prompt += $"{item.name} x {item.quantity} - £{itemQuantity * item.value}\n";
            }

            return prompt;
        }

        private static void AddToBasket(Inventory addTo, Inventory removeFrom, int selectedItemIndex, int quantity = 1)
        {
            Item currentItem = new Item(removeFrom.record[selectedItemIndex]);

            addTo.AddItem(currentItem, quantity);
            removeFrom.RemoveItem(currentItem, quantity);
        }

        private static void RemoveFromBasket(Inventory addTo, Inventory removeFrom, int selectedItemIndex, int quantity = 1)
        {
            Item currentItem = new Item(removeFrom.record[selectedItemIndex]);

            addTo.AddItem(currentItem, quantity);
            removeFrom.RemoveItem(currentItem, quantity);
        }

        /// <summary>
        /// Fills the Stock Inventory of the Shop randomly with Items from the sellableItems List
        /// </summary>
        private static void InitialiseStock()
        {
            stockInitialised = true; // Marks the shops' stock as now being initialized this method isn't repeated

            Random rnd = new Random(); // New random class used to generate randoms values for stock initialization
            int stockAmount = rnd.Next(5, 10); // Number of unique Items to be added to the stock

            for (int i = 0; i <= stockAmount; i++) // Iterates stockAmount number of times
            {
                int tempItemIndex;

                do
                {
                    tempItemIndex = rnd.Next(0, sellableItems.Count);
                }
                while (stock.record.Contains(sellableItems[tempItemIndex])); // Ensures only onr of each item is added to the stock

                Item currentItem = new Item(sellableItems[tempItemIndex]); // Makes a new copy of selected item
                int addQuantity = rnd.Next(1, currentItem.maxStackQuantity); // Generates a random quantity for the previous item

                stock.AddItem(currentItem, addQuantity); // The item gets added to the Stock Inventory
            }
        }

        /// <summary>
        /// Works out the total value of the playerBasket Inventory
        /// </summary>
        /// <returns>Total value of playerBasket Inventory</returns>
        private static float BasketValue()
        {
            float tempVal = 0;

            foreach (Item item in playerBasket.record)
            {
                tempVal += item.value * item.quantity;
            }

            return tempVal;
        }

        /// <summary>
        /// Removes all items from one inventory and adds them to another
        /// </summary>
        private static void CheckoutItems(Inventory removeFrom, Inventory addTo)
        {
            int invSize = removeFrom.record.Count;

            for (int i = 0; i < invSize; i++)
            {
                Item item = new Item(removeFrom.record[i]);

                addTo.AddItem(item, item.quantity);
            }

            removeFrom.record.Clear(); // Clears target Inventory of items
            return;
        }

        /// <summary>
        /// Used when purchasing items to checkout based on whether the player can afford their basket
        /// </summary>
        /// <returns></returns>
        private static bool Checkout()
        {
            if (Player.balance >= BasketValue())
            {
                Player.balance -= BasketValue();
                CheckoutItems(playerBasket, Player.inventory);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
