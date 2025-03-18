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

		public bool CheckCollision(Rectangle playerBoundingBox)
		{
			for (int y = 0; y < tiles.GetLength(0); y++)
			{
				for (int x = 0; x < tiles.GetLength(1); x++)
				{
					if (tiles[y, x] == 1)
					{
						Rectangle tileBoundingBox = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
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