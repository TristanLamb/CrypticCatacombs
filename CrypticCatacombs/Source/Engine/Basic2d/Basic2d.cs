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

        public Vector2 pos, dims, frameSize, updateOffset;

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
            updateOffset = OFFSET;
		}

		//more resource heavy hover method (pixels)
		public virtual bool Hover(Vector2 OFFSET)
        {
			return HoverImg(OFFSET);
        }

		public virtual bool HoverImg(Vector2 OFFSET)
		{
			//Vector2 mousePos = Globals.mouse.newMousePos;
			Vector2 buttonPos = pos + OFFSET;
			Vector2 mousePos = new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y);
			//System.Diagnostics.Debug.WriteLine($"Mouse: {mousePos} | Button Pos: {buttonPos} | Size: {dims}");


			if (mousePos.X >= (pos.X + OFFSET.X) - dims.X / 2 && mousePos.X <= (pos.X + OFFSET.X) + dims.X / 2 && mousePos.Y >= (pos.Y + OFFSET.Y) - dims.Y / 2 && mousePos.Y <= (pos.Y + OFFSET.Y) + dims.Y / 2)
			{
				//System.Diagnostics.Debug.WriteLine("Hover detected!");
				return true;
			}

			return false;
		}


		public virtual void Draw()
		{
			if (myModel != null)
			{
				Globals.spriteBatch.Draw(
					myModel,
					new Rectangle((int)(pos.X + updateOffset.X), (int)(pos.Y + updateOffset.Y), (int)dims.X, (int)dims.Y),
					null,
					Color.White, //tint
					rotation, //rotation
					new Microsoft.Xna.Framework.Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2),
					new SpriteEffects(),
					0 //layer depth
					);
			}
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

		public virtual void Draw(Vector2 OFFSET, Color COLOR)
		{
			if (myModel != null)
			{
				Globals.spriteBatch.Draw(
					myModel,
					new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y),
					null,
					COLOR, //tint
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
