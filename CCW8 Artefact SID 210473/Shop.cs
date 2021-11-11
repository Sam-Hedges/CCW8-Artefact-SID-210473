using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact
{
    public static class Shop
    {
        public static Inventory stock;
        public static Inventory playerBasket;

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
