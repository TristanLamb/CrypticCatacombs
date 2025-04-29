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
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CrypticCatacombs
{
    public class Menu2d
    {
        protected bool active;

        public Vector2 pos, dims, topLeft;

        public Animated2d bkg;

        public Button closeButton;

        public SpriteFont font;

        public PassObject CloseAction;

        public Menu2d(Vector2 POS, Vector2 DIMS, PassObject CLOSEACTION)
        {
            pos = POS;
            dims = DIMS;
            CloseAction = CLOSEACTION;

            bkg = new Animated2d("2d/Misc/MessageBKG", new Vector2(0, 0), new Vector2(dims.X, dims.Y), new Vector2(1, 1), Color.White);

			closeButton = new Button("2d/UI/XButton", new Vector2(bkg.dims.X/2, -bkg.dims.Y/2), new Vector2(30, 30), new Vector2(1, 1), "", "", Close, 2);

            font = Globals.content.Load<SpriteFont>("Fonts/Arial16");
		}

		#region Properties
		public virtual bool Active
        {
            get
            {
                return active;
            }
            set
			{
                active = value;
			}
		}
		#endregion

		public virtual void Update()
        {
            if(Active)
            {
                topLeft = pos - dims / 2;
                closeButton.Update(pos);
            }
        }

        public virtual void Close(object INFO)
        {
            Active = false;
        }

        public virtual void Draw()
        {
			if (Active)
			{
                bkg.Draw(pos);
                closeButton.Draw(pos);
			}
		}
    }
}
