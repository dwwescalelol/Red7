using System.Collections.Generic;
using System.Linq;

namespace Red7
{
    internal static class Rules
    {
        public static string Reason { get; set; }

        public static Player WhoIsWinning(Game game)
        {
            Reason = "";
            return game.Canvas.GetTopCardColour() switch
            {
                Colours.RED => Red(game),
                Colours.ORANGE => Orange(game),
                Colours.YELLOW => Yellow(game),
                Colours.BLUE => Blue(game),
                Colours.GREEN => Green(game),
                Colours.INDIGO => Indigo(game),
                Colours.VIOLET => Violet(game),
                _ => Red(game),
            };
        }

        #region Red
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private static Player Red(Game game)
        {
            Player winner = null;

            List<Card> winningCards = new List<Card>();
            List<Card> playerCards = new List<Card>();
            foreach (Player player in game.Players)
            {
                //only players who are in the games pallets are considered
                if (!player.InPlay)
                    continue;

                playerCards.Clear();

                FindMaxCards(playerCards, player);

                if (winningCards.Count == 0 || playerCards[0].Number > winningCards[0].Number)
                {
                    winningCards = playerCards.ToList();
                    winner = player;
                }
                else if (winningCards[0].Number == playerCards[0].Number)
                {
                    if (playerCards == TieRules.TieBreak(winningCards, playerCards))
                    {
                        winningCards = playerCards.ToList();
                        winner = player;
                    }
                }
            }

            return winner;
        }

        private static void FindMaxCards(List<Card> playerCards, Player player)
        {
            foreach (Card card in player.Palette.Cards)
            {
                if (playerCards.Count == 0 || card.Number > playerCards[0].Number)
                {
                    playerCards.Clear();
                    playerCards.Add(card);
                }
                else if (card.Number == playerCards[0].Number)
                {
                    playerCards.Add(card);
                }
            }
        }
        #endregion

        #region Orange
        /// <summary>
        /// Player with most of one number will win. If player has multiple sets of cards
        /// that are of equal size, the strongest set will be considered. For example
        /// a player with 2 4's and 2 6's will have both sets considered.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private static Player Orange(Game game)
        {
            //need to keep track of winner and lists of cards to be sent to tie break
            Player winner = null;

            List<Card> winningCards = new List<Card>();
            List<Card> playerCards = new List<Card>();

            foreach (Player player in game.Players)
            {
                //only players who are in the games pallets are considered
                if (!player.InPlay)
                    continue;

                playerCards.Clear();

                //find number(s) that player palette has the most of
                //if hand is {Y4,R4,V1,B6,R6} list will be {4, 6}
                int largestMostOccNum = FindLargestMostOccNum(player);

                //adds cards with most occouring cards to list
                foreach (Card c in player.Palette.Cards)
                {
                    if (c.Number == largestMostOccNum)
                        playerCards.Add(c);
                }

                if (playerCards.Count > winningCards.Count)
                {
                    winningCards = playerCards.ToList();
                    winner = player;
                }
                else if (playerCards.Count == winningCards.Count)
                {
                    if (playerCards == TieRules.TieBreak(winningCards, playerCards))
                    {

                        winningCards = playerCards.ToList();
                        winner = player;
                    }
                }
            }
            return winner;
        }

        private static int FindLargestMostOccNum(Player player)
        {
            //dic contains a number as key and the freq of the number in player palette
            Dictionary<int, int> dict = new();

            //find highest freq palette has
            foreach (Card card in player.Palette.Cards)
            {
                if (!dict.ContainsKey(card.Number))
                    dict[card.Number] = 0;

                dict[card.Number]++;
            }

            int mostOfOneNumber = dict.Values.Max();

            //find numbers that are of that freq
            int largestMostOccNum = 0;

            foreach (KeyValuePair<int, int> entry in dict)
                if (entry.Value == mostOfOneNumber && entry.Key > largestMostOccNum)
                    largestMostOccNum = entry.Key;

            return largestMostOccNum;
        }

        #endregion

        #region Yellow
        private static Player Yellow(Game game)
        {
            //need to keep track of winner and lists of cards to be sent to tie break
            Player winner = null;

            List<Card> winningCards = new List<Card>();
            List<Card> playerCards = new List<Card>();

            foreach (Player player in game.Players)
            {
                //only players who are in the games pallets are considered
                if (!player.InPlay)
                    continue;

                playerCards.Clear();

                //find number(s) that player palette has the most of
                //if hand is {Y4,R4,V1,B6,R6} list will be {4, 6}
                Colours largestMostOccNum = FindLargestMostOccColour(player);

                //adds cards with most occouring cards to list
                foreach (Card c in player.Palette.Cards)
                {
                    if (c.Colour == largestMostOccNum)
                        playerCards.Add(c);
                }

                if (playerCards.Count > winningCards.Count)
                {
                    winningCards = playerCards.ToList();
                    winner = player;
                }
                else if (playerCards.Count == winningCards.Count)
                {
                    if (playerCards == TieRules.TieBreak(winningCards, playerCards))
                    {

                        winningCards = playerCards.ToList();
                        winner = player;
                    }
                }
            }
            return winner;
        }

