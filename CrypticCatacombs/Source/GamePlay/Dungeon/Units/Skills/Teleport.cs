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
using CrypticCatacombs.Source.GamePlay.Dungeon.Effects;

namespace CrypticCatacombs
{
    class Teleport : Skill
    {

        public Teleport(AttackableObject OWNER)
            : base(OWNER)
        {

            //icon = new Animated2d("2d/Misc/Teleport", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), Color.White);
        }

		public override void Targetting(Vector2 OFFSET, Player ENEMY)
		{

			GameGlobals.PassEffect(new TeleportEffect(Globals.mouse.newMousePos - OFFSET, new Vector2(owner.dims.X, owner.dims.Y), 266));
			GameGlobals.PassEffect(new TeleportEffect(new Vector2(owner.pos.X, owner.pos.Y), new Vector2(owner.dims.X, owner.dims.Y), 266));

			owner.pos = new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET;

            done = true;
            active = false;
		}

	}
}
