using System;

namespace Red7
{
    internal partial class Game
    {
        public Player[] Players { get; }
        public CardCollection Deck { get; }
        public CardCollection Canvas { get; }
        public bool IsActive { get; set; }
        private int activePlayers;

        public Game()
        {
            Deck = new CardCollection();
            CreateDeck();
            activePlayers = GetNumberOfPlayers();
            Players = new Player[activePlayers];
            AddPlayers();
            Canvas = new CardCollection();
            IsActive = true;
        }

        /// <summary>
        /// Creates the deck that the game will use
        /// </summary>
        private void CreateDeck()
        {
            foreach (Colours colour in Enum.GetValues(typeof(Colours)))
                for (int i = 1; i < 8; i++)
                    Deck.AddCard(new Card(colour, i));

            Deck.ShuffleCards();
        }

        /// <summary>
        /// Itterates through players and prompts each player to take their turn.
        /// </summary>
        public void PlayTurn()
        {
            foreach (Player player in Players)
            {
                if (!player.InPlay)
                    continue;

                Console.WriteLine("\nIT'S " + player.Name + " TURN! PRESS ANY KEY TO ADVANCE\n");
                
                Console.ReadKey();

                ShowPalettes();

                Console.Write("\nHAND: ");
                player.Hand.PrintCards();
                Console.Write("\nPALETTE: ");
                player.Palette.PrintCards();
                Console.Write("\nCANVAS: ");
                Canvas.PrintCards();

                TurnType(player);
                Console.Clear();
            }
        }

        private void ShowPalettes()
        {
            foreach (Player player in Players)
            {
                Console.Write(player.Name + " PALETTE: ");
                player.Palette.PrintCards();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Returns the player who has won the game, or null if no player has won yet.
        /// </summary>
        /// <returns>Player who has won</returns>
        public Player HasWon()
        {
            if (activePlayers > 1)
                return null;

            foreach (Player player in Players)
                if (player.InPlay)
                    return player;

            return null;
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
                Console.WriteLine("ENTER PLAYER {0} NAME", i+1);
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
    }
}
