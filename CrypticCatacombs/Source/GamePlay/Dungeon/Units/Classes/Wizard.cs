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
    public class Wizard : Unit
    {
        private SpriteEffects spriteEffect = SpriteEffects.None;
        public Wizard(string PATH, Vector2 POS, Vector2 DIMS, int OWNERID)
            : base(PATH, POS, DIMS, OWNERID)
        {
            speed = 2.0f;

			health = 5;
			healthMax = health;
		}

        public override void Update(Vector2 OFFSET)
        {
            bool checkScroll = false;

			if (Globals.keyboard.GetPress("A"))
            {
                pos = new Vector2(pos.X - speed, pos.Y);
				spriteEffect = SpriteEffects.FlipHorizontally;
				checkScroll = true;
			}

            if (Globals.keyboard.GetPress("D"))
            {
                pos = new Vector2(pos.X + speed, pos.Y);
				spriteEffect = SpriteEffects.None;
				checkScroll = true;
			}

            if (Globals.keyboard.GetPress("W"))
            {
                pos = new Vector2(pos.X, pos.Y - speed);
				checkScroll = true;
			}

            if (Globals.keyboard.GetPress("S"))
            {
                pos = new Vector2(pos.X, pos.Y + speed);
				checkScroll = true;
			}

			//rotation = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET); //to add rotation to follow mouse
			

			if (Globals.mouse.LeftClick() || Globals.keyboard.GetPress("Space"))
            {
                GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
            }

            if(checkScroll)
            {
                GameGlobals.CheckScroll(pos);
            }


            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
