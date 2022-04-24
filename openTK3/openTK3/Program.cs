namespace openTK3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Politov3_OpenTK"))
            {
                game.Run(120.0);
            }
        }
    }
}
