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

        public void LoadContent(ContentManager content)
        {
            ballObject = content.Load<Texture2D>("ball");
            ballPosition = new Vector2(400, 300);
            ballSpeed = 100f;
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            KeyboardState keyPressed = Keyboard.GetState();
            float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyPressed.IsKeyDown(Keys.W)) { ballPosition.Y -= updatedBallSpeed; }
            if (keyPressed.IsKeyDown(Keys.S)) { ballPosition.Y += updatedBallSpeed; }
            if (keyPressed.IsKeyDown(Keys.A)) { ballPosition.X -= updatedBallSpeed; }
            if (keyPressed.IsKeyDown(Keys.D)) { ballPosition.X += updatedBallSpeed; }

            ballPosition.X = MathHelper.Clamp(ballPosition.X, 0, graphics.PreferredBackBufferWidth - ballObject.Width);
            ballPosition.Y = MathHelper.Clamp(ballPosition.Y, 0, graphics.PreferredBackBufferHeight - ballObject.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballObject, ballPosition, Color.White);
        }
    }
}
