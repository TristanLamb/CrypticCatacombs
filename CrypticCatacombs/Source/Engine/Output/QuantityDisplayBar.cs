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
    public class QuantityDisplayBar
    {
        public int boarder;

        public Basic2d bar, barBackground;

        public Color color;

        public QuantityDisplayBar(Vector2 DIMS, int BOARDER, Color COLOR)
        {
            boarder = BOARDER;
            color = COLOR;
            bar = new Basic2d("2d/Misc/solid", new Vector2(0, 0), new Vector2(DIMS.X - boarder * 2, DIMS.Y - boarder * 2));
			barBackground = new Basic2d("2d/Misc/shade", new Vector2(0, 0), new Vector2(DIMS.X, DIMS.Y));
		}

        public virtual void Update(float CURRENT, float MAX)
        {
            bar.dims = new Vector2(CURRENT/MAX*(barBackground.dims.X - boarder * 2), bar.dims.Y);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            barBackground.Draw(OFFSET, new Vector2(0, 0), Color.Black);
            bar.Draw(OFFSET + new Vector2(boarder, boarder), new Vector2(0, 0), color);
        }
    }
}
