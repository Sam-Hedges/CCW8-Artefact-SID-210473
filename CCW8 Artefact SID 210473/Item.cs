using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact
{
    public class Item
    {
        public Item(string name, string description, float value, int maxStackQuantity)
        {
            this.name = name;
            this.description = description;
            this.value = value;
            this.quantity = 1;
            this.maxStackQuantity = maxStackQuantity;
        }

        public Item()
        {
            name = string.Empty;
            description = string.Empty;
            value = 0;
            quantity = 0;
            maxStackQuantity = 1;
        }

        public string name;
        public string description;
        public float value;
        public int quantity;
        public int maxStackQuantity;

        public void AddToQuantity(int amountToAdd)
        {
            quantity += amountToAdd;
        }
    }
}
