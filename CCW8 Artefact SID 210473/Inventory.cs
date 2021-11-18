using System;
using System.Collections.Generic;
using System.Linq;

namespace Artefact
{
    /// <summary>
    /// Represents an inventory that can store groups of Items 
    /// </summary>
    public class Inventory
    {
        public List<Item> record = new List<Item>();

        /// <summary>
        /// Adds "<c>quantityToAdd</c>" amount of "<c>item</c>" to the Inventories' list
        /// </summary>
        /// <param name="item">The item to be added to the inventory</param>
        /// <param name="quantityToAdd">The quantity of items to be added to the inventory</param>
        public void AddItem(Item item, int quantityToAdd)
        {

            while (quantityToAdd > 0)
            { 
                if (record.Exists(list => (list.name == item.name) && (list.quantity < list.maxStackQuantity)))
                {
                    Item currentItem = record.First(list => (list.name == item.name) && (list.quantity < list.maxStackQuantity));

                    int toCompleteStackQuantity = (item.maxStackQuantity - currentItem.quantity);

                    int tempQuantityToAdd = Math.Min(quantityToAdd, toCompleteStackQuantity);

                    currentItem.AddToQuantity(tempQuantityToAdd);

                    quantityToAdd -= tempQuantityToAdd;
                }
                else
                {                 
                    Item tempItem = new Item(item);

                    tempItem.quantity = quantityToAdd;
                    quantityToAdd = 0;

                    record.Add(tempItem);
                }

            }
        }

        /// <summary>
        /// Removes "<c>quantityToRemove</c>" amount of "<c>item</c>" from the Inventories' list
        /// </summary>
        /// <param name="item">The item to be removed from the inventory</param>
        /// <param name="quantityToRemove">The quantity of items to be removed from the inventory</param>
        public void RemoveItem(Item item, int quantityToRemove)
        {
            while (quantityToRemove > 0)
            {

                if (record.Exists(list => (list.name == item.name) && (list.quantity >= 0)))
                {
                    Item currentItem = record.First(list => (list.name == item.name) && (list.quantity >= 0));

                    int maxRemoveQuantity = (item.maxStackQuantity - currentItem.quantity);

                    int tempQuantityToRemove = Math.Min(quantityToRemove, currentItem.quantity);

                    currentItem.AddToQuantity(-tempQuantityToRemove);

                    quantityToRemove -= tempQuantityToRemove;

                    if (currentItem.quantity < 1)
                    {
                        record.Remove(currentItem);
                    }
                }
                else
                {
                    throw new Exception("Not valid item to remove or trying to remove too much");
                    //quantityToRemove = 0;
                }
 
            }
        }
    }
}
