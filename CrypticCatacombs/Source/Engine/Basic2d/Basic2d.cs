using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrypticCatacombs
{
    public class Basic2d
    {
        public float rotation;

        public Vector2 pos, dims, frameSize;

        public Texture2D myModel;
        public Basic2d(string PATH, Vector2 POS, Vector2 DIMS)
        {
            pos = new Vector2(POS.X, POS.Y);
            dims = new Vector2(DIMS.X, DIMS.Y);
			rotation = 0.0f;

			myModel = Globals.content.Load<Texture2D>(PATH);
        }

        public virtual void Update(Vector2 OFFSET)
        {

        }

        public virtual void Draw(Vector2 OFFSET)
        {
            if(myModel != null)
            {
                Globals.spriteBatch.Draw(
                    myModel,
                    new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y),
                    null,
                    Color.White, //tint
                    rotation, //rotation
                    new Microsoft.Xna.Framework.Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2),
                    new SpriteEffects(), 
                    0 //layer depth
                    );
            }
		}

		public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN, Color COLOR)
        {
            if (myModel != null)
            {
                Globals.spriteBatch.Draw(
                    myModel,
                    new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y),
                    null,
					COLOR, //tint
                    rotation, //rotation
                    new Microsoft.Xna.Framework.Vector2(ORIGIN.X, ORIGIN.Y),
                    new SpriteEffects(),
                    0 //layer depth
                    );
            }
        }

		public virtual void Draw(Vector2 OFFSET, SpriteEffects SPRITEEFFECT)
		{
			if (myModel != null)
			{
				Globals.spriteBatch.Draw(
					myModel,
					new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y),
					null,
					Color.White, //tint
					rotation, //rotation
					new Microsoft.Xna.Framework.Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2),
					SPRITEEFFECT,
					0 //layer depth
					);
			}
		}
	}
}
