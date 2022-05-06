using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieRules
{
    /// <summary>
    /// Returns the better list of cards in a tie
    /// </summary>
    /// <param name="c1">First list</param>
    /// <param name="c2">Second list</param>
    /// <returns>List which wins in a Tie Break</returns>
    public static List<Card> TieBreak(List<Card> c1, List<Card> c2)
    {
        if (c1.Count == c2.Count)
            return BestCard(c1, c2);

        return c1.Count > c2.Count ? c1 : c2;
    }

    private static List<Card> BestCard(List<Card> c1, List<Card> c2)
    {
        Card p1 = BestCard(c1);
        Card p2 = BestCard(c2);

        return p1.CompareTo(p2) > 0 ? c1 : c2;
    }

    /// <summary>
    /// Finds the best card in a list of Cards
    /// <para>Where better is firstly higher number, than closer to red colour</para>
    /// </summary>
    /// <param name="cards">List of cards to find best card from</param>
    /// <returns>Best <see cref="Card"/> in list of cards</returns>
    public static Card BestCard(List<Card> cards)
    {
        Card bestCard = cards[0];

        foreach (Card c in cards)
            if (c.CompareTo(bestCard) > 0)
                bestCard = c;

        return bestCard;
    }
}
