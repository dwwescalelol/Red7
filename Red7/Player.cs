using System;

namespace Red7
{
    internal class Player : IPlayer
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

        public void DrawFromDeck(CardCollection deck)
        {
            if (deck.GetNumberOfCards() == 0)
                return;

            Hand.AddCard(deck.RemoveCard(0));
        }

        public void ReturnCardsToDeck(CardCollection deck)
        {
            for(int i = 0; i < Palette.GetNumberOfCards(); i++)
                deck.AddCard(Palette.RemoveCard(0));

            for (int i = 0; i < Hand.GetNumberOfCards(); i++)
                deck.AddCard(Hand.RemoveCard(0));
        }

        public void PlayToPalette(int i)
        {
            if (i >= Hand.GetNumberOfCards())
            {
                Console.WriteLine("index {0} unreachable", i);
                return;
            }

            Palette.AddCard(Hand.RemoveCard(i));
        }

        public void PlayToCanvas(int i, CardCollection canvas)
        {
            if (i >= Hand.GetNumberOfCards())
                return;

            canvas.AddCard(Hand.RemoveCard(i));
        }

        public void ReturnPaletteToHand()
        {
            int lastCardIndex = Palette.GetNumberOfCards() - 1;
            Hand.AddCard(Palette.RemoveCard(lastCardIndex));
        }

        public void ReturnCanvasToHand(CardCollection canvas)
        {
            int lastCardIndex = canvas.GetNumberOfCards()-1;
            Hand.AddCard(canvas.RemoveCard(lastCardIndex));
        }

    }
}
