using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cryptic_Catacombs

{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStateManager _gameStateManager;
        private Player _player;
        private MenuManager _menuManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameStateManager = new GameStateManager();
            _player = new Player();
            _menuManager = new MenuManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player.LoadContent(Content);
            _menuManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _gameStateManager.Update(Keyboard.GetState(), this);
            if (_gameStateManager.CurrentGameState == GameState.Playing)
            {
                _player.Update(gameTime, _graphics);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); 
            _spriteBatch.Begin();

            if (_gameStateManager.CurrentGameState == GameState.Playing)
            {
                _player.Draw(_spriteBatch);
            }
            else
            {
                _menuManager.Draw(_spriteBatch, _gameStateManager.CurrentGameState);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
