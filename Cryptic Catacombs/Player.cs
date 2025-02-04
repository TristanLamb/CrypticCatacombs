using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cryptic_Catacombs
{
    public class Player
    {
        private Texture2D ballObject;
        private Vector2 ballPosition;
        private float ballSpeed;
<<<<<<< HEAD
        public Rectangle BoundingBox => new Rectangle((int)ballPosition.X, (int)ballPosition.Y, 32, 32);
=======
>>>>>>> 1b8b20c (Adding Starting Files)

        public void LoadContent(ContentManager content)
        {
            ballObject = content.Load<Texture2D>("ball");
            ballPosition = new Vector2(400, 300);
            ballSpeed = 100f;
        }

<<<<<<< HEAD
        public void Update(GameTime gameTime, GraphicsDeviceManager graphics, Map map)
        {
            KeyboardState keyPressed = Keyboard.GetState();
            float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 newPosition = ballPosition;
            
            Vector2 tempPosition = ballPosition;

            if (keyPressed.IsKeyDown(Keys.W))
            {
                tempPosition.Y -= updatedBallSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y, 32, 32)))
                    newPosition.Y -= updatedBallSpeed;
            }
            if (keyPressed.IsKeyDown(Keys.S))
            {
                tempPosition.Y += updatedBallSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y + 31, 32, 32)))
                    newPosition.Y += updatedBallSpeed;
            }
            if (keyPressed.IsKeyDown(Keys.A))
            {
                tempPosition.X -= updatedBallSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X, (int)tempPosition.Y, 32, 32)))
                    newPosition.X -= updatedBallSpeed;
            }
            if (keyPressed.IsKeyDown(Keys.D))
            {
                tempPosition.X += updatedBallSpeed;
                if (!map.CheckCollision(new Rectangle((int)tempPosition.X + 31, (int)tempPosition.Y, 32, 32)))
                    newPosition.X += updatedBallSpeed;
            }

            ballPosition = newPosition;
=======
        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            KeyboardState keyPressed = Keyboard.GetState();
            float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyPressed.IsKeyDown(Keys.W)) { ballPosition.Y -= updatedBallSpeed; }
            if (keyPressed.IsKeyDown(Keys.S)) { ballPosition.Y += updatedBallSpeed; }
            if (keyPressed.IsKeyDown(Keys.A)) { ballPosition.X -= updatedBallSpeed; }
            if (keyPressed.IsKeyDown(Keys.D)) { ballPosition.X += updatedBallSpeed; }
>>>>>>> 1b8b20c (Adding Starting Files)

            ballPosition.X = MathHelper.Clamp(ballPosition.X, 0, graphics.PreferredBackBufferWidth - ballObject.Width);
            ballPosition.Y = MathHelper.Clamp(ballPosition.Y, 0, graphics.PreferredBackBufferHeight - ballObject.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballObject, ballPosition, Color.White);
        }
    }
}
