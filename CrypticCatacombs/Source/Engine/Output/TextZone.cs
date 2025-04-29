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
    public class TextZone
    {
        public int maxWidth, lineHeight;
        string str;
        public Vector2 pos, dims;
        public Color color;

        public SpriteFont font;

        public List<string> lines = new List<string>();

        public TextZone(Vector2 POS, string STR, int MAXWIDTH, int LINEHEIGHT, string FONT, Color FONTCOLOR)
		{
			pos = POS;
			str = STR;

			maxWidth = MAXWIDTH;
			lineHeight = LINEHEIGHT;
			color = FONTCOLOR;

			font = Globals.content.Load<SpriteFont>(FONT);

			if (str != "")
			{
				ParseLines();
			}
		}

		#region properties
		public string Str
		{
			get
			{
				return str;
			}
			set
			{
				str = value;
				ParseLines();
			}
		}
		#endregion

		public virtual void ParseLines()
		{
			lines.Clear();

			List<string> wordList = new List<string>();
			string tempString = "";

			int currentWidth = 0;

			if (str != "" && str != null)
			{
				wordList = str.Split(' ').ToList<string>();

				for (int i = 0; i < wordList.Count; i++)
				{
					if (tempString != "")
					{
						tempString += " ";
					}

					currentWidth = (int)(font.MeasureString(tempString + wordList[i]).X);

					if (currentWidth > maxWidth)
					{
						lines.Add(tempString);
						tempString = wordList[i];
					}
					else
					{
						tempString += wordList[i];
					}
				}

				if (tempString != "")
				{
					lines.Add(tempString);
				}

				SetDims();
			}
		}


		public virtual void SetDims()
		{
			dims = new Vector2(maxWidth, lineHeight * lines.Count);
		}

		public virtual void Draw(Vector2 OFFSET)
		{
			for(int i = 0; i < lines.Count; i++)
			{
				Globals.spriteBatch.DrawString(font, lines[i], OFFSET + new Vector2(pos.X, pos.Y + (lineHeight * i)), color);
			}
		}
	}
}
