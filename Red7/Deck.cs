using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red7
{
    internal class Deck : CardCollection
    {
        /// <summary>
        /// Shuffels the ordering of the current Cards collection using a fisher yates shuffle
        /// </summary>
        public void ShuffleCards()
        {
            Random rnd = new Random();

            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int r = rnd.Next(i);
                Card value = Cards[r];
                Cards[r] = Cards[i];
                Cards[i] = value;
            }
        }
    }
}
