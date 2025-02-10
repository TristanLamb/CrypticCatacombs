using System;

namespace Cryptic_Catacombs
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new Game1();
                game.Run();
        }
    }
}

