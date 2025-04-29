using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrypticCatacombs
{


    public class GameGlobals
    {
        public static bool paused = false, nextRoom = false, enemiesDefeated = false;

        public static float score = 0.0f;

        public static KeyBindList keyBinds;

        public static PassObject PassProjectile, PassGold, PassEffect, PassMob, PassSpawnPoint, CheckScroll;

        public static Map map;

        public static User user;

        public static int currentLevel = 1, roomsCompleted = 1;

        public static int currentMapLayout = 0;
	}
}
