using System;

namespace Red7
{
    internal partial class Game
    {
        public Player[] Players { get; }
        public CardCollection Canvas { get; }
        public bool IsActive { get; set; }

        public Deck Deck { get; }
        private int activePlayers;

        public Game()
        {
            Deck = new Deck();
            Createdeck();
            activePlayers = GetNumberOfPlayers();
            Players = new Player[activePlayers];
            AddPlayers();
            Canvas = new CardCollection();
            IsActive = true;
        }

        #region Game Setup

        /// <summary>
        /// Creates the deck that the game will use
        /// </summary>
        private void Createdeck()
        {
            foreach (Colours colour in Enum.GetValues(typeof(Colours)))
                for (int i = 1; i < 8; i++)
                    Deck.AddCard(new Card(colour, i));

            Deck.ShuffleCards();
        }

        /// <summary>
        /// Gets the number of players in the current game
        /// </summary>
        /// <returns>int The amount of players in the current game</returns>
        private static int GetNumberOfPlayers()
        {
            Console.WriteLine("HOW MANY PLAYERS?");

            return GetNumberInput("ENTER 1-4", 1, 4);
        }

        /// <summary>
        /// Adds all the players to the Players array and propts each one to enter a name
        /// </summary>
        private void AddPlayers()
        {
            for (int i = 0; i < Players.Length; i++)
            {
                Console.WriteLine("ENTER PLAYER {0} NAME", i + 1);
                Players[i] = new Player(Console.ReadLine());
                GivePlayerHand(Players[i]);
            }
        }

        /// <summary>
        /// Gives each player 7 cards to put into their hand to start the round
        /// </summary>
        /// <param name="player">Player to give cards to</param>
        private void GivePlayerHand(Player player)
        {
            for (int i = 0; i < 7; i++)
                player.DrawFromDeck(Deck);
        }
        #endregion

        /// <summary>
        /// Prints the Pallets of all players to the Console.
        /// </summary>
        private void ShowPalettes(Player currentPlayer)
        {
            foreach (Player player in Players)
            {
                if(currentPlayer == player)
                    continue;

                Console.Write(player.Name + " PALETTE: ");
                player.Palette.PrintCards(false);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print the top card in the canvas to the console, if canvas empty print RED 
        /// </summary>
        private void ShowCanvasCard()
        {
            if (Canvas.IsEmpty())
                Console.WriteLine("CANVAS IS EMPTY: " + Colours.RED);
            else
                Console.WriteLine("CANVAS: " + Canvas.GetTopCardColour());
        }

        /// <summary>
        /// Returns the player who has won the game, or null if no player has won yet.
        /// </summary>
        /// <returns>Player who has won.</returns>
        public Player WhoWon()
        {
            if (activePlayers > 1)
                return null;

            foreach (Player player in Players)
                if (player.InPlay)
                    return player;

            return null;
        }

        public bool HasFinished()
        {
            return activePlayers == 1;
        }

    }
}
