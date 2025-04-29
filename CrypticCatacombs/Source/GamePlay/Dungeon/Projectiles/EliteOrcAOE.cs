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

	public class EliteOrcAOE : Projectile2d
	{
		private CustomTimer projectileTimer;
		private float rotationSpeed = 5f;

		public EliteOrcAOE(Vector2 POS, AttackableObject OWNER, Vector2 TARGET)
			: base("2d/Projectiles/EliteOrcAttackAOE", POS, new Vector2(150, 150), OWNER, TARGET)
		{
			speed = 2.5f;
			timer = new CustomTimer(3000);

			rotation = (float)Math.Atan2(TARGET.Y - POS.Y, TARGET.X - POS.X);

			projectileTimer = new CustomTimer(700);

		}

		//testing collision
		public override void Update(Vector2 OFFSET, List<AttackableObject> UNITS)
		{
			projectileTimer.UpdateTimer();

			// Rotate the projectile to create a circular motion effect around the target
			if (projectileTimer.Test()) // This checks if the timer has reached its target time (1000ms or 1 second)
			{
				// Move the projectile in a circular pattern relative to its starting position
				float angle = rotation + rotationSpeed;
				direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
				SetDirection(direction);

				// Optionally, reset the timer after each movement or effect if you want it to keep moving in steps
				projectileTimer.ResetToZero();
			}




			base.Update(OFFSET, UNITS);

		}

		public void SetDirection(Vector2 direction)
		{
			this.direction = direction;
			// You could scale the direction by speed or apply it to update the position each frame
		}


		//trails or effects for projectiles
		public override void Draw(Vector2 OFFSET)
		{
			base.Draw(OFFSET);
		}
	}
}