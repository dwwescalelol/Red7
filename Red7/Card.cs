using System;

namespace Red7
{
    enum Colours
    {
        VIOLET,
        INDIGO,
        BLUE,
        GREEN,
        YELLOW,
        ORANGE,
        RED
    }

    internal class Card : IComparable
    {
        public Colours Colour { get; }
        public int Number { get; }

        public Card(Colours colour, int number)
        {
            Colour = colour;
            Number = number;
        }

        public override string ToString()
        {
            return Colour.ToString() + " " + Number;
        }

        public int CompareTo(object obj)
        {
            if (obj is not Card card)
                return 1;

            if (Number == card.Number)
                return card.Colour.CompareTo(Colour);

            return Number.CompareTo(card.Number);
        }
    }
}
