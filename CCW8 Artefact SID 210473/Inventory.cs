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

                    int maxStackQuantity = (item.maxStackQuantity - currentItem.quantity);

                    int tempQuantityToAdd = Math.Min(quantityToAdd, maxStackQuantity);

                    currentItem.AddToQuantity(tempQuantityToAdd);

                    quantityToAdd -= tempQuantityToAdd;
                }
                else
                {                 
                    Item tempItem = item;

                    tempItem.quantity = 0;

                    record.Add(item);
                }

            }
        }
    }
}
