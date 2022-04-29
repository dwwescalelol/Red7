using System;

namespace Red7
{
    internal class Program
    {
        static void Main(string[] args)
            {
            TestGame game = new TestGame();
            
            while(!game.HasFinished())
                game.PlayTurn();

            Console.WriteLine(Rules.WhoIsWinning(game).Name);

            //Console.WriteLine(game.WhoWon().Name + " has won! Congrats!");

            //Console.ReadLine();
        }
    }
}
