using System;
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
        private Map _map;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.ApplyChanges();

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

            base.LoadContent();

            int screenWidth = _graphics.PreferredBackBufferWidth;
            int screenHeight = _graphics.PreferredBackBufferHeight;

            int tileCols = 25;
            int tileRows = 15;

            int tileSize = Math.Min(screenWidth / tileCols, screenHeight / tileRows);

            int[,] tiles = new int[tileRows, tileCols];
            for (int y = 0; y < tileRows; y++)
            {
                for (int x = 0; x < tileCols; x++)
                {
                    if (x == 0 || y == 0 || x == tileCols - 1 || y == tileRows - 1)
                    {
                        tiles[y,x] = 1;
                    }
                    else if (x == tileCols / 2 && y == tileRows / 2)
                    {
                        tiles[y,x] = 2;
                    }
                    else
                    {
                        tiles[y,x] = 0;
                    }
                }
            }

            _map = new Map(tiles, tileSize);
            _map.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _gameStateManager.Update(Keyboard.GetState(), this);
            if (_gameStateManager.CurrentGameState == GameState.Playing)
            {
                _player.Update(gameTime, _graphics, _map);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); 
            _spriteBatch.Begin();
            
            
            if (_gameStateManager.CurrentGameState == GameState.Playing)
            {
                _map.Draw(_spriteBatch);
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
