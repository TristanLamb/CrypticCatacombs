using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CrypticCatacombs
{
    public class GamePlay
    {
        int gameState;

        Dungeon dungeon;

        public GamePlay()
        {
            gameState = 0; //game playing

            ResetDungeon(null);
        }

		public virtual void Update()
        {
            if(gameState == 0)
            {
                dungeon.Update();
            }
        }

        public virtual void ResetDungeon(object INFO)
        {
            dungeon = new Dungeon(ResetDungeon);
        }

        public virtual void Draw()
        {
            if(gameState == 0)
            {
                dungeon.Draw(Vector2.Zero);
            }    
        }


    }
}
