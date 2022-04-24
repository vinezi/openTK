using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openTK4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Politov4_OpenTK"))
            {
                game.Run(120.0);
            }
        }
    }
}
