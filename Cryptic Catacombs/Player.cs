using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cryptic_Catacombs
{
    public class Player
    {
        private Texture2D playerIdleTexture;
        private Texture2D playerWalkingTexture;
        private Vector2 playerPosition;
        private float playerSpeed;
        private int health;
        public Rectangle BoundingBox => new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 32, 32);

        // Animation
        private int frameWidth;
        private int frameHeight; 
        private int currentFrame;
        private int totalFrames;
        private float timePerFrame;
        private float timeElapsed;
        private bool isWalking;
        private bool isMovingRight;



        public Player()
        {
            health = 100;
        }
        public int Health => health;


        public void TakeDamage(int damage)
        {
            health -= damage; // subtracts the damage taken from health
            if (health < 0)
            {
                health = 0; // ensure health doesn't go below zero (Change Later for animation)
            }
        }




        public void LoadContent(ContentManager content)
        {
            playerIdleTexture = content.Load<Texture2D>("swordsmanIdle");
            playerWalkingTexture = content.Load<Texture2D>("swordsmanWalking");
            playerPosition = new Vector2(300, 300);
            playerSpeed = 100f;

            //animation
            if (playerIdleTexture.Name.Contains("Idle"))
            {
                frameWidth = playerIdleTexture.Width / 6;
                totalFrames = 5;
            }
            else if (playerWalkingTexture.Name.Contains("Walking"))
            {
                frameWidth = playerWalkingTexture.Width / 8;
                totalFrames = 7;
            }

            frameHeight = playerIdleTexture.Height; 
            currentFrame = 0;
            timePerFrame = 0.2f; 
            timeElapsed = 0f;

            isWalking = false;
        }




        public void Update(GameTime gameTime, GraphicsDeviceManager graphics, Map map)
        {
            KeyboardState keyPressed = Keyboard.GetState();
            float updatedPlayerSpeed = playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 newPosition = playerPosition;
            
            Vector2 tempPosition = playerPosition;
            bool moving = false;

            if (keyPressed.IsKeyDown(Keys.W))
            {
                tempPosition.Y -= updatedPlayerSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y, 32, 32)))
                    newPosition.Y -= updatedPlayerSpeed;
                moving = true;
            }
            if (keyPressed.IsKeyDown(Keys.S))
            {
                tempPosition.Y += updatedPlayerSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y + 31, 32, 32)))
                    newPosition.Y += updatedPlayerSpeed;
                moving = true;
            }
            if (keyPressed.IsKeyDown(Keys.A))
            {
                tempPosition.X -= updatedPlayerSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y, 32, 32)))
                    newPosition.X -= updatedPlayerSpeed;
                moving = true;
                isMovingRight = false;
            }
            if (keyPressed.IsKeyDown(Keys.D))
            {
                tempPosition.X += updatedPlayerSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y, 32, 32)))
                    newPosition.X += updatedPlayerSpeed;
                moving = true;
                isMovingRight = true;
            }
            if (moving)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            playerPosition = newPosition;

            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, graphics.PreferredBackBufferWidth - 32);
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, graphics.PreferredBackBufferHeight - 32);


            //animation
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= timePerFrame)
            {
                currentFrame = (currentFrame + 1) % totalFrames;
                timeElapsed = 0f;
            }
        }




        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D currentTexture;
            if (isWalking)
            {
                currentTexture = playerWalkingTexture;
            }
            else
            {
                currentTexture = playerIdleTexture;
            }

            SpriteEffects spriteEffect;
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
                currentTexture,           // Texture to draw
                playerPosition,           // Position on the screen
                sourceRectangle,          // Portion of the texture to draw
                Color.White,              // Color (no tint)
                0f,                       // Rotation (no rotation)
                Vector2.Zero,             // Origin (top-left corner)
                scale,                    // Scale factor
                spriteEffect,             // No flip effects
                0f                        // Layer depth (default)
                );
        }
    }
}
