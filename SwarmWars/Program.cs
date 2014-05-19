using System;

namespace SwarmWars
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SwarmWars game = new SwarmWars())
            {
                game.Run();
            }
        }
    }
}

