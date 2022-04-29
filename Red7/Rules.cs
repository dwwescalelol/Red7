using System.Collections.Generic;
using System.Linq;

namespace Red7
{
    internal static class Rules
    {
        public static string Reason { get; set; }

        public static Player WhoIsWinning(TestGame game)
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
        private static Player Red(TestGame game)
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
        private static Player Orange(TestGame game)
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
                List<int> mostRecurringNumbers = FindMostOcouringNums(player);

                BestOrangeHand(player, playerCards, mostRecurringNumbers);

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

        /// <summary>
        /// If a player has the same highest value of multiple cards we must
        /// find the best cards that the player holds
        /// <para>for example having if hand is {Y4,R4,V1,B6,R6} the player has 2 4's
        /// and 2 6's. We must find which combination is best to compare against the winning
        /// hand</para>
        /// </summary>
        /// <param name="player">Player we are checking the palette of</param>
        /// <param name="playerCards">The players cards to be compared to the current strongest cards</param>
        /// <param name="mostRecurringNumbers">The numbers that the player has the highest frequency(s) of</param>
        private static void BestOrangeHand(Player player, List<Card> playerCards, List<int> mostRecurringNumbers)
        {
            //new list to store temp card sequences to, if playerCards empty or worsae than temp, temp replaces
            List<Card> playerComparisonCards = new List<Card>();
            foreach (int num in mostRecurringNumbers)
            {
                foreach (Card card in player.Palette.Cards)
                {
                    if (card.Number == num)
                    {
                        playerComparisonCards.Add(card);
                    }
                }
                if (playerComparisonCards == TieRules.TieBreak(playerComparisonCards, playerCards))
                {
                    playerCards = playerComparisonCards.ToList();
                }
            }
        }

        private static List<int> FindMostOcouringNums(Player player)
        {
            Dictionary<int, int> dict = new();
            int mostOfOneNumber = FindFreqOfMostOccouringNum(player, dict);

            List<int> mostRecurringNumbers = new List<int>();
            foreach (KeyValuePair<int, int> entry in dict)
            {
                if (entry.Value == mostOfOneNumber)
                {
                    mostRecurringNumbers.Add(entry.Key);
                }
            }

            return mostRecurringNumbers;
        }

        private static int FindFreqOfMostOccouringNum(Player player, Dictionary<int, int> dict)
        {
            foreach (Card card in player.Palette.Cards)
            {
                if (!dict.ContainsKey(card.Number))
                    dict[card.Number] = 0;

                dict[card.Number]++;
            }
            int mostOfOneNumber = dict.Values.Max();
            return mostOfOneNumber;
        }

        #endregion

        #region Yellow
        private static Player Yellow(TestGame game)
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
                List<Colours> mostRecurringNumbers = FindMostOccColours(player);

                BestYellowHand(player, playerCards, mostRecurringNumbers);

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

        /// <summary>
        /// If a player has the same highest value of multiple cards we must
        /// find the best cards that the player holds
        /// <para>for example having if hand is {Y4,R4,V1,B6,R6} the player has 2 4's
        /// and 2 6's. We must find which combination is best to compare against the winning
        /// hand</para>
        /// </summary>
        /// <param name="player">Player we are checking the palette of</param>
        /// <param name="playerCards">The players cards to be compared to the current strongest cards</param>
        /// <param name="mostRecurringColours">The numbers that the player has the highest frequency(s) of</param>
        private static void BestYellowHand(Player player, List<Card> playerCards, List<Colours> mostRecurringColours)
        {
            //new list to store temp card sequences to, if playerCards empty or worsae than temp, temp replaces
            List<Card> playerComparisonCards = new List<Card>();
            foreach (Colours colour in mostRecurringColours)
            {
                foreach (Card card in player.Palette.Cards)
                {
                    if (card.Colour == colour)
                    {
                        playerComparisonCards.Add(card);
                    }
                }
                if (playerComparisonCards == TieRules.TieBreak(playerComparisonCards, playerCards))
                {
                    playerCards = playerComparisonCards.ToList();
                }
            }
        }

        private static List<Colours> FindMostOccColours(Player player)
        {
            Dictionary<Colours, int> dict = new();
            int mostOfOneNumber = FindFreqMostOccColours(player, dict);

            List<Colours> mostRecurringColours = new List<Colours>();
            foreach (KeyValuePair<Colours, int> entry in dict)
            {
                if (entry.Value == mostOfOneNumber)
                {
                    mostRecurringColours.Add(entry.Key);
                }
            }

            return mostRecurringColours;
        }

        private static int FindFreqMostOccColours(Player player, Dictionary<Colours, int> dict)
        {
            foreach (Card card in player.Palette.Cards)
            {
                if (!dict.ContainsKey(card.Colour))
                    dict[card.Colour] = 0;

                dict[card.Colour]++;
            }
            int mostOfOneNumber = dict.Values.Max();
            return mostOfOneNumber;
        }

        #endregion

        #region Green
        private static Player Green(TestGame game)
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
        private static Player Blue(TestGame game)
        {
            Player winner = null;

            List<Card> winningCards = new List<Card>();
            List<Card> playerCards = new List<Card>();
            foreach (Player player in game.Players)
            {
                //foreach (Card card in player.Palette.Cards)
                //{
                //    if (playerCards.Find(i => i.Colour == value))
                //    {

                //    }
                //}

                //int diffColoursPlayer = colours.Count;

                //if (diffColoursPlayer > mostDiffColoursGame)
                //{
                //    mostDiffColoursGame = diffColoursPlayer;
                //    winner = player;
                //}
                //else if (mostDiffColoursGame == diffColoursPlayer)
                //{

                //}
            }
            return winner;
        }
        #endregion

        #region Indigo
        private static Player Indigo(TestGame game)
        {
            int mostDiffColoursGame = 0;
            Player winner = null;
            foreach (Player player in game.Players)
            {
                List<Colours> colours = new();

                foreach (Card card in player.Palette.Cards)
                {
                    if (!colours.Contains(card.Colour))
                        colours.Add(card.Colour);
                }

                int diffColoursPlayer = colours.Count;

                if (diffColoursPlayer > mostDiffColoursGame)
                {
                    mostDiffColoursGame = diffColoursPlayer;
                    winner = player;
                }
                else if (mostDiffColoursGame == diffColoursPlayer)
                {

                }
            }
            return winner;
        }
        #endregion

        #region Violet
        private static Player Violet(TestGame game)
        {
            int mostCardsBelow4Game = 0;
            Player winner = null;
            foreach (Player player in game.Players)
            {
                int cardsBelow4Player = 0;

                foreach (Card card in player.Palette.Cards)
                {
                    if(card.Number < 4)
                        cardsBelow4Player++;
                }

                if (cardsBelow4Player > mostCardsBelow4Game)
                {
                    mostCardsBelow4Game = cardsBelow4Player;
                    winner = player;
                }
                else if (mostCardsBelow4Game == cardsBelow4Player)
                {

                }
            }
            return winner;
        }
        #endregion
    }
}
