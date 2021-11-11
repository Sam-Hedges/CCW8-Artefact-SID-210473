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

        public string name { get; private set; }
        public string description { get; private set; }
        public float value { get; set; }
        public int quantity { get; set; }
        public int maxStackQuantity { get; private set; }

        public void AddToQuantity(int amountToAdd)
        {
            quantity += amountToAdd;
        }
    }
}