        private static Colours FindLargestMostOccColour(Player player)
        {
            //dic contains a number as key and the freq of the number in player palette
            Dictionary<Colours, List<Card>> dict = new();

            //find highest freq palette has
            foreach (Card card in player.Palette.Cards)
            {
                if (!dict.ContainsKey(card.Colour))
                    dict[card.Colour] = new List<Card>();

                dict[card.Colour].Add(card);
            }

            Card bestCard = null;
            int numCardsOneColour = 0;

            foreach(KeyValuePair<Colours, List<Card>> pair in dict)
            {
                if (pair.Value.Count > numCardsOneColour)
                {
                    numCardsOneColour = pair.Value.Count;
                    bestCard = TieRules.BestCard(pair.Value);
                }
            }

            return bestCard.Colour;
        }

        #endregion

        #region Green
        private static Player Green(Game game)
        {
            Player winner = null;

            List<Card> winningCards = new List<Card>();
            List<Card> playerCards = new List<Card>();
            foreach (Player player in game.Players)
            {
                //only players who are in the games pallets are considered
                if (!player.InPlay)
                    continue;

                playerCards.Clear();

                foreach (Card card in player.Palette.Cards)
                {
                    if (card.Number % 2 == 0)
                        playerCards.Add(card);
                }

                if (playerCards.Count > winningCards.Count)
                {
                    winningCards = playerCards.ToList();
                    winner = player;
                }
                else if (playerCards.Count == winningCards.Count)
                {
                    if (playerCards == TieRules.TieBreak(winningCards, playerCards))
                    {

                        winningCards = playerCards.ToList();
                        winner = player;
                    }
                }
            }
            return winner;
        }
        #endregion

        #region Blue
        //Most differenc colours wins
        private static Player Blue(Game game)
        {
            Player winner = null;

            List<Card> winningCards = new List<Card>();
            List<Card> playerCards = new List<Card>();
            foreach (Player player in game.Players)
            {
                //only players who are in the games pallets are considered
                if (!player.InPlay)
                    continue;

                playerCards.Clear();

                //find best cards of all unique colorus in palette
                Dictionary<Colours, Card> dict = new();

                foreach(Card card in player.Palette.Cards)
                {
                    if(!dict.ContainsKey(card.Colour) || dict[card.Colour].CompareTo(card) < 0)
                    {
                        dict[card.Colour] = card;
                    }
                }

                //add all the best cards of each colour in palette to considered cards
                foreach(KeyValuePair<Colours, Card> pair in dict)
                {
                    playerCards.Add(pair.Value);
                }

                //switch hands if player better or tiebreak
                if (playerCards.Count > winningCards.Count)
                {
                    winningCards = playerCards.ToList();
                    winner = player;
                }
                else if (playerCards.Count == winningCards.Count)
                {
                    if (playerCards == TieRules.TieBreak(winningCards, playerCards))
                    {

                        winningCards = playerCards.ToList();
                        winner = player;
                    }
                }
            }
            return winner;
        }
        #endregion

        #region Indigo
        private static Player Indigo(Game game)
        {
            Player winner = null;

            List<Card> winningCards = new List<Card>();
            List<Card> playerCards = new List<Card>();
            foreach (Player player in game.Players)
            {
                //only players who are in the games pallets are considered
                if (!player.InPlay)
                    continue;
                playerCards.Clear();
                playerCards = FindLargestSequenceOfCards(player);

                //switch hands if player better or tiebreak
                if (playerCards.Count > winningCards.Count)
                {
                    winningCards = playerCards.ToList();
                    winner = player;
                }
                else if (playerCards.Count == winningCards.Count)
                {
                    if (playerCards == TieRules.TieBreak(winningCards, playerCards))
                    {

                        winningCards = playerCards.ToList();
                        winner = player;
                    }
                }
            }
            return winner;
        }

        private static List<Card> FindLargestSequenceOfCards(Player player)
        {
            //dictionary stores best cards for each number
            Dictionary<int, Card> cards = new Dictionary<int, Card>();

            foreach(Card card in player.Palette.Cards)
                if (!cards.ContainsKey(card.Number) || cards[card.Number].CompareTo(card) < 0)
                    cards[card.Number] = card;

            //find longest run of cards, and where run starts
            int startBest = 0;
            int currentLength = 1;
            int maxLength = 1;

            //from 1 -> 7 as the cards are only from 1 - 7
            for (int i = 1; i < 8; i++)
            {
                if (!cards.ContainsKey(i))
                    continue;

                int startCurrent = cards[i].Number;
                while (i < 7 && cards.ContainsKey(i + 1))
                {
                    currentLength++;
                    i++;
                }
                if (currentLength >= maxLength)
                {
                    startBest = startCurrent;
                    maxLength = currentLength;
                }
            }

            //add run of cards to list and return list to be player cards
            List<Card> playerCards = new List<Card>();

            for(int i = startBest; i < maxLength + startBest; i++)
                playerCards.Add(cards[i]);

            return playerCards;
        }

        #endregion

        #region Violet
        private static Player Violet(Game game)
        {
            Player winner = null;

            List<Card> winningCards = new List<Card>();
            List<Card> playerCards = new List<Card>();
            foreach (Player player in game.Players)
            {
                //only players who are in the games pallets are considered
                if (!player.InPlay)
                    continue;

                playerCards.Clear();

                foreach (Card card in player.Palette.Cards)
                {
                    if (card.Number < 4)
                        playerCards.Add(card);
                }

                if (playerCards.Count > winningCards.Count)
                {
                    winningCards = playerCards.ToList();
                    winner = player;
                }
                else if (playerCards.Count == winningCards.Count)
                {
                    if (playerCards == TieRules.TieBreak(winningCards, playerCards))
                    {

                        winningCards = playerCards.ToList();
                        winner = player;
                    }
                }
            }
            return winner;
        }
        #endregion
    }
}
