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

	public class InvisibleStillProjectile : Projectile2d
	{
		float ticks, currentTicks;

		public InvisibleStillProjectile(Vector2 POS, Vector2 DIMS, Unit OWNER, Vector2 TARGET, int MSEC)
			: base("2d/Misc/solid", POS, DIMS, OWNER, TARGET)
		{
			ticks = 3;
			currentTicks = 0;

			timer = new CustomTimer(MSEC);
		}

		public override void Update(Vector2 OFFSET, List<Unit> UNITS)
		{
			base.Update(OFFSET, UNITS);


			if (timer.Timer >= timer.MSec * (currentTicks / (ticks - 1))) //3 ticks
			{
				for (int i = 0; i < UNITS.Count; i++)
				{
					if (Globals.GetDistance(UNITS[i].pos, pos) <= dims.X / 2)
					{
						UNITS[i].GetHit(owner, 1.0f);
					}
				}
				currentTicks++;
			}
		}

		public override void ChangePosition()
		{
			
		}

		public override bool HitSomething(List<Unit> UNITS)
		{
			return false;
		}

		public override void Draw(Vector2 OFFSET)
		{
			
		}
	}
}