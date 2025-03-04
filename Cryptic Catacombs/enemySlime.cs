using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace Cryptic_Catacombs
{
	public class enemySlime
	{
		private Texture2D enemyWalkingTexture;
		private Texture2D enemyBasicAttackTexture;
		private Vector2 position;
		private float speed;
		private int damage;

		public Rectangle slimeBoundingBox;

		// Animation
		private int frameWidth;
		private int frameHeight;
		private int currentFrame;
		private int totalFrames;
		private float timePerFrame;
		private float timeElapsed;
		private bool isMovingRight;
		private bool isAttacking;
		//private int attackFrameDuration;


		public enemySlime(Vector2 initialPosition, int damage, float speed)
		{
			this.position = initialPosition;
			this.damage = damage;
			this.speed = speed;

		}


		public void LoadContent(ContentManager content)
		{
			enemyWalkingTexture = content.Load<Texture2D>("slimeWalk");
			enemyBasicAttackTexture = content.Load<Texture2D>("slimeBasicAttack");
			position = new Vector2(400, 400);
			speed = 70f;
			if (enemyWalkingTexture.Name.Contains("slimeWalk"))
			{
				frameWidth = enemyWalkingTexture.Width / 6;
				totalFrames = 5;
			}
			else if (enemyBasicAttackTexture.Name.Contains("slimeBasicAttack"))
			{
				frameWidth = enemyBasicAttackTexture.Width / 6;
				totalFrames = 5;
			}
			frameHeight = enemyWalkingTexture.Height;
			currentFrame = 0;
			timePerFrame = 0.2f;
			timeElapsed = 0f;
		}

		
		public void Update(GameTime gameTime, Player player)
		{
			// Basic AI movement following the player
			Vector2 direction = player.BoundingBox.Center.ToVector2() - position;

			if (direction.Length() > 0)
			{
				direction.Normalize();
			}

			Vector2 newPosition = position + direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			slimeBoundingBox = new Rectangle(
					(int)(newPosition.X - (enemyWalkingTexture.Width / 6 * 1.75f) / 2),
					(int)(newPosition.Y - enemyWalkingTexture.Height * 1.75f / 2),
					(int)(enemyWalkingTexture.Width / 6 - 50 * 1.75f), //reducing hitbox size (width)
					(int)(enemyWalkingTexture.Height -50 * 1.75f) //reducing hitbox size (height)
				);

			if (!slimeBoundingBox.Intersects(player.BoundingBox))
			{
				position = newPosition;
			}
			else
			{
				// If collision occurs, you can either stop the slime's movement or implement a different response.
				// In this case, we stop the slime's movement at the player.
				// For a more complex response, you could push the slime away from the player or make it bounce.
			}



			// Check if enemy is close enough to attack
			if (Vector2.Distance(position, player.BoundingBox.Center.ToVector2()) < 50)
			{
				Attack(player);
				isAttacking = true;
				timePerFrame = 0.1f;
			}
			else
			{
				isAttacking = false;
			}

			// checking direction for animation
			if (position.X < player.BoundingBox.Center.X)
			{
				isMovingRight = true;
			}
			else if (position.X > player.BoundingBox.Center.X)
			{
				isMovingRight = false;
			}

			// Update animation
			timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (timeElapsed >= timePerFrame)
			{
				currentFrame = (currentFrame + 1) % totalFrames;
				timeElapsed = 0f;
			}


			//debugging
			Debug.WriteLine("Current Frame: " + currentFrame);
		}

		public void Attack(Player player)
		{
			player.TakeDamage(damage);
			//any other effects that may happen
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			SpriteEffects spriteEffect = SpriteEffects.None;
			Texture2D currentEnemyTexture = enemyWalkingTexture;


			if (isAttacking)
			{
				currentEnemyTexture = enemyBasicAttackTexture;
			}

			if (isMovingRight)
			{
				spriteEffect = SpriteEffects.None;
			}
			else
			{
				spriteEffect = SpriteEffects.FlipHorizontally;
			}

			Rectangle sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

			float scale = 1.75f;
			spriteBatch.Draw(
				currentEnemyTexture,             // Texture to draw
				position,					     // Position on the screen
				sourceRectangle,				 // Portion of the texture to draw
				Color.White,					 // Color (no tint)
				0f,								 // Rotation (no rotation)
				Vector2.Zero,					 // Origin (top-left corner)
				scale,							 // Scale factor
				spriteEffect,					 // No flip effects
				0f								 // Layer depth (default)
				);

		}
	}
}
