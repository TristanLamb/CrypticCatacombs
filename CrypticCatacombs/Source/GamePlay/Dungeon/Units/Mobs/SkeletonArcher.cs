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
    public class SkeletonArcher : Mob
    {
		private SpriteEffects spriteEffect = SpriteEffects.None;
		private Texture2D idleTexture;
		private Texture2D walkTexture;
		public SkeletonArcher(Vector2 POS, Vector2 FRAMES, int OWNERID)
            : base("2d/Units/Mobs/SkeletonArcherIdle", POS, new Vector2(150, 150), new Vector2(6, 1), OWNERID)
        {
            speed = 1.0f;
			attackRange = 400;
			health = 5;
			attackTimer = new CustomTimer(1500);
			killValue = 2;

			idleTexture = Globals.content.Load<Texture2D>("2d/Units/Mobs/SkeletonArcherIdle");
			walkTexture = Globals.content.Load<Texture2D>("2d/Units/Mobs/SkeletonArcherWalk");

			frameAnimations = true;
			currentAnimation = 0;
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 6, 198, 0, "Walk"));
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 6, 198, 0, "Idle"));
		}

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {

			bool wasMoving = pos != moveTo;

			base.Update(OFFSET, ENEMY, GRID);

			bool isMoving = pos != moveTo;

			if (isMoving && !isAttacking)
			{
				if (myModel != walkTexture)
				{
					myModel = walkTexture;
					SetAnimationByName("Walk");
				}
			}
			else
			{
				if (myModel != idleTexture)
				{
					myModel = idleTexture;
					SetAnimationByName("Idle");
				}
			}

		}

		public override void AI(Player ENEMY, SquareGrid GRID)
		{
			//fix with new list of units
			if (ENEMY.wizard != null && (Globals.GetDistance(pos, ENEMY.wizard.pos) < attackRange * .8f || isAttacking))
            {
				isAttacking = true;

				attackTimer.UpdateTimer();

				if(attackTimer.Test())
				{
					GameGlobals.PassProjectile(new Arrow(new Vector2(pos.X, pos.Y), this, new Vector2(ENEMY.wizard.pos.X, ENEMY.wizard.pos.Y)));

					attackTimer.ResetToZero();
					isAttacking = false;
				}
            }
            else
            {
				base.AI(ENEMY, GRID);
			}
			
		}


		public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
