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
using System.Windows.Forms;

namespace CrypticCatacombs
{
    public class UI
    {
		public Basic2d pauseOverlay;

		public Basic2d bkg, messageBKG;

		public SpriteFont font;

        public QuantityDisplayBar healthBar, manaBar;

		public int originalHealthBarSize;


		public UI()
        {
			pauseOverlay = new Basic2d("2d/UI/PauseOverlay", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(300, 300));

            font = Globals.content.Load<SpriteFont>("Fonts/Arial16");

			messageBKG = new Basic2d("2d/Misc/MessageBKG", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(250, 75));
			bkg = new Basic2d("2d/Misc/shade", new Vector2(104, 16), new Vector2(350, 150));
			healthBar = new QuantityDisplayBar(new Vector2(104, 16), 2, Color.Red); //bar = 100 since boarder
			manaBar = new QuantityDisplayBar(new Vector2(204, 16), 2, Color.Blue);
			originalHealthBarSize = 104;
		}
	

        public void Update(Dungeon DUNGEON)
        {
            healthBar.Update(DUNGEON.user.wizard.health, DUNGEON.user.wizard.healthMax); //bar size
			manaBar.Update(DUNGEON.user.wizard.mana, DUNGEON.user.wizard.manaMax);
		}

		public void NewHealthBar(int HEALTHBARINCREASE)
		{
			int healthBarIncrease = HEALTHBARINCREASE * 10;
			healthBar = new QuantityDisplayBar(new Vector2(originalHealthBarSize + healthBarIncrease, 16), 2, Color.Red);
		}
		

        public void Draw(Dungeon DUNGEON)
        {
            string tempString;
            Vector2 strDims;
			bool displayEnemiesKilled = false; // Enable or disable EnemiesKilled counter

			if (displayEnemiesKilled == true)
            {
				tempString = "Score = " + GameGlobals.score;
				strDims = font.MeasureString(tempString);
				Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight - 60), Color.Black);
			}

			bkg.Draw(new Vector2(-5, Globals.screenHeight - 60));

			tempString = "Gold = " + GameGlobals.user.gold;
			strDims = font.MeasureString(tempString);
			Globals.spriteBatch.DrawString(font, tempString, new Vector2(50, Globals.screenHeight - 100), Color.Black);

			healthBar.Draw(new Vector2(40, Globals.screenHeight - 60));
			manaBar.Draw(new Vector2(40, Globals.screenHeight - 40));


			if (DUNGEON.user.wizard.dead)
			{
				messageBKG.Draw(new Vector2(0, 25));
				tempString = "You Have Died!";
				strDims = font.MeasureString(tempString);
				Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight/2), Color.Black);

				tempString = "Press Enter to Restart!";
				strDims = font.MeasureString(tempString);
				Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight / 2 + 24), Color.Black);

				User user = new User(1);
				GameGlobals.user = user;
			}

			if(GameGlobals.paused)
			{
				pauseOverlay.Draw(Vector2.Zero);
			}
		}
    }
}
