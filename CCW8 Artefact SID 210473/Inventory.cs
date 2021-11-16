using System;
using System.Collections.Generic;
using System.Linq;

namespace Artefact
{
    public class Inventory
    {
        public readonly List<Item> record = new List<Item>();

        public void AddItem(Item item, int quantityToAdd)
        {

            while (quantityToAdd > 0)
            {

                if (record.Exists(list => (list.Name == item.Name) && (list.Quantity < item.MaxStackQuantity)))
                {
                    Item currentItem = record.First(list => (list.Name == item.Name) && (list.Quantity < item.MaxStackQuantity));

                    int maxStackQuantity = (item.MaxStackQuantity - currentItem.Quantity);

                    int tempQuantityToAdd = Math.Min(quantityToAdd, maxStackQuantity);

                    currentItem.AddToQuantity(tempQuantityToAdd);

                    quantityToAdd -= tempQuantityToAdd;
                }
                else
                {                 
                    Item tempItem = item;

                    tempItem.Quantity = 0;

                    record.Add(item);
                }

            }
        }

        public void RemoveItem(Item item, int quantityToRemove)
        {
            while (quantityToRemove > 0)
            {

                if (record.Exists(list => (list.Name == item.Name) && (list.Quantity > 0)))
                {
                    Item currentItem = record.First(list => (list.Name == item.Name) && (list.Quantity > 0));

                    int maxRemoveQuantity = (item.MaxStackQuantity - currentItem.Quantity);

                    int tempQuantityToRemove = Math.Min(quantityToRemove, currentItem.Quantity);

                    currentItem.AddToQuantity(-tempQuantityToRemove);

                    quantityToRemove -= tempQuantityToRemove;

                    if (currentItem.Quantity < 1)
                    {
                        record.Remove(currentItem);
                    }
                }
                else
                {
                    throw new Exception("Not valid item to remove or trying to remove too much");
                }
 
            }
        }
    }
}
