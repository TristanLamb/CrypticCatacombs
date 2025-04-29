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
    public class Slime : Mob
    {
		private SpriteEffects spriteEffect = SpriteEffects.None;
		private Texture2D idleTexture;
		private Texture2D walkTexture;
		public Slime(Vector2 POS, Vector2 FRAMES, int OWNERID)
            : base("2d/Units/Mobs/SlimeIdle", POS, new Vector2(150, 150), new Vector2(6, 1), OWNERID)
        {
            speed = 1.75f;
            health = 2;
			//frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 6, 66, 0, "Walk"));

			idleTexture = Globals.content.Load<Texture2D>("2d/Units/Mobs/SlimeIdle");
			walkTexture = Globals.content.Load<Texture2D>("2d/Units/Mobs/SlimeWalk");

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
        /*
        public override void AI(Player ENEMY, SquareGrid GRID)
        {
            if(pathNodes == null || (pathNodes.Count == 0 && pos.X == moveTo.X && pos.Y == moveTo.Y))
            {
                pathNodes = FindPath(GRID, GRID.GetSlotFromPixel(ENEMY.wizard.pos, Vector2.Zero));
                moveTo = pathNodes[0];
                pathNodes.RemoveAt(0);

				//pos += Globals.RadialMovement(ENEMY.wizard.pos, pos, speed);
			}
            else
            {
                MoveUnit();

				if (Globals.GetDistance(pos, ENEMY.wizard.pos) < GRID.slotDims.X * 1.2f)
				{
					ENEMY.wizard.GetHit(this, 1);
					dead = true;
				}
			}
            
        }
        */


		public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
