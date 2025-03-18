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
    class FlameWall : Skill
    {

        public FlameWall(AttackableObject OWNER)
            : base(OWNER)
        {

            
        }

		public override void Targetting(Vector2 OFFSET, Player ENEMY)
		{
			if (Globals.mouse.LeftClickRelease())
			{
				targetEffect.done = true;

				GameGlobals.PassProjectile(new FlameWallProjectile(Globals.mouse.newMousePos - OFFSET, (Unit)owner, new Vector2(0, 0), 500));

				done = true;
				active = false;
			}
			else
			{
				targetEffect.pos = Globals.mouse.newMousePos - OFFSET;

			}
		}

	}
}
