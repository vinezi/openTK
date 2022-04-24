using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openGLFirstTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Politov_OpenTK"))
            {
                //Указана частота обновления 60 раз/секунду. Если опустить – 
                //будет использоваться     максимально-возможная.
                game.Run(60.0);

            }


        }



    }
}
