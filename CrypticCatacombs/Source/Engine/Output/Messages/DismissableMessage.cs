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
    class DismissableMessage : Message
    {
        public Basic2d bkg;

        public Button button;

        public PassObject ConfirmFunction;

        public DismissableMessage(Vector2 POS, Vector2 DIMS, string MSG, Color COLOR, bool LOCKSCREEN, PassObject CONFIRMFUNCTION)
            : base(POS, DIMS, MSG, 2000, COLOR, LOCKSCREEN)
        {
            bkg = new Basic2d("2d/Misc/solid", new Vector2(pos.X, pos.Y), new Vector2(dims.X, dims.Y));

            button = new Button("2d/Misc/shade", new Vector2(pos.X, pos.Y + 20), new Vector2(70, 35), new Vector2(1, 2), "Fonts/Arial16", "OK", new PassObject(CompleteClick), 2);
			ConfirmFunction = CONFIRMFUNCTION;
		}

		public override void Update()
		{
            button.Update(Vector2.Zero);
		}

		public virtual void CompleteClick(object INFO)
        {
            ConfirmFunction(INFO);
            done = true;
        }

        public override void Draw()
        {
            bkg.Draw(Vector2.Zero);
			if (button != null)
			{
				button.Draw(Vector2.Zero);
			}

			textZone.color = color;
            textZone.Draw(new Vector2(pos.X - textZone.dims.X/2 + 20, pos.Y - 75));
            
		}


	}
}
