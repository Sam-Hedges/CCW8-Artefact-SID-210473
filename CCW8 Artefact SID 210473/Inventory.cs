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

                if (record.Exists(list => (list.name == item.name) && (list.quantity < item.maxStackQuantity)))
                {
                    Item currentItem = record.First(list => (list.name == item.name) && (list.quantity < item.maxStackQuantity));

                    int toCompleteStackQuantity = (item.maxStackQuantity - currentItem.quantity);

                    int tempQuantityToAdd = Math.Min(quantityToAdd, toCompleteStackQuantity);

                    int currentItemIndex = record.IndexOf(currentItem);

                    record[currentItemIndex].AddToQuantity(tempQuantityToAdd);

                    quantityToAdd -= tempQuantityToAdd;
                }
                else
                {                 
                    Item tempItem = item;

                    tempItem.quantity = 0;

                    if (quantityToAdd <= item.maxStackQuantity) 
                    { 
                        tempItem.AddToQuantity(quantityToAdd);
                        quantityToAdd = 0;
                    }

                    record.Add(tempItem);
                }

            }
        }

        public void RemoveItem(Item item, int quantityToRemove)
        {
            while (quantityToRemove > 0)
            {

                if (record.Exists(list => (list.name == item.name) && (list.quantity > 0)))
                {
                    Item currentItem = record.First(list => (list.name == item.name) && (list.quantity > 0));

                    int maxRemoveQuantity = (item.maxStackQuantity - currentItem.quantity);

                    int tempQuantityToRemove = Math.Min(quantityToRemove, currentItem.quantity);

                    int currentItemIndex = record.IndexOf(currentItem);

                    record[currentItemIndex].AddToQuantity(-tempQuantityToRemove);

                    quantityToRemove -= tempQuantityToRemove;

                    if (record[currentItemIndex].quantity < 1)
                    {
                        record.Remove(record[currentItemIndex]);
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
