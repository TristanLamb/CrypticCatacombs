<<<<<<< HEAD
﻿using System;

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

=======
﻿
using var game = new Cryptic_Catacombs.Game1();
game.Run();
>>>>>>> 1b8b20c (Adding Starting Files)
