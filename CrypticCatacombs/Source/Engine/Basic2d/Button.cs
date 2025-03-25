using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrypticCatacombs
{
    public class Button : Basic2d
    {

        public bool isPressed, isHovered;
        public string text;

        public Color hoverColor;

        public SpriteFont font;

        public object info;

        public Action<int> ButtonClicked;

		public Button(string PATH, Vector2 POS, Vector2 DIMS, string FONTPATH, string TEXT, Action<int> BUTTONCLICKED, object INFO)
            : base(PATH, POS, DIMS)
        {
            text = TEXT;
			ButtonClicked = BUTTONCLICKED;
			this.info = INFO;

			if (FONTPATH != "")
			{
				font = Globals.content.Load<SpriteFont>(FONTPATH);
				System.Diagnostics.Debug.WriteLine($"Font loaded: {font != null}"); // Debug to check if font is loaded

			}

			isPressed = false;
            hoverColor = new Color(200, 230, 255);
		}

        public override void Update(Vector2 OFFSET)
        {
			//System.Diagnostics.Debug.WriteLine($"Updating button: {text}");
			if (Hover(OFFSET))
            {
				//System.Diagnostics.Debug.WriteLine($"Mouse hovering over: {text}");
				isHovered = true;

				if (Globals.mouse.LeftClick())
                {
                    isHovered = false;
					isPressed = true;
				}
                else if(Globals.mouse.LeftClickRelease())
                {
					System.Diagnostics.Debug.WriteLine($"Button Clicked: {text}");
					if (ButtonClicked != null)
					{
						ButtonClicked((int)info);
					}
					Reset();
				}

            }
            else
            {
                isHovered = false;
            }

            if(!Globals.mouse.LeftClick() && !Globals.mouse.LeftClickHold())
            {
				isPressed = false;
			}

                base.Update(OFFSET);
        }


        public virtual void Reset()
        {
			isPressed = false;
			isHovered = false;
		}

   //     public virtual void RunButtonClick()
   //     {
			//System.Diagnostics.Debug.WriteLine($"Button clicked: {text}, Passing parameter: {info}");

			//if (ButtonClicked != null)
   //         {
   //             ButtonClicked(info);
   //         }
   //         Reset();
   //     }


		public override void Draw()
        {
            Color tempColor = Color.White;
            if(isPressed)
            {
				tempColor = Color.Gray;
			}
			else if (isHovered)
            {
				tempColor = hoverColor;
			}


			Globals.spriteBatch.Draw(myModel, pos + updateOffset, null, tempColor, 0f, new Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2), 1.0f, SpriteEffects.None, 0f);


			base.Draw();

			Vector2 strDims = font.MeasureString(text);
			Globals.spriteBatch.DrawString(font, text, pos + updateOffset + new Vector2(-strDims.X / 2, -strDims.Y / 2), Color.Black);
		}
	}
}
