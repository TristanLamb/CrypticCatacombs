using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using CrypticCatacombs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrypticCatacombs
{

	public class Arrow : Projectile2d
	{


		public Arrow(Vector2 POS, AttackableObject OWNER, Vector2 TARGET)
			: base("2d/Projectiles/Arrow", POS, new Vector2(50, 50), OWNER, TARGET)
		{
			speed = 5.0f;
			timer = new CustomTimer(1800);
			rotation = (float)Math.Atan2(TARGET.Y - POS.Y, TARGET.X - POS.X);
		}

		//testing collision
		public override void Update(Vector2 OFFSET, List<AttackableObject> UNITS)
		{
			base.Update(OFFSET, UNITS);
		}

		//trails or effects for projectiles
		public override void Draw(Vector2 OFFSET)
		{
			base.Draw(OFFSET);
		}
	}
}