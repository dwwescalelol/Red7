using System;

namespace Red7
{
    internal class Player
    {
        public string Name { get; set; }
        public CardCollection Hand { get; }
        public CardCollection Palette { get; }
        public bool InPlay { get; set; }
        public int TurnOrder { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = new CardCollection();
            Palette = new CardCollection();
            InPlay = true;
            TurnOrder = 0;
        }

        /// <summary>
        /// Takes a card from the deck and adds it to the Hand
        /// </summary>
        /// <param name="deck">CardCollection that represents the deck</param>
        public void DrawFromDeck(CardCollection deck)
        {
            if (deck.GetNumberOfCards() == 0)
                return;

            Hand.AddCard(deck.RemoveCard(0));
        }

        /// <summary>
        /// Removes all the cards from a CardCollection (either Hand or Palette) and adds them to the deck
        /// </summary>
        /// <param name="deck">CardCollection that represents the deck</param>
        public void ReturnCardsToDeck(CardCollection deck)
        {
            for(int i = 0; i < Palette.GetNumberOfCards(); i++)
                deck.AddCard(Palette.RemoveCard(0));

            for (int i = 0; i < Hand.GetNumberOfCards(); i++)
                deck.AddCard(Hand.RemoveCard(0));
        }

        /// <summary>
        /// Takes a card from the Hand and adds it to the Palette
        /// </summary>
        /// <param name="i">int index of card in Hand to play to Palette</param>
        public void PlayToPalette(int i)
        {
            if (i >= Hand.GetNumberOfCards())
            {
                Console.WriteLine("index {0} unreachable", i);
                return;
            }

            Palette.AddCard(Hand.RemoveCard(i));
        }

        /// <summary>
        /// Takes a card from the Hand and adds it to the Canvas
        /// </summary>
        /// <param name="i">int index of card in Hand to play to Canvas</param>
        /// <param name="canvas">CardCollection that represents the canvas</param>
        public void PlayToCanvas(int i, CardCollection canvas)
        {
            if (i >= Hand.GetNumberOfCards())
                return;

            canvas.AddCard(Hand.RemoveCard(i));
        }

        /// <summary>
        /// Returns the palette back to the hand, used for undoing moves
        /// </summary>
        public void ReturnPaletteToHand()
        {
            int lastCardIndex = Palette.GetNumberOfCards() - 1;
            Hand.AddCard(Palette.RemoveCard(lastCardIndex));
        }

        /// <summary>
        /// Returns the canvas back to the hand, used for undoing moves
        /// </summary>
        public void ReturnCanvasToHand(CardCollection canvas)
        {
            int lastCardIndex = canvas.GetNumberOfCards()-1;
            Hand.AddCard(canvas.RemoveCard(lastCardIndex));
        }
    }
}
