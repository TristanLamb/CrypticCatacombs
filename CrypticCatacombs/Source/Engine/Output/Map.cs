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
    public class Map : Basic2d
    {
        public Vector2 backgroundDims;
		private Texture2D wallTexture;


		public Map(string PATH, string wallPath, Vector2 POS, Vector2 DIMS, Vector2 BACKGROUNDDIMS)
            : base(PATH, POS, new Vector2((float)Math.Floor(DIMS.X), (float)Math.Floor(DIMS.Y)))
        {
			backgroundDims = new Vector2((float)Math.Floor(BACKGROUNDDIMS.X), (float)Math.Floor(BACKGROUNDDIMS.Y));
			wallTexture = Globals.content.Load<Texture2D>(wallPath);
		}


		public override void Draw(Vector2 OFFSET)
		{
			float numX = (float)Math.Ceiling(backgroundDims.X / dims.X);
			float numY = (float)Math.Ceiling(backgroundDims.Y / dims.Y);

			for (int i = 0; i < numX; i++)
			{
				for (int j = 0; j < numY; j++)
				{
					bool isEdge = (i == 0 || j == 0 || i >= numX - 1 || j >= numY - 1);
					Texture2D texture;
					if (isEdge)
					{
						texture = wallTexture;
					}
					else
					{
						texture = myModel;
					}

					float xSpaceRemaining = Math.Min(dims.X, backgroundDims.X - (i * dims.X));
					float ySpaceRemaining = Math.Min(dims.Y, backgroundDims.Y - (j * dims.Y));
					float xPercentageRemaining = Math.Min(1, xSpaceRemaining / dims.X);
					float yPercentageRemaining = Math.Min(1, ySpaceRemaining / dims.Y);

					Globals.spriteBatch.Draw(
						texture,
						new Rectangle(
							(int)(pos.X + OFFSET.X + dims.X * i),
							(int)(pos.Y + OFFSET.Y + dims.Y * j),
							(int)Math.Ceiling(dims.X * xPercentageRemaining),
							(int)Math.Ceiling(dims.Y * yPercentageRemaining)
						),
						new Rectangle(
							0, 0,
							(int)(xPercentageRemaining * texture.Bounds.Width),
							(int)(yPercentageRemaining * texture.Bounds.Height)
						),
						Color.White
					);
				}
			}
		}

	}
}
