using System;

namespace Red7
{
    internal class Program
    {
        static void Main(string[] args)
            {
            Game game = new Game();
            
            while(true)
               game.PlayTurn();


            Console.ReadLine();
        }
    }
}
