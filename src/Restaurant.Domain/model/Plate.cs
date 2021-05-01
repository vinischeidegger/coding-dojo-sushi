using System;

namespace Restaurant.BillCalculator.Domain.Model
{
    /// <summary>
    /// A plate is the minimum represenation of an item from a Restaurant Order
    /// </summary>
    public class Plate
    {
        public Color Color;
        public decimal Price;

        public Plate(Color color)
        {
            Color = color;
        }
    }

    public enum Color
    {
        Grey,
        Green,
        Yellow,
        Red,
        Blue
    }
}

