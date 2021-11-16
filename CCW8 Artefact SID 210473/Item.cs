using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact
{
    public class Item
    {
        public Item(string name, string description, float value, int maxStackQuantity)
        {
            Name = name;
            Description = description;
            Value = value;
            Quantity = 1;
            MaxStackQuantity = maxStackQuantity;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Value { get; set; }
        public int Quantity { get; set; }
        public int MaxStackQuantity { get; private set; }

        public void AddToQuantity(int amountToAdd)
        {
            Quantity += amountToAdd;
        }
    }
}
