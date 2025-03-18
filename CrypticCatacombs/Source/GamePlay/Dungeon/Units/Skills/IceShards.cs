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
    class IceShards : Skill
    {

        public IceShards(AttackableObject OWNER)
            : base(OWNER)
        {

            
        }

		public override void Update(Vector2 OFFSET, Player ENEMY)
		{
			if (active)
            {
				Vector2 pos = owner.pos;
				Vector2 target = Globals.mouse.newMousePos - OFFSET;
				GameGlobals.PassProjectile(new IceShardsProjectile(pos, (Unit)owner, target, 500));
				done = true;
                active = false;
			}
		}

	}
}
