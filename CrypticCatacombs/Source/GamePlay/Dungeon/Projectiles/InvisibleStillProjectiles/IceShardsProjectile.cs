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

	public class IceShardsProjectile : Projectile2d
	{

		public IceShardsProjectile(Vector2 POS, Unit OWNER, Vector2 TARGET, int MSEC)
			: base("2d/Projectiles/IceShards", POS, new Vector2(120, 120), OWNER, TARGET)
		{
			speed = 12.0f;
			Vector2 direction = TARGET - POS;
			direction.Normalize();
			this.direction = direction;
		}

		//testing collision
		public override void Update(Vector2 OFFSET, List<AttackableObject> UNITS)
		{
			pos += direction * speed;
			HitSomething(UNITS);
			base.Update(OFFSET, UNITS);
		}

		public override void ChangePosition()
		{
			
		}

		//trails or effects for projectiles
		public override void Draw(Vector2 OFFSET)
		{
			base.Draw(OFFSET);
		}
	}
}