using System;
using System.Collections.Generic;


namespace Red7
{
    internal class CardCollection
    {
        private readonly List<Card> cards;

        public CardCollection()
        {
            cards = new List<Card>();
        }

        /// <summary>
        /// Gets the number of cards in the CardCollection
        /// </summary>
        /// <returns>int number of cards in CardCollection</returns>
        public int GetNumberOfCards()
        {
            return cards.Count;
        }

        /// <summary>
        /// Prints all the cards and their respective indexes to the Console
        /// Formatted as (index) COLOUR NUMBER
        /// </summary>
        public void PrintCards()
        {
            for(int i = 0; i < GetNumberOfCards(); i++)
                Console.Write("(" + i+1 + ")" + " " + cards[i].ToString() + " ");
        }

        /// <summary>
        /// Adds a card to the end of the CardCollection
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        /// <summary>
        /// Removes a card at index i from the CardCollection and returns it
        /// If the index is negative or out of the bounds of the CardCollection will return null
        /// </summary>
        /// <param name="i">int index of card to be removed</param>
        /// <returns>Card at index i</returns>
        public Card RemoveCard(int i)
        {
            if(cards.Count <= i)
                return null;

            cards.Remove(cards[i]);
            return cards[i];
        }

        /// <summary>
        /// Shuffels the ordering of the current card collection using a fisher yates shuffle
        /// </summary>
        public void ShuffleCards()
        {
            Random rnd = new ();

            for (int i = cards.Count - 1; i > 0; i--)
            {
                int r = rnd.Next(i);
                Card value = cards[r];
                cards[r] = cards[i];
                cards[i] = value;
            }
        }
    }
}
