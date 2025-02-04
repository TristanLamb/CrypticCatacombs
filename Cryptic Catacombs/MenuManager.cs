using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Cryptic_Catacombs
{
    public class MenuManager
    {
        private SpriteFont font;

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("arial");
        }

        public void Draw(SpriteBatch spriteBatch, GameState gameState)
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    DrawMainMenu(spriteBatch);
                    break;
                case GameState.Paused:
                    DrawPauseMenu(spriteBatch);
                    break;
                case GameState.GameOver:
                    DrawGameOverMenu(spriteBatch);
                    break;
            }
        }

        private void DrawMainMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Main Menu", new Vector2(350, 100), Color.White);
            spriteBatch.DrawString(font, "Press ENTER to Start", new Vector2(300, 200), Color.White);
            spriteBatch.DrawString(font, "Press ESC to Quit", new Vector2(300, 300), Color.White);
        }

        private void DrawPauseMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "PAUSED", new Vector2(350, 100), Color.White);
            spriteBatch.DrawString(font, "Press ESC or ENTER to Resume", new Vector2(250, 200), Color.White);
        }

        private void DrawGameOverMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "GAME OVER", new Vector2(350, 100), Color.White);
            spriteBatch.DrawString(font, "Press ENTER to Restart", new Vector2(250, 200), Color.White);
            spriteBatch.DrawString(font, "Press ESC to Quit", new Vector2(250, 300), Color.White);
        }
    }
}
