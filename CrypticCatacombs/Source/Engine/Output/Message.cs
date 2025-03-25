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
    public class Message
    {
        public bool done, lockScreen;
        public Vector2 pos, dims;
        public Color color;
        public TextZone textZone;

        public CustomTimer timer;

        public Message(Vector2 POS, Vector2 DIMS, string MSG, int MSEC, Color COLOR, bool LOCKSCREEN)
        {
            pos = POS;
            dims = DIMS;
            color = COLOR;
			lockScreen = LOCKSCREEN;
			done = false;

            textZone = new TextZone(new Vector2(0, 0), MSG, (int)(dims.X * .9f), 22, "Fonts/Arial16", COLOR);

			timer = new CustomTimer(MSEC);
		}

        public virtual void Update()
        {
            timer.UpdateTimer();
            if (timer.Test())
			{
				done = true;
			}
            textZone.color = color * (float)(.9f * (float)(timer.MSec - (float)timer.Timer) / (float)timer.MSec); //fades out message
		}

        public virtual void Draw()
        {
            textZone.Draw(new Vector2(pos.X-textZone.dims.X/2, pos.Y));
        }
    }
}
