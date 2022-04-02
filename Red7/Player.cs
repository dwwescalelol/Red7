using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red7
{
    internal class Player : IPlayer
    {
        public string Name { get; set; }
        public CardCollection Hand { get; }
        public CardCollection Palette { get; }
        public bool InPlay { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = new CardCollection();
            Palette = new CardCollection();
            InPlay = true;
        }

        public void DrawFromDeck(CardCollection deck)
        {
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
                return;

            Palette.AddCard(Hand.RemoveCard(i));
        }

        public void PlayToCanvas(int i, CardCollection canvas)
        {
            if (i >= Hand.GetNumberOfCards())
                return;

            canvas.AddCard(Hand.RemoveCard(i));
        }
    }
}
