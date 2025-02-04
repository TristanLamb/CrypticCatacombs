using Microsoft.Xna.Framework.Input;

namespace Cryptic_Catacombs
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public class GameStateManager
    {
        public GameState CurrentGameState { get; private set; }

        public GameStateManager()
        {
            CurrentGameState = GameState.MainMenu;
        }

        public void Update(KeyboardState keyPressed, Game1 game)
        {
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (keyPressed.IsKeyDown(Keys.Enter))
                    {
                        CurrentGameState = GameState.Playing;
                    }
                    if (keyPressed.IsKeyDown(Keys.Escape))
                    {
                        game.Exit();
                    }
                    break;

                case GameState.Playing:
                    if (keyPressed.IsKeyDown(Keys.Escape))
                    {
                        CurrentGameState = GameState.Paused;
                    }
                    break;

                case GameState.Paused:
                    if (keyPressed.IsKeyDown(Keys.Escape) || keyPressed.IsKeyDown(Keys.Enter))
                    {
                        CurrentGameState = GameState.Playing;
                    }
                    break;

                case GameState.GameOver:
                    if (keyPressed.IsKeyDown(Keys.Enter))
                    {
                        CurrentGameState = GameState.Playing;
                    }
                    if (keyPressed.IsKeyDown(Keys.Escape))
                    {
                        game.Exit();
                    }
                    break;
            }
        }
    }
}
