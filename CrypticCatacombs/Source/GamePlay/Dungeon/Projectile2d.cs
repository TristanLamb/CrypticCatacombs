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

	public class Projectile2d : Basic2d
	{

		public bool done;

		public float speed;

		public Vector2 direction;

		public AttackableObject owner;

		public CustomTimer timer;

		public Projectile2d(string PATH, Vector2 POS, Vector2 DIMS, AttackableObject OWNER, Vector2 TARGET)
			: base(PATH, POS, DIMS)
		{
			done = false;

			speed = 5.0f;

			owner = OWNER;

			direction = TARGET - owner.pos;
			direction.Normalize();

			rotation = Globals.RotateTowards(pos, new Vector2(TARGET.X, TARGET.Y));

			timer = new CustomTimer(1200);
		}

		//testing collision
		public virtual void Update(Vector2 OFFSET, List<AttackableObject> UNITS)
		{
			ChangePosition();
			timer.UpdateTimer();
			if(timer.Test())
			{

				done = true;
			}

			//used for hitting mobs, deciding if it hit
			if(HitSomething(UNITS))
			{
				done = true;
			}	
		}

		public virtual void ChangePosition()
		{
			pos += direction * speed;
		}

		public virtual bool HitSomething(List<AttackableObject> UNITS)
		{
			for (int i = 0; i < UNITS.Count; i++)
			{

				if(owner.ownerId != UNITS[i].ownerId && Globals.GetDistance(pos, UNITS[i].pos) < UNITS[i].hitDist)
				{
					UNITS[i].GetHit(owner, 1);
					return true; //removes projectile
				}
			}
			return false;
		}

		//trails or effects for projectiles
		public override void Draw(Vector2 OFFSET)
		{
			base.Draw(OFFSET);
		}
	}
}