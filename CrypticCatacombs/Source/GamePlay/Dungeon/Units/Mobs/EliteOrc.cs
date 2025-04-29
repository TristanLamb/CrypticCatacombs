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
    public class EliteOrc : Mob
    {
		private SpriteEffects spriteEffect = SpriteEffects.None;
		private Texture2D idleTexture;
		private Texture2D walkTexture;
		private Texture2D attackAOE;
		private Texture2D attackWave;
		private Texture2D attackMelee;
		bool isUsingAOE;
		bool isUsingWave;
		bool isUsingMelee;

		private int aoeAttackCount = 0;
		private int maxAoeAttacks = 3;
		private bool aoeOnCooldown = false;
		private CustomTimer aoeCooldownTimer = new CustomTimer(7000); // 3 seconds cooldown for aoe

		private int waveAttackCount = 0;
		private int maxWaveAttacks = 4;
		private bool waveOnCooldown = false;
		private CustomTimer waveCooldownTimer = new CustomTimer(4000); // 5 seconds cooldown for wave

		private int meleeAttackCount = 0;  // Melee attack counter
		private bool meleeOnCooldown = false;  // Melee cooldown
		private CustomTimer meleeCooldownTimer = new CustomTimer(5000);



		public EliteOrc(Vector2 POS, Vector2 FRAMES, int OWNERID)
            : base("2d/Units/Mobs/EliteOrcIdle", POS, new Vector2(275, 275), new Vector2(6, 1), OWNERID)
        {
            speed = 0.8f;
			attackRange = 300;
			health = 50;
			attackTimer = new CustomTimer(1200);

			idleTexture = Globals.content.Load<Texture2D>("2d/Units/Mobs/EliteOrcIdle");
			walkTexture = Globals.content.Load<Texture2D>("2d/Units/Mobs/EliteOrcWalk");
			attackAOE = Globals.content.Load<Texture2D>("2d/Units/Mobs/EliteOrcAOE");
			attackWave = Globals.content.Load<Texture2D>("2d/Units/Mobs/EliteOrcWave");
			attackMelee = Globals.content.Load<Texture2D>("2d/Units/Mobs/EliteOrcMelee");

			frameAnimations = true;
			currentAnimation = 0;
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), new Vector2(6, 1), new Vector2(0, 0), 6, 132, 0, "Walk"));
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), new Vector2(6, 1), new Vector2(0, 0), 6, 132, 0, "Idle"));
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), new Vector2(11, 1), new Vector2(0, 0), 11, 132, 0, "AttackAOE"));
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), new Vector2(9, 1), new Vector2(0, 0), 9, 132, 0, "AttackWave"));
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), new Vector2(7, 1), new Vector2(0, 0), 7, 132, 0, "AttackMelee"));
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
			else if (isUsingAOE && isAttacking)
			{
				myModel = attackAOE;
				SetAnimationByName("AttackAOE");
			}
			else if (isUsingWave && isAttacking)
			{
				myModel = attackWave;
				SetAnimationByName("AttackWave");
			}
			else if (isUsingMelee && isAttacking)
			{
				myModel = attackMelee;
				SetAnimationByName("AttackMelee");
			}
			else
			{
				if (myModel != idleTexture)
				{
					myModel = attackMelee;
					SetAnimationByName("Idle");
				}
			}


		}

		public override void AI(Player ENEMY, SquareGrid GRID)
		{
			if (aoeOnCooldown)
			{
				aoeCooldownTimer.UpdateTimer();
				if (aoeCooldownTimer.Test())
				{
					aoeOnCooldown = false;
					aoeAttackCount = 0;
				}
			}
			if (waveOnCooldown)
			{
				waveCooldownTimer.UpdateTimer();
				if (waveCooldownTimer.Test())
				{
					waveOnCooldown = false;
					waveAttackCount = 0;
				}
			}
			if (meleeOnCooldown)
			{
				meleeCooldownTimer.UpdateTimer();
				if (meleeCooldownTimer.Test())
				{
					meleeOnCooldown = false;
					meleeAttackCount = 0;
				}
			}

			
			//EliteOrcAOE
			if (!aoeOnCooldown && ENEMY.wizard != null && (Globals.GetDistance(pos, ENEMY.wizard.pos) < attackRange * .9f))
			{
				isUsingMelee = false;
				isUsingWave = false;
				isUsingAOE = true;
				isAttacking = true;

				attackTimer.UpdateTimer();

				if (attackTimer.Test())
				{
					int numProjectiles = 12;
					float angleIncrement = MathHelper.TwoPi / numProjectiles;


					for (int i = 0; i < numProjectiles; i++)
					{
						float angle = i * angleIncrement;
						angle -= MathHelper.PiOver2;
						Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
						Vector2 projectilePosition = new Vector2(pos.X + direction.X * 10, pos.Y + direction.Y * 10);

						EliteOrcAOE projectile = new EliteOrcAOE(projectilePosition, this, projectilePosition);
						projectile.rotation = angle;
						GameGlobals.PassProjectile(projectile);
					}

					aoeAttackCount++;
					attackTimer.ResetToZero();

					if (aoeAttackCount >= maxAoeAttacks)
					{
						aoeOnCooldown = true;
						aoeCooldownTimer.ResetToZero();
					}

					isUsingAOE = false;
					isAttacking = false;
				}
			}

			//EliteOrcWave
			else if (!waveOnCooldown && ENEMY.wizard != null && (Globals.GetDistance(pos, ENEMY.wizard.pos) >= attackRange * .55f && (Globals.GetDistance(pos, ENEMY.wizard.pos) <= attackRange * 1.2f)))
			{
				isUsingMelee = false;
				isUsingAOE = false;
				isUsingWave = true;
				isAttacking = true;

				attackTimer.UpdateTimer();

				if (attackTimer.Test())
				{
					GameGlobals.PassProjectile(new EliteOrcWave(new Vector2(pos.X, pos.Y), this, new Vector2(ENEMY.wizard.pos.X, ENEMY.wizard.pos.Y)));
					
					waveAttackCount++;
					attackTimer.ResetToZero();

					if (waveAttackCount >= maxWaveAttacks)
					{
						waveOnCooldown = true;
						waveCooldownTimer.ResetToZero();
					}

					isUsingWave = false;
					isAttacking = false;
				}

			}
			
			// Melee attack if within range
			else if (!meleeOnCooldown && ENEMY.wizard != null && (Globals.GetDistance(pos, ENEMY.wizard.pos) < attackRange * .3f))
			{
				isUsingAOE = false;
				isUsingWave = false;
				isUsingMelee = true;
				isAttacking = true;

				attackTimer.UpdateTimer();

				if (attackTimer.Test())
				{
					// Here you can apply melee damage or effects (like GetHit or other effects)
					ENEMY.wizard.GetHit(this, 1);  // Example: Apply melee damage to the wizard

					meleeAttackCount++;
					attackTimer.ResetToZero();

					if (meleeAttackCount >= 1)  // Just use 1 melee attack, no cooldown until the timer is reset
					{
						meleeOnCooldown = true;
						meleeCooldownTimer.ResetToZero();
					}

					isUsingMelee = false;
					isAttacking = false;
				}
			}
			else
			{
				isUsingMelee = false;
				isUsingWave = false;
				isUsingAOE = false;
				isAttacking = false;
				base.AI(ENEMY, GRID);
			}
			


		}


		public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
