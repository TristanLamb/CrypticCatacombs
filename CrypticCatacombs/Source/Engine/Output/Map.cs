using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CrypticCatacombs
{
	public class Map
	{
		private int[,] tiles;
		private Texture2D floorTexture;
		private Texture2D wallTexture;
		private Texture2D doorTexture;
		private Texture2D emptyTexture;
		private int tileSize;

		public Map(int[,] TILES, int TILESIZE)
		{
			tiles = TILES;
			tileSize = TILESIZE;
		}

		public void LoadContent(ContentManager content)
		{
			floorTexture = content.Load<Texture2D>("2d/Map/floor");
			wallTexture = content.Load<Texture2D>("2d/Map/wall");
			doorTexture = content.Load<Texture2D>("2d/Map/door");
			emptyTexture = content.Load<Texture2D>("2d/Map/EmptySpace");
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int y = 0; y < tiles.GetLength(0); y++)
			{
				for (int x = 0; x < tiles.GetLength(1); x++)
				{
					Texture2D texture = tiles[y, x] switch
					{
						0 => floorTexture,
						1 => wallTexture,
						2 => doorTexture,
						3 => emptyTexture,
						_ => null
					};

					if (texture != null)
					{
						spriteBatch.Draw(
							texture,
							new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),
							Color.White
						);
					}
				}
			}
		}

		public bool CheckCollision(Vector2 playerPosition)
		{
			int tileX1 = (int)((playerPosition.X - 15) / tileSize); // Left edge
			int tileY1 = (int)((playerPosition.Y - 5) / tileSize); // Top edge
			int tileX2 = (int)((playerPosition.X + 20) / tileSize); // Right edge
			int tileY2 = (int)((playerPosition.Y + 20) / tileSize); // Bottom edge

			return tiles[tileY1, tileX1] == 1 || tiles[tileY1, tileX2] == 1 ||
				   tiles[tileY2, tileX1] == 1 || tiles[tileY2, tileX2] == 1;
		}


	}
}