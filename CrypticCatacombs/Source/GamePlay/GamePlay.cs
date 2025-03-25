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
    public class GamePlay
    {
		bool lockUpdate;
		private MainMenu mainMenu;
		private CharSelectionScreen charSelectionScreen;
		private Dungeon dungeon;
		private Main main;

		PassObject ChangeGameState;

		public GamePlay(PassObject CHANGEGAMESTATE, Main main)
        {
			lockUpdate = false;
			ChangeGameState = CHANGEGAMESTATE;
			this.main = main;
			mainMenu = new MainMenu(this);
			charSelectionScreen = new CharSelectionScreen(this);

			ResetDungeon(null);
        }

		public void LoadContent(ContentManager content)
		{
			dungeon.LoadContent(content);
		}

		public virtual void Update()
        {
			lockUpdate = false;
			for (int i = 0; i < Globals.msgList.Count; i++)
			{
				Globals.msgList[i].Update();
				if (!Globals.msgList[i].done)
				{
					if (Globals.msgList[i].lockScreen)
					{
						lockUpdate = true;
					}
				}
				else
				{
					Globals.msgList.RemoveAt(i);
					i--;
				}

			}
			if (!lockUpdate)
			{
				if (Globals.gameState == -1)
				{
					main.Exit();
				}
				if (Globals.gameState == 0)
				{
					mainMenu.Update();
				}
				else if (Globals.gameState == 1)
				{
					charSelectionScreen.Update();
				}
				else if (Globals.gameState == 2)
				{
					dungeon.Update();
				}
			}



			
		}

		public void ChangeState(object newState)
		{
			Globals.gameState = (int)newState;
			//System.Diagnostics.Debug.WriteLine($"GamePlay state changed to: {Globals.gameState}");
			if(Globals.gameState == 2)
			{
				Globals.msgList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(200, 60), "Level 1", 4000, Color.Black, false));

			}
		}

		public virtual void ExitGame(object INFO)
		{
			Globals.gameState = 0;
		}

		public virtual void ResetDungeon(object INFO)
        {
            MapLayouts mapLayouts = new MapLayouts();
            dungeon = new Dungeon(ResetDungeon, mapLayouts, 1, ChangeGameState);
			dungeon.LoadContent(Globals.content);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
        {
			if (Globals.gameState == 0)
			{
				mainMenu.Draw();
			}
			else if (Globals.gameState == 1)
			{
				charSelectionScreen.Draw();
			}
			else if (Globals.gameState == 2)
			{
				dungeon.Draw(Vector2.Zero, spriteBatch);
			}

			for (int i = 0; i < Globals.msgList.Count; i++)
			{
				Globals.msgList[i].Draw();
			}
		}


    }
}
