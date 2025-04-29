using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrypticCatacombs
{
    public class Mob : Unit
    {
        public bool isAttacking, currentlyPathing;

        public float attackRange;

        public CustomTimer rePathTimer = new CustomTimer(200), attackTimer = new CustomTimer(350);

		private SpriteEffects spriteEffect = SpriteEffects.None;

		private CustomTimer damageTimer;

		public Mob(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) 
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
			currentlyPathing = false;
            attackRange = 50;
			isAttacking = false;
			speed = 2.0f;

			damageTimer = new CustomTimer(1500); //time until next damage tick
		}

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
            AI(ENEMY, GRID);

			base.Update(OFFSET, ENEMY, GRID);
        }


        public virtual void AI(Player ENEMY, SquareGrid GRID)
        {
			rePathTimer.UpdateTimer();
			damageTimer.UpdateTimer();

			if (ENEMY.wizard.pos.X < pos.X)
			{
				spriteEffect = SpriteEffects.FlipHorizontally; // Face left
			}
			else
			{
				spriteEffect = SpriteEffects.None; // Face right
			}

			if (pathNodes == null || (pathNodes.Count == 0 && pos.X == moveTo.X && pos.Y == moveTo.Y) || rePathTimer.Test())
			{
				if(!currentlyPathing)
				{
					Task repathTask = new Task(() =>
					{
						currentlyPathing = true;

						pathNodes = FindPath(GRID, GRID.GetSlotFromPixel(ENEMY.wizard.pos, Vector2.Zero));
						moveTo = pathNodes[0];
						pathNodes.RemoveAt(0);

						rePathTimer.ResetToZero();

						currentlyPathing = false;
					});

					repathTask.Start();
				}
				
			}
			else
			{
				MoveUnit();

				if (Globals.GetDistance(pos, ENEMY.wizard.pos) < GRID.slotDims.X * 1.2f)
				{
					if (damageTimer.Test())
					{
						ENEMY.wizard.GetHit(this, 1);
						damageTimer.ResetToZero();
					}

					if (this is Slime || this is SkeletonArcher)
					{
						dead = true;
					}
				}
			}


		}

        public override void Draw(Vector2 OFFSET, SpriteEffects spriteEffects)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
