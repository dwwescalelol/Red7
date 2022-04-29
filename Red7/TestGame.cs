using System;

namespace Red7
{
    internal class TestGame
    {
        public Player[] Players { get; }
        public CardCollection Canvas { get; }
        public bool IsActive { get; set; }

        public Deck Deck { get; }
        private int activePlayers;

        public TestGame()
        {
            Deck = new Deck();
            Createdeck();
            activePlayers = 3;
            Players = new Player[activePlayers];
            Canvas = new CardCollection();
            AddPlayers();
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
        /// Adds all the players to the Players array and propts each one to enter a name
        /// </summary>
        private void AddPlayers()
        {
            Players[0] = new Player("ZAZ");
            Players[1] = new Player("DAVE");
            Players[2] = new Player("JEFF");
            foreach (Player p in Players)
                GivePlayerCards(p);

            SetTurnOrder();
        }

        /// <summary>
        /// Finds the <see cref="Player"/> in <see cref="Players"/> that is currently winning,
        /// and sets the next <see cref="Player"/> to take the first turn as specified in the rules
        /// by sorting the <see cref="Players"/> by the TurnOrder assigned to each <see cref="Player"/>. 
        /// </summary>
        private void SetTurnOrder()
        {
            Player startPlayer = Rules.WhoIsWinning(this);
            int turnOrder = 0;
            bool isFound = false;

            for(int i = 0; turnOrder < Players.Length; i++)
            {
                //makes array circular
                if (i == Players.Length)
                    i = 0;
                //player after current winning player goes first
                if(isFound)
                {
                    Players[i].TurnOrder = turnOrder;
                    turnOrder++;
                }
                if (Players[i].Equals(startPlayer))
                {
                    isFound = true;
                }
            }
            SortPlayersByTurnOrder();
        }

        /// <summary>
        /// Insert sort to sort the <see cref="Players"/> in order of lowest <see cref="Player.TurnOrder"/>
        /// to highest.
        /// </summary>
        private void SortPlayersByTurnOrder()
        {
            for (int i = 0; i < Players.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (Players[j - 1].TurnOrder > Players[j].TurnOrder)
                    {
                        Player temp = Players[j - 1];
                        Players[j - 1] = Players[j];
                        Players[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Gives each <see cref="Player"/> 7 cards to put into their hand to start the round and 1
        /// card to each <see cref="Player"/> Palette
        /// </summary>
        /// <param name="player">Player to give cards to</param>
        private void GivePlayerCards(Player player)
        {
            for (int i = 0; i < 8; i++)
                player.DrawFromDeck(Deck);
            player.PlayToPalette(0);
        }

        #endregion

        #region Player UI
        /// <summary>
        /// Prints the Pallets of all players to the Console.
        /// </summary>
        private void ShowPalettes(Player currentPlayer)
        {
            foreach (Player player in Players)
            {
                //print current player pallet in different place
                if (currentPlayer == player)
                    continue;

                if (!player.InPlay)
                    Console.Write(player.Name + " PALETTE: FORFITED\n");
                else
                {
                    Console.Write(player.Name + " PALETTE: ");
                    player.Palette.PrintCards(false);
                    Console.WriteLine();
                }
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
        /// 
        /// </summary>
        private void PlayerUI(Player player)
        {
            Console.Clear();
            Console.Write(player.Name + "'S TURN\n");

            ShowCanvasCard();
            ShowPalettes(player);

            //hand ordered to signify selection
            Console.Write("\nYOUR HAND: ");
            player.Hand.PrintCards(true);
            //palette unordered to signify no selection
            Console.Write("\nYOUR PALETTE: ");
            player.Palette.PrintCards(false);
        }
        #endregion

        #region PlayerMoves
        /// <summary>
        /// Prompts the user to input a int that represents the card they wish to play to the palette from their hand.
        /// </summary>
        /// <param name="player">Player to prompt and play card.</param>
        private static void PlayToPalette(Player player)
        {
            int cardNumber = GetNumberInput("CHOOSE A CARD TO PLAY TO THE PALETTE",
                1, player.Hand.GetNumberOfCards()) - 1;

            player.PlayToPalette(cardNumber);
        }

        /// <summary>
        /// Prompts the user to input a int that represents the card they wish to play to the canvas from their hand.
        /// </summary>
        /// <param name="player">Player to prompt and play card.</param>
        private void PlayToCanvas(Player player)
        {
            int cardNumber = GetNumberInput("CHOOSE A CARD TO PLAY TO THE CANVAS",
                1, player.Hand.GetNumberOfCards()) - 1;

            player.PlayToCanvas(cardNumber, Canvas);
        }

        /// <summary>
        /// Prompts the user to input a int that represents the <see cref="Card"/> they wish to play to the palette and from their hand.
        /// </summary>
        /// <param name="player">Player to prompt and play cards</param>
        private void PlayToBoth(Player player)
        {
            int cardToCanvas;
            int cardToPalette;
            bool cardsAreEqual = false;

            do
            {
                if (cardsAreEqual)
                    Console.WriteLine("CANNOT CHOOSE THE SAME CARD TO PLAY TO THE CANVAS AND PALETTE\n" +
                        "SELECT 2 UNIQUE CARDS");

                cardToPalette = GetNumberInput("CHOOSE A CARD TO PLAY TO THE PALETTE", 1, player.Hand.GetNumberOfCards()) - 1;
                cardToCanvas = GetNumberInput("CHOOSE A CARD TO PLAY TO THE CANVAS", 1, player.Hand.GetNumberOfCards()) - 1;

                if (cardToCanvas == cardToPalette)
                    cardsAreEqual = true;
                else
                    cardsAreEqual = false;
            }
            while (cardsAreEqual);

            //cards are played by index, thus play the largest index to not change the others index
            if (cardToCanvas > cardToPalette)
            {
                player.PlayToCanvas(cardToCanvas, Canvas);
                player.PlayToPalette(cardToPalette);
            }
            else
            {
                player.PlayToPalette(cardToPalette);
                player.PlayToCanvas(cardToCanvas, Canvas);
            }
        }

        /// <summary>
        /// Changes the players InPlay property to false indicating a surrender
        /// </summary>
        /// <param name="player">Player to surrender</param>
        private void Surrender(Player player)
        {
            player.InPlay = false;
            activePlayers--;
        }
        #endregion

        #region Turn Logic
        /// <summary>
        /// Itterates through players and prompts each player to take their turn.
        /// </summary>
        public void PlayTurn()
        {
            foreach (Player player in Players)
            {
                //Checks if game is over, it if has exits method
                if (HasFinished())
                    return;

                //If player has forfitted go onto next player
                if (player.InPlay)
                {
                    Console.Clear();
                    Console.WriteLine("\nIT'S " + player.Name + "'S TURN! PRESS ANY KEY TO ADVANCE\n");
                    Console.ReadKey();

                    PlayerUI(player);
                    int turnType = TurnType(player);
                    while (!IsLegalMove(player) && player.InPlay)
                    {
                        UndoMove(player, turnType);
                        PlayerUI(player);
                        turnType = TurnType(player);
                    }
                }
                else
                {
                    Console.WriteLine("\nPLAYER " + player.Name + " HAS BEEN ELIMINATED PRESS ENTER TO CONTINUE\n");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Prompts user to pick an number from 1-4 which represents an action and then executes the action.
        /// <para>Where 1: Play to Palette, 2: Play to Canvas, 3: Play to Both, 4: Surrender.</para>
        /// </summary>
        /// <param name="player"></param>
        private int TurnType(Player player)
        {
            //cannot use GetNumberInput to directly assign turnType as if the user only has 1 card then they
            //cannot play to both the palette and canvas - thus must check
            int turnType;
            do
            {
                turnType = GetNumberInput(
                    "\nPLAY TO: PALLET (1), CANVAS (2), BOTH (3) OR SURRENDER (4)",
                    1, 4);
            }
            while (turnType == 3 && player.Hand.GetNumberOfCards() < 2);

            switch (turnType)
            {
                case 1:
                    PlayToPalette(player);
                    break;

                case 2:
                    PlayToCanvas(player);
                    break;

                case 3:
                    PlayToBoth(player);
                    break;

                case 4:
                    Surrender(player);
                    break;
            }

            return turnType;
        }

        /// <summary>
        /// Undoes the actions of a <see cref="Player"/> by taking the most recently played <see cref="Card"/> to the 
        /// (<see cref="CardCollection"/>) Canvas/Palette, then returning them to the Hand of the <see cref="Player"/>
        /// </summary>
        /// <param name="player">Player to undo actions of</param>
        /// <param name="turnType"></param>
        public void UndoMove(Player player, int turnType)
        {
            Console.WriteLine("\nILLEGAL MOVE, PRESS ANY KEY TO CONTINUE");
            Console.ReadKey();

            switch (turnType)
            {
                case 1:
                    player.ReturnPaletteToHand();
                    break;

                case 2:
                    player.ReturnCanvasToHand(Canvas);
                    break;

                case 3:
                    player.ReturnPaletteToHand();
                    player.ReturnCanvasToHand(Canvas);
                    break;
            }
        }
        #endregion

        #region GameState

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

        /// <summary>
        /// Returns a <see cref="bool"/> representing if the game has finished.
        /// </summary>
        /// <returns>true if the game has finished, false if not.</returns>
        public bool HasFinished()
        {
            return activePlayers == 1;
        }

        /// <summary>
        /// Checks if a action taken by a player is legal.
        /// <para>A legal move is one that makes the player winning by the top card in the canvas <see cref="Colours"/> rule.</para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="turnType"></param>
        /// <returns><see cref="bool"/>: true if the move is legal, false if not.</returns>
        public bool IsLegalMove(Player player)
        {
            return Rules.WhoIsWinning(this).Equals(player);
        }

        #endregion

        /// <summary>
        /// Prompts the user to select a number between two numbers, the returns the user input number if valid.
        /// If the user does not enter a valid input, user is reprompted to chose another valid value.
        /// </summary>
        /// <param name="prompt">string to write to the console to notify the user of their choices</param>
        /// <param name="min">int representing the first choice of the user</param>
        /// <param name="max">int representing the last choice of the user</param>
        /// <returns><see cref="int"/> user selected</returns>
        private static int GetNumberInput(string prompt, int min, int max)
        {
            int num;
            do
            {
                Console.WriteLine(prompt);
                _ = int.TryParse(Console.ReadLine(), out num);
            }
            while (num < min || num > max);
            return num;
        }
    }
}
