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
    public class CharacterMenu : Menu2d
    {
        public Wizard wizard;

		//public TextZone textZone;
		public List<TextZone> textZones;


		public CharacterMenu(Wizard WIZARD)
            : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(350, 500), null)
        {
            wizard = WIZARD;

			textZones = new List<TextZone>();

			textZones.Add(new TextZone(new Vector2(0, 0), "Gold: " + GameGlobals.user.gold.ToString(), (int)(dims.X * .9f), 22, "Fonts/Arial16", Color.Black));
			textZones.Add(new TextZone(new Vector2(0, 0), "Health: " + GameGlobals.user.wizard.health.ToString() + "/" + GameGlobals.user.wizard.healthMax.ToString(), (int)(dims.X * .9f), 22, "Fonts/Arial16", Color.Black));
			textZones.Add(new TextZone(new Vector2(0, 0), "Mana: " + GameGlobals.user.wizard.mana.ToString() + "/" + GameGlobals.user.wizard.manaMax.ToString(), (int)(dims.X * .9f), 22, "Fonts/Arial16", Color.Black));
		}

		public override void Update()
		{
			base.Update();
			textZones.Clear();
			textZones.Add(new TextZone(new Vector2(0, 0), "Gold: " + GameGlobals.user.gold.ToString(), (int)(dims.X * .9f), 22, "Fonts/Arial16", Color.Black));
			textZones.Add(new TextZone(new Vector2(0, 0), "Health: " + GameGlobals.user.wizard.health.ToString() + "/" + GameGlobals.user.wizard.healthMax.ToString(), (int)(dims.X * .9f), 22, "Fonts/Arial16", Color.Black));
			textZones.Add(new TextZone(new Vector2(0, 0), "Mana: " + GameGlobals.user.wizard.mana.ToString() + "/" + GameGlobals.user.wizard.manaMax.ToString(), (int)(dims.X * .9f), 22, "Fonts/Arial16", Color.Black));
		}

		public override void Draw()
		{
			base.Draw();

			if(Active)
			{
				string tempStr = "" + wizard.name;
				Vector2 strDims = font.MeasureString(tempStr);
				Globals.spriteBatch.DrawString(font, tempStr, topLeft + new Vector2(bkg.dims.X/2 - strDims.X/2, 40), Color.Black);

				//textZone.Draw(topLeft + new Vector2(10, 100));
				for (int i = 0; i < textZones.Count; i++)
				{
					textZones[i].Draw(topLeft + new Vector2(10, 100 + i * 50)); // 50px space between lines
				}
			}
		}
	}
}
