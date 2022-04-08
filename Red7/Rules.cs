using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red7
{
    internal class Rules
    {
        public Game Game { get; }
        public string Reason { get; }

        public Rules(Game game)
        {
            Game = game;
        }

        public Player WhoIsWinning()
        {

            return Game.Canvas.Cards[^1].Colour switch
            {
                Colours.RED => Red(),
                Colours.ORANGE => Orange(),
                Colours.YELLOW => Yellow(),
                Colours.BLUE => Blue(),
                Colours.GREEN => Green(),
                Colours.INDIGO => Indigo(),
                Colours.VIOLET => Violet(),
                _ => null,
            };
        }

        private Player Red()
        {
            int maxAll = 0;
            CardCollection winningHand = new CardCollection();
            CardCollection playerHand = new CardCollection();
            Player winner = null;
            foreach (Player player in Game.Players)
            {
                int maxPlayer = 0;

                foreach (Card card in player.Palette.Cards)
                {
                    if (card.Number > maxPlayer)
                        maxPlayer = card.Number;
                }

                if (maxPlayer > maxAll)
                {
                    maxAll = maxPlayer;
                    winner = player;
                }

                if (maxPlayer == maxAll)
                {
 
                }
            }

            return winner;
        }

        private Player Orange()
        {
            return null;
        }

        private Player Yellow()
        {
            return null;
        }

        private Player Green()
        {
            return null;
        }

        private Player Blue()
        {
            return null;
        }

        private Player Indigo()
        {
            return null;
        }

        private Player Violet()
        {
            return null;
        }
    }
}
