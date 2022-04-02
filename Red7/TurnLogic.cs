using System;

namespace Red7
{
    internal partial class Game
    {
        /// <summary>
        /// Prompts the user to select a number between two numbers, the returns the user input number if valid.
        /// </summary>
        /// <param name="prompt">string to write to the console to notify the user of their choices</param>
        /// <param name="min">int representing the first choice of the user</param>
        /// <param name="max">int representing the last choice of the user</param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        private void TurnType(Player player)
        {
            //cannot use GetNumberInput to directly assign turnType as if the user only has 1 card then they
            //cannot play to both the palette and canvas - thus must check
            int turnType;
            do
            {
                turnType = GetNumberInput(
                    "PLAY TO: PALLET (1), CANVAS (2), BOTH (3) OR SURRENDER (4)",
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
        }

        /// <summary>
        /// Prompts the user to input a int that represents the card they wish to play to the palette from their hand
        /// </summary>
        /// <param name="player">Player to prompt and play card</param>
        private static void PlayToPalette(Player player)
        {
            int cardNumber;
            do
            {
                Console.WriteLine("CHOOSE A CARD TO PLAY TO THE PALETTE");
                _ = int.TryParse(Console.ReadLine(), out cardNumber);
            }
            while (cardNumber < 1 || cardNumber > player.Hand.GetNumberOfCards());

            player.PlayToPalette(cardNumber);
        }

        /// <summary>
        /// Prompts the user to input a int that represents the card they wish to play to the canvas from their hand
        /// </summary>
        /// <param name="player">Player to prompt and play card</param>
        private void PlayToCanvas(Player player)
        {
            int cardNumber;
            do
            {
                Console.WriteLine("CHOOSE A CARD TO PLAY TO THE PALETTE");
                _ = int.TryParse(Console.ReadLine(), out cardNumber);
            }
            while (cardNumber < 1 || cardNumber > player.Hand.GetNumberOfCards());

            player.PlayToCanvas(cardNumber, Canvas);
        }

        /// <summary>
        /// Prompts the user to input a int that represents the card they wish to play to the palette and from their hand
        /// </summary>
        /// <param name="player">Player to prompt and play cards</param>
        private void PlayToBoth(Player player)
        {
            int cardToCanvas = GetNumberInput("CHOOSE A CARD TO PLAY TO THE PALETTE",
                1, player.Hand.GetNumberOfCards());

            int cardToPalette;
            do
            {
                cardToPalette = GetNumberInput("CHOOSE A CARD TO PLAY TO THE PALETTE",
                    1, player.Hand.GetNumberOfCards());
            }
            while (cardToCanvas == cardToPalette);

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
        }
    }
}
