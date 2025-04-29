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
	public class FireCircle : Effect2d
	{
		public FireCircle(Vector2 POS, Vector2 DIMS, int MSEC)
			: base("2d/Projectiles/FlameWall", POS, new Vector2(250, 250), new Vector2(5, 1), MSEC)
		{
			/*
			frameAnimations = true;
			currentAnimation = 0;
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), new Vector2(0, 0), new Vector2(0, 0), 5, 66, 0, "FlameWall"));
			SetAnimationByName("FlameWall");
			*/

		}

		public override void Update(Vector2 OFFSET)
		{
			//rotation += (float)Math.PI * 2.0f / 60.0f;
			
			base.Update(OFFSET);
		}
	}
}
