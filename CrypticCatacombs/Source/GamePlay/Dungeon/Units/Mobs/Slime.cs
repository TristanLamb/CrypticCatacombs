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
    public class Slime : Mob
    {
        public Slime(Vector2 POS, int OWNERID)
            : base("2d/Units/Mobs/SlimePng", POS, new Vector2(150, 150), OWNERID)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            

			base.Update(OFFSET, ENEMY);
        }


        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
