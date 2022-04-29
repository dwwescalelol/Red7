using System;
using System.Collections.Generic;


namespace Red7
{
    internal class CardCollection
    {
        public List<Card> Cards { get; }

        public CardCollection()
        {
            Cards = new List<Card>();
        }

        /// <summary>
        /// Gets the number of Cards in the CardCollection
        /// </summary>
        /// <returns>int number of Cards in CardCollection</returns>
        public int GetNumberOfCards()
        {
            return Cards.Count;
        }

        /// <summary>
        /// Prints all the Cards and their respective indexes to the Console
        /// Formatted as (index) COLOUR NUMBER
        /// </summary>
        public void PrintCards(bool ordered)
        {
            if (ordered)
            {
                for (int i = 0; i < GetNumberOfCards(); i++)
                    Console.Write("({0}) {1} ", i + 1, Cards[i].ToString());
            }
            else
            {
                string buffer = "";
                for (int i = 0; i < GetNumberOfCards(); i++)
                {
                    if (i == 1)
                        buffer = ", ";
                    Console.Write(buffer + Cards[i].ToString());
                }
            }
        }

        /// <summary>
        /// Adds a card to the end of the CardCollection
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        /// <summary>
        /// Removes a card at index i from the CardCollection and returns it
        /// If the index is negative or out of the bounds of the CardCollection will return null
        /// </summary>
        /// <param name="i">int index of card to be removed</param>
        /// <returns>Card at index i</returns>
        public Card RemoveCard(int i)
        {
            if (Cards.Count <= i)
                return null;

            Card card = Cards[i];
            Cards.Remove(Cards[i]);
            return card;
        }

        /// <summary>
        /// Returns the colour of the top card of this card collection
        /// <para>If empty return <see cref="Colours.RED"/></para>
        /// </summary>
        /// <returns><see cref="Colours"/> of the top card of the <see cref="CardCollection"/></returns>
        public Colours GetTopCardColour()
        {
            if (IsEmpty())
                return Colours.RED;
            return Cards[^1].Colour;
        }

        /// <summary>
        /// Returns a boolean dependant on if the <see cref="CardCollection"/> is empty
        /// </summary>
        /// <returns>bool true if empty, false if not</returns>
        public bool IsEmpty()
        {
            return Cards.Count == 0;
        }
    }
}
