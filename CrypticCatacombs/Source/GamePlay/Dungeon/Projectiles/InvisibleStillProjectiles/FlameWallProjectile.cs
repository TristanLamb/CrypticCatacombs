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

	public class FlameWallProjectile : InvisibleStillProjectile
	{

		public FlameWallProjectile(Vector2 POS, Unit OWNER, Vector2 TARGET, int MSEC)
			: base(POS, new Vector2(200, 200), OWNER, TARGET, MSEC)
		{
			GameGlobals.PassEffect(new FireCircle(new Vector2(POS.X, POS.Y), new Vector2(dims.X, dims.Y), MSEC));
		}

		//testing collision
		public override void Update(Vector2 OFFSET, List<AttackableObject> UNITS)
		{
			base.Update(OFFSET, UNITS);
		}

		public override void ChangePosition()
		{
			
		}

		public override bool HitSomething(List<AttackableObject> UNITS)
		{
			return false;
		}

		//trails or effects for projectiles
		public override void Draw(Vector2 OFFSET)
		{
			
		}
	}
}