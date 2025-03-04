using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cryptic_Catacombs
{
    public class ClassSelectionMenu
    {
        private SpriteFont font;
        private string[] classOptions = { "Melee", "Range", "Magic" };
        private int selectedIndex = 0;

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Arial"); // Load a font from content
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                GameStateManager.SelectedClass = "Melee";
                GameStateManager.CurrentGameState = GameState.Playing;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                GameStateManager.SelectedClass = "Range";
                GameStateManager.CurrentGameState = GameState.Playing;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                GameStateManager.SelectedClass = "Magic";
                GameStateManager.CurrentGameState = GameState.Playing;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < classOptions.Length; i++)
            {
                Color color = (i == selectedIndex) ? Color.Yellow : Color.White;
                spriteBatch.DrawString(font, classOptions[i], new Vector2(300, 200 + i * 40), color);
            }
        }
    }
}
