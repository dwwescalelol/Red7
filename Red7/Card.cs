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

    internal class Card
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
            Card other = (Card)obj;
            //goes in order of Tie, check number first then colour
            if (Number == other.Number)
                return Colour.CompareTo(other.Colour);

            return Number.CompareTo(other.Number);
        }
    }
}
