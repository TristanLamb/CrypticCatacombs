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
        private ClassSelectionMenu _classSelectionMenu;
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
            _menuManager = new MenuManager();
            _classSelectionMenu = new ClassSelectionMenu();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _menuManager.LoadContent(Content);
            _classSelectionMenu.LoadContent(Content);

            

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
            if (GameStateManager.CurrentGameState == GameState.ClassSelection)
            {
                _classSelectionMenu?.Update();
            }
            else if (GameStateManager.CurrentGameState == GameState.Playing)
            {
                if (_player == null)
                {
                    switch (GameStateManager.SelectedClass)
                    {
                        case "Melee":
                            _player = new MeleePlayer();
                            _player.LoadContent(Content);
                            GameStateManager.CurrentGameState = GameState.Playing;
                            break;
                        case "Range":
                            _player = new RangePlayer();
                            _player.LoadContent(Content);
                            GameStateManager.CurrentGameState = GameState.Playing;
                            break;
                        case "Magic":
                            _player = new MagicPlayer();
                            _player.LoadContent(Content);
                            GameStateManager.CurrentGameState = GameState.Playing;
                            break;
                        default:
                            Console.WriteLine("ERROR: No Class Selected!");
                            return;
                    }
                    //if (_player != null)
                    //{
                    //    _player.LoadContent(Content);
                    //}
                }
            }
            if (_player != null)
            {
                _player.Update(gameTime, _graphics, _map);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); 
            _spriteBatch.Begin();
            
            if (GameStateManager.CurrentGameState == GameState.MainMenu)
            {
                _menuManager.Draw(_spriteBatch, GameStateManager.CurrentGameState);
            }
            else if (GameStateManager.CurrentGameState == GameState.ClassSelection)
            {
                _classSelectionMenu?.Draw(_spriteBatch);
            }
            else if (GameStateManager.CurrentGameState == GameState.Playing)
            {
                _map.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
            }
            else
            {
                _menuManager.Draw(_spriteBatch, GameStateManager.CurrentGameState);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
