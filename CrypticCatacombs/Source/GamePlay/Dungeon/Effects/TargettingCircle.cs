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
    public class TargettingCircle : Effect2d
    {
        public TargettingCircle(Vector2 POS, Vector2 DIMS)
			: base("2d/Misc/TargetCircle", POS, new Vector2(50, 50), new Vector2(1, 1), 400)
		{
			noTimer = true;
		}

		
	}
}
