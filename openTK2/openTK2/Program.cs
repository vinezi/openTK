using System;

namespace openTK2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Politov2_OpenTK"))
            {
                game.Run(120.0);

            }
        }
    }
}
