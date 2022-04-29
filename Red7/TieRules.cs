using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red7
{
    internal static class TieRules
    {

        public static List<Card> TieBreak(List<Card> c1, List<Card> c2)
        {
            return MostCards(c1, c2);
        }

        private static List<Card> MostCards(List<Card> c1, List<Card> c2)
        {
            if (c1.Count == c2.Count)
                return BestCard(c1, c2);

            return c1.Count > c2.Count ? c1: c2;
        }

        private static List<Card> HighestNumber(List<Card> c1, List<Card> c2)
        {
            int max1 = HighestCard(c1);
            int max2 = HighestCard(c2);

            if (max1 == max2)
                return BestColour(c1, c2, max1);

            return max1 > max2 ? c1 : c2;
        }

        private static List<Card> BestColour(List<Card> c1, List<Card> c2, int num)
        {
            Colours max1 = BestColour(c1, num);
            Colours max2 = BestColour(c2, num);

            return max1 > max2 ? c1 : c2;
        }


        private static int HighestCard(List<Card> cards)
        {
            int max = 0;
            foreach (Card c in cards)
                if (c.Number > max)
                    max = c.Number;

            return max;
        }

        private static Colours BestColour(List<Card> cards, int num)
        {
            Colours maxColour = Colours.VIOLET;
            foreach (Card c in cards)
                if (c.Number == num && c.Colour > maxColour)
                    maxColour = c.Colour;
            
            return maxColour;

        }

        private static List<Card> BestCard(List<Card> c1, List<Card> c2)
        {
            Card p1 = BestCard(c1);
            Card p2 = BestCard(c2);

            return p1.CompareTo(p2) > 0 ? c1 : c2;
        }

        public static Card BestCard(List<Card> cards)
        {
            Card bestCard = cards[0];

            foreach(Card c in cards)
            {
                if(c.CompareTo(bestCard) < 0)
                {
                    bestCard = c;
                }
            }

            return bestCard;
        }
    }
}
