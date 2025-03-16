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
    public class UI
    {
		public SpriteFont font;

        public QuantityDisplayBar healthBar;

		public UI()
        {
            font = Globals.content.Load<SpriteFont>("Fonts/Arial16");

            healthBar = new QuantityDisplayBar(new Vector2(104, 16), 2, Color.Red); //bar = 100 since boarder
        }
	

        public void Update(Dungeon DUNGEON)
        {
            healthBar.Update(DUNGEON.user.wizard.health, DUNGEON.user.wizard.healthMax); //bar size
        }

        public void Draw(Dungeon DUNGEON)
        {
            string tempString;
            Vector2 strDims;
			bool displayEnemiesKilled = false; // Enable or disable EnemiesKilled counter

            if(displayEnemiesKilled == true)
            {
				tempString = "Score = " + GameGlobals.score;
				strDims = font.MeasureString(tempString);
				Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight - 40), Color.Black);
			}
            

            healthBar.Draw(new Vector2(40, Globals.screenHeight - 60));


			if (DUNGEON.user.wizard.dead)
			{
				tempString = "You Have Died!";
				strDims = font.MeasureString(tempString);
				Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight/2), Color.Black);
				tempString = "Press Enter to Restart!";
				strDims = font.MeasureString(tempString);
				Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight / 2 + 24), Color.Black);
			}
		}
    }
}
