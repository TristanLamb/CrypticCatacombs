using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cryptic_Catacombs
{
    public class Player
    {
        private Texture2D playerTexture;
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



        public Player()
        {
            health = 100;
        }
        public int Health => health;


        public void TakeDamage(int damage)
        {
            health -= damage; // subtracts the damage taken from health
            if (health < 0) health = 0; // ensure health doesn't go below zero (Change Later for animation)
        }




        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("swordsmanIdle");
            playerPosition = new Vector2(300, 300);
            playerSpeed = 100f;

            //animation
            frameWidth = playerTexture.Width / 6; 
            frameHeight = playerTexture.Height; 
            currentFrame = 0;
            totalFrames = 5;  
            timePerFrame = 0.2f; 
            timeElapsed = 0f;
        }




        public void Update(GameTime gameTime, GraphicsDeviceManager graphics, Map map)
        {
            KeyboardState keyPressed = Keyboard.GetState();
            float updatedPlayerSpeed = playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 newPosition = playerPosition;
            
            Vector2 tempPosition = playerPosition;

            if (keyPressed.IsKeyDown(Keys.W))
            {
                tempPosition.Y -= updatedPlayerSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y, 32, 32)))
                    newPosition.Y -= updatedPlayerSpeed;
            }
            if (keyPressed.IsKeyDown(Keys.S))
            {
                tempPosition.Y += updatedPlayerSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y + 31, 32, 32)))
                    newPosition.Y += updatedPlayerSpeed;
            }
            if (keyPressed.IsKeyDown(Keys.A))
            {
                tempPosition.X -= updatedPlayerSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y, 32, 32)))
                    newPosition.X -= updatedPlayerSpeed;
            }
            if (keyPressed.IsKeyDown(Keys.D))
            {
                tempPosition.X += updatedPlayerSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y, 32, 32)))
                    newPosition.X += updatedPlayerSpeed;
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
            Rectangle sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            float scale = 1.75f;
            spriteBatch.Draw(
                playerTexture,            // Texture to draw
                playerPosition,           // Position on the screen
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
