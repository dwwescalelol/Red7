using System.Collections.Generic;


namespace Red7
{
    internal static class TieRules
    {

        public static List<Card> TieBreak(List<Card> c1, List<Card> c2)
        {
            if (c1.Count == c2.Count)
                return BestCard(c1, c2);

            return c1.Count > c2.Count ? c1: c2;
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
