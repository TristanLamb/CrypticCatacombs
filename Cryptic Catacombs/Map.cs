using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Serialization;

namespace Cryptic_Catacombs
{
    public class Map
    {
        private int[,] _tiles;
        private Texture2D _floorTexture;
        private Texture2D _wallTexture;
        private Texture2D _doorTexture;
        private int _tileSize;

        public Map(int[,] tiles, int tileSize)
        {
            _tiles = tiles;
            _tileSize = tileSize;
        }

        public void LoadContent(ContentManager content)
        {
            _floorTexture = content.Load<Texture2D>("Floor");
            _wallTexture = content.Load<Texture2D>("Wall");
            _doorTexture = content.Load<Texture2D>("Door");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < _tiles.GetLength(0); y++)
            {
                for (int x = 0; x < _tiles.GetLength(1); x++)
                {
                    Texture2D texture = _tiles[y, x] switch
                    {
                        0 => _floorTexture,
                        1 => _wallTexture,
                        2 => _doorTexture,
                        _ => null
                    };

                    if (texture != null)
                    {
                        spriteBatch.Draw(
                            texture, 
                            new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), 
                            Color.White
                        );
                    }
                }
            }
        }

        public bool CheckCollision(Rectangle playerBoundingBox)
        {
            for (int y = 0; y < _tiles.GetLength(0); y++)
            {
                for (int x = 0; x < _tiles.GetLength(1); x++)
                {
                    if (_tiles[y,x] == 1)
                    {
                        Rectangle tileBoundingBox = new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize);
                        if (playerBoundingBox.Intersects(tileBoundingBox))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}