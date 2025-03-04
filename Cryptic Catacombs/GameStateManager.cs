using Microsoft.Xna.Framework.Input;

namespace Cryptic_Catacombs
{
    public enum GameState
    {
        MainMenu,
        ClassSelection,
        Playing,
        Paused,
        GameOver
    }

    public class GameStateManager
    {
        public static GameState CurrentGameState { get; set; } = GameState.MainMenu;
        public static string SelectedClass { get; set; } = "Melee";
        public GameStateManager()
        {
            CurrentGameState = GameState.MainMenu;
        }

        public void Update(KeyboardState keyPressed, Game1 game)
        {
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (CurrentGameState == GameState.MainMenu && keyPressed.IsKeyDown(Keys.Enter))
                    {
                        CurrentGameState = GameState.ClassSelection;
                    }
                    if (keyPressed.IsKeyDown(Keys.Escape))
                    {
                        game.Exit();
                    }
                    break;

                case GameState.ClassSelection:
                    if (keyPressed.IsKeyDown(Keys.M))
                    {
                        SelectedClass = "Melee";
                        CurrentGameState = GameState.Playing;
                    }
                    if (keyPressed.IsKeyDown(Keys.R))
                    {
                        SelectedClass = "Range";
                        CurrentGameState = GameState.Playing;
                    }
                    if (keyPressed.IsKeyDown(Keys.W))
                    {
                        SelectedClass = "Magic";
                        CurrentGameState = GameState.Playing;
                    }
                    break;

                case GameState.Playing:
                    if (keyPressed.IsKeyDown(Keys.Escape))
                    {
                        CurrentGameState = GameState.Paused;
                    }
                    break;

                case GameState.Paused:
                    if (keyPressed.IsKeyDown(Keys.Enter))
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
