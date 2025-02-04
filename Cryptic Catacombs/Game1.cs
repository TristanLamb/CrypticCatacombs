<<<<<<< HEAD
using System;
=======
>>>>>>> 1b8b20c (Adding Starting Files)
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
<<<<<<< HEAD
        private Map _map;
=======
>>>>>>> 1b8b20c (Adding Starting Files)

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
<<<<<<< HEAD
            _graphics.ApplyChanges();

=======
>>>>>>> 1b8b20c (Adding Starting Files)
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
<<<<<<< HEAD

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
=======
>>>>>>> 1b8b20c (Adding Starting Files)
        }

        protected override void Update(GameTime gameTime)
        {
            _gameStateManager.Update(Keyboard.GetState(), this);
            if (_gameStateManager.CurrentGameState == GameState.Playing)
            {
<<<<<<< HEAD
                _player.Update(gameTime, _graphics, _map);
=======
                _player.Update(gameTime, _graphics);
>>>>>>> 1b8b20c (Adding Starting Files)
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); 
            _spriteBatch.Begin();
<<<<<<< HEAD
            
            
            if (_gameStateManager.CurrentGameState == GameState.Playing)
            {
                _map.Draw(_spriteBatch);
=======

            if (_gameStateManager.CurrentGameState == GameState.Playing)
            {
>>>>>>> 1b8b20c (Adding Starting Files)
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
