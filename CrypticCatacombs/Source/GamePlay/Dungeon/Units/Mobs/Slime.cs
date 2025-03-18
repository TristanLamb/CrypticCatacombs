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
		private SpriteEffects spriteEffect = SpriteEffects.None;
		public Slime(Vector2 POS, Vector2 FRAMES, int OWNERID)
            : base("2d/Units/Mobs/SlimePng", POS, new Vector2(150, 150), new Vector2(6, 1), OWNERID)
        {
            speed = 2.0f;
            health = 2;
			//frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 6, 66, 0, "Walk"));
		}

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {

			//SetAnimationByName("Walk");
			base.Update(OFFSET, ENEMY);

        }


        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
