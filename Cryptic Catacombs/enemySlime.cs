using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace Cryptic_Catacombs
{
	public class enemySlime
	{
		private Texture2D enemyTexture;
		private Vector2 position;
		private float speed;
		private int damage;

		// Animation
		private int currentFrame;
		private float elapsedTime;
		private float frameDuration; // controls animation speed
		private int currentRow; // row in sprite sheet
		private int[] framesPerRow; // number of frames per row

		public enemySlime(Vector2 initialPosition, int damage, float speed)
		{
			this.position = initialPosition;
			this.damage = damage;
			this.speed = speed;

			// Animation
			currentFrame = 0;
			elapsedTime = 0f;
			frameDuration = 0.1f; //  animation speed
			currentRow = 0; // default to first row (idle)
			framesPerRow = new int[] { 6, 6, 6, 12, 4, 4, 4 }; // frames per row in the sprite sheet
		}


		public void LoadContent(ContentManager content)
		{
			//debugging
			Debug.WriteLine("Loading slimeWalk texture...");
			try
			{
				enemyTexture = content.Load<Texture2D>("slimeWalk");

				if (enemyTexture != null)
				{
					Debug.WriteLine("slimeWalk texture loaded successfully!");
				}
				else
				{
					Debug.WriteLine("Failed to load slimeWalk texture. The texture is null.");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error loading texture: {ex.Message}");
			}
		}

		
		public void Update(GameTime gameTime, Player player)
		{
			// Basic AI movement following the player
			Vector2 direction = player.BoundingBox.Center.ToVector2() - position;
			if (direction.Length() > 0)
			{
				direction.Normalize();
				position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}

			// Check if enemy is close enough to attack
			if (Vector2.Distance(position, player.BoundingBox.Center.ToVector2()) < 50)
			{
				Attack(player);
			}

			// Update animation
			elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (elapsedTime > frameDuration)
			{
				currentFrame++;
				if (currentFrame >= 5)  // We have 6 frames in total
				{
					currentFrame = 0;  // Reset to first frame if we go past the last one
				}
				elapsedTime -= frameDuration; // Reset elapsed time
			}

			//debugging
			Debug.WriteLine("Current Frame: " + currentFrame);
		}

		public void Attack(Player player)
		{
			player.TakeDamage(damage);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Calculate the source rectangle based on the current frame and row
			int frameWidth = enemyTexture.Width / 6;
			int frameHeight = enemyTexture.Height;

			Rectangle sourceRectangle = new Rectangle(
			frameWidth * currentFrame,       // X position of the frame
			frameHeight * currentRow,        // Y position of the frame
			frameWidth,                      // Width of the frame
			frameHeight                      // Height of the frame
			);

			//debugging
			Debug.WriteLine($"Drawing Frame: {currentFrame}, Source Rect: {sourceRectangle}");
			float scale = 1.75f;
			spriteBatch.Draw(
				enemyTexture,             // Texture to draw
				position,                 // Position on the screen
				sourceRectangle,          // Portion of the texture to draw
				Color.White,              // Color (no tint)
				0f,                       // Rotation (no rotation)
				Vector2.Zero,             // Origin (top-left corner)
				scale,                    // Scale factor
				SpriteEffects.None,       // No flip effects
				0f                        // Layer depth (default)
				);

		}
	}
}
