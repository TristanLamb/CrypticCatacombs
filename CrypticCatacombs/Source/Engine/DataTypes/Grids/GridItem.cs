using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CrypticCatacombs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrypticCatacombs
{
    public class GridItem : Animated2d
    {
        public GridItem(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES)
			: base(PATH, POS, DIMS, FRAMES, Color.White)
		{
			
		}
	}
}
