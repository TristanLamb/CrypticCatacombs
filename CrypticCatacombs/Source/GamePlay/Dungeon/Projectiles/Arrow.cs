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


		public Arrow(Vector2 POS, Unit OWNER, Vector2 TARGET)
			: base("2d/Projectiles/fireball", POS, new Vector2(100, 100), OWNER, TARGET)
		{
			speed = 4.0f;
			timer = new CustomTimer(1800);
		}

		//testing collision
		public override void Update(Vector2 OFFSET, List<Unit> UNITS)
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