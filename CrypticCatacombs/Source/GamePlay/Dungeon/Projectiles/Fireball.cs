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

	public class Fireball : Projectile2d
	{


		public Fireball(Vector2 POS, AttackableObject OWNER, Vector2 TARGET)
			: base("2d/Projectiles/fireball", POS, new Vector2(100, 100), OWNER, TARGET)
		{
			timer = new CustomTimer(900);
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