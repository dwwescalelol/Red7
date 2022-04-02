using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red7
{
    internal interface IPlayer
    {
        /// <summary>
        /// Takes a card from the deck and adds it to the Hand
        /// </summary>
        /// <param name="deck">CardCollection that represents the deck</param>
        public void DrawFromDeck(CardCollection deck);
        /// <summary>
        /// Removes all the cards from a CardCollection (either Hand or Palette) and adds them to the deck
        /// </summary>
        /// <param name="deck">CardCollection that represents the deck</param>
        public void ReturnCardsToDeck(CardCollection deck);

        /// <summary>
        /// Takes a card from the Hand and adds it to the Palette
        /// </summary>
        /// <param name="i">int index of card in Hand to play to Palette</param>
        public void PlayToPalette(int i);
        /// <summary>
        /// Takes a card from the Hand and adds it to the Canvas
        /// </summary>
        /// <param name="i">int index of card in Hand to play to Canvas</param>
        /// <param name="canvas">CardCollection that represents the canvas</param>
        public void PlayToCanvas(int i, CardCollection canvas);
    }
}
