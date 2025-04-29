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
using Microsoft.VisualBasic.Logging;

namespace CrypticCatacombs
{
	public class Map
	{
		private int[,] tiles;
		private Texture2D floorTexture;
		private Texture2D wallTexture;
		private Texture2D doorTopM;
		private Texture2D doorTopS;
		private Texture2D doorRightM;
		private Texture2D doorRightMET;
		private Texture2D doorRightMEB;
		private Texture2D doorRightS;
		private Texture2D doorBottomM;
		private Texture2D doorBottomS;
		private Texture2D doorLeftM;
		private Texture2D doorLeftS;
		private Texture2D emptyTexture;
		private Texture2D doorRightOpenT;
		private Texture2D shelfTop;
        private Texture2D shelfSide;
        private Texture2D shelfBottom;
		private Texture2D shelfBottomCorner;
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
			doorTopM = content.Load<Texture2D>("2d/Map/doorTopM");
			doorTopS = content.Load<Texture2D>("2d/Map/doorTopS");
			doorRightM = content.Load<Texture2D>("2d/Map/doorRightM");
			doorRightMET = content.Load<Texture2D>("2d/Map/doorRightMET");
			doorRightMEB = content.Load<Texture2D>("2d/Map/doorRightMEB");
			doorRightS = content.Load<Texture2D>("2d/Map/doorRightS");
			doorBottomM = content.Load<Texture2D>("2d/Map/doorBottomM");
			doorBottomS = content.Load<Texture2D>("2d/Map/doorBottomS");
			doorLeftM = content.Load<Texture2D>("2d/Map/doorLeftM");
			doorLeftS = content.Load<Texture2D>("2d/Map/doorLeftS");
			emptyTexture = content.Load<Texture2D>("2d/Map/EmptySpace");
            shelfTop = content.Load<Texture2D>("2d/Map/shelfTop");
            shelfSide = content.Load<Texture2D>("2d/Map/shelfSide");
            shelfBottom = content.Load<Texture2D>("2d/Map/shelfBottom");
			shelfBottomCorner = content.Load<Texture2D>("2d/Map/shelfBottomCorner");
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
						//2 => shelf,
						3 => emptyTexture,
						7 => shelfTop,
						8 => shelfSide,
						9 => shelfBottom,
						10 => shelfBottomCorner,
						11 => doorTopM,
						12 => doorTopS,
						13 => doorRightM,
						14 => doorRightS,
						15 => doorBottomM,
						16 => doorBottomS,
						17 => doorLeftM,
						18 => doorLeftS,
						19 => doorRightOpenT,
						20 => doorRightMET,
						21 => doorRightMEB,
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
			int tileX1 = (int)((playerPosition.X - 15) / tileSize);
			int tileY1 = (int)((playerPosition.Y - 5) / tileSize);
			int tileX2 = (int)((playerPosition.X + 20) / tileSize);
			int tileY2 = (int)((playerPosition.Y + 20) / tileSize);

			tileX1 = Math.Clamp(tileX1, 0, tiles.GetLength(1) - 1);
			tileX2 = Math.Clamp(tileX2, 0, tiles.GetLength(1) - 1);
			tileY1 = Math.Clamp(tileY1, 0, tiles.GetLength(0) - 1);
			tileY2 = Math.Clamp(tileY2, 0, tiles.GetLength(0) - 1);

			//System.Diagnostics.Debug.WriteLine($"enemiesdefeated: {GameGlobals.enemiesDefeated}");
			bool IsBlocked(int x, int y)
			{
				int value = tiles[y, x];
				if (value == 1 || value == 8 || value == 9 || value == 10) return true;
				if (!GameGlobals.enemiesDefeated && value >= 11 && value <= 18) return true;
				return false;
			}

			return IsBlocked(tileX1, tileY1) || IsBlocked(tileX2, tileY1) ||
				   IsBlocked(tileX1, tileY2) || IsBlocked(tileX2, tileY2);
		}




	}
}