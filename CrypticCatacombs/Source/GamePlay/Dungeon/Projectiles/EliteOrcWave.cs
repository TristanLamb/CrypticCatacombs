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

	public class EliteOrcWave : Projectile2d
	{


		public EliteOrcWave(Vector2 POS, AttackableObject OWNER, Vector2 TARGET)
			: base("2d/Projectiles/EliteOrcAttackWave", POS, new Vector2(150, 150), OWNER, TARGET)
		{
			speed = 5.5f;
			timer = new CustomTimer(2800);
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