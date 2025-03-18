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
    public class Mob : Unit
    {
		private SpriteEffects spriteEffect = SpriteEffects.None;

		public Mob(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) 
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            AI(ENEMY.wizard);

			base.Update(OFFSET);
        }


        public virtual void AI(Wizard WIZARD)
        {
            pos += Globals.RadialMovement(WIZARD.pos, pos, speed);
			//rotation = Globals.RotateTowards(pos, WIZARD.pos); //used to rotate towards player
			if (WIZARD.pos.X < pos.X)
				spriteEffect = SpriteEffects.FlipHorizontally; // Face left
			else
				spriteEffect = SpriteEffects.None; // Face right

			if (!dead && Globals.GetDistance(pos, WIZARD.pos) < 15)
            {
                WIZARD.GetHit(this, 1);
                dead = true;
            }
        }

        public override void Draw(Vector2 OFFSET, SpriteEffects spriteEffects)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
