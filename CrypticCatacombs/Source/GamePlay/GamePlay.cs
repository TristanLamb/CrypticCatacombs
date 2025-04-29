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
		List<int> mapLayoutIndex1 = new List<int> { 1, 2, 5};
		List<int> mapLayoutIndex2 = new List<int> { 3, 4 , 6 };

		public GamePlay(PassObject CHANGEGAMESTATE, Main main)
        {
			lockUpdate = false;
			ChangeGameState = CHANGEGAMESTATE;
			this.main = main;
			mainMenu = new MainMenu(this);
			charSelectionScreen = new CharSelectionScreen(this);

			Random rand = new Random();
			mapLayoutIndex1 = mapLayoutIndex1.OrderBy(x => rand.Next()).ToList();
			System.Diagnostics.Debug.WriteLine(string.Join(", ", mapLayoutIndex1));
			mapLayoutIndex2 = mapLayoutIndex2.OrderBy(x => rand.Next()).ToList();
			System.Diagnostics.Debug.WriteLine(string.Join(", ", mapLayoutIndex2));


			User user = new User(1);
			GameGlobals.user = user;
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
				Globals.msgList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(300, 60), $"Level_{GameGlobals.currentLevel}", 4000, Color.Black, false));

			}
		}

		public virtual void ExitGame(object INFO)
		{
			Globals.gameState = 0;
		}

		/*
		public virtual void ResetDungeon(object INFO)
        {
            MapLayouts mapLayouts = new MapLayouts();
            dungeon = new Dungeon(ResetDungeon, mapLayouts, GameGlobals.currentMapLayout, ChangeGameState);
			dungeon.LoadContent(Globals.content);
		}
		*/

		public virtual void ResetDungeon(object INFO)
		{
			//Random rand = new Random();
			//int randomNumber = rand.Next(1, 5); 
			int mapIndex = 0;
			if (GameGlobals.roomsCompleted == 0 || GameGlobals.roomsCompleted == 1)
			{
				if (mapLayoutIndex1.Count == 0)
				{
					mapLayoutIndex1 = new List<int> { 1, 2, 5 }.OrderBy(x => new Random().Next()).ToList();
				}
				mapIndex = mapLayoutIndex1[0];
				mapLayoutIndex1.RemoveAt(0);
			}
			if (GameGlobals.roomsCompleted == 2)
			{
				if (mapLayoutIndex2.Count == 0)
				{
					mapLayoutIndex2 = new List<int> { 3, 4, 6 }.OrderBy(x => new Random().Next()).ToList();
				}
				mapIndex = mapLayoutIndex2[0];
				mapLayoutIndex2.RemoveAt(0);
			}
			if (GameGlobals.roomsCompleted == 3)
			{
				//change to shop
				mapIndex = 9;
				//mapIndex = mapLayoutIndex2[0];
				//mapLayoutIndex2.RemoveAt(0);
			}
			if (GameGlobals.roomsCompleted == 4)
			{
				if (mapLayoutIndex2.Count == 0)
				{
					mapLayoutIndex2 = new List<int> { 3, 4, 6 }.OrderBy(x => new Random().Next()).ToList();
				}
				mapIndex = mapLayoutIndex2[0];
				mapLayoutIndex2.RemoveAt(0);
			}
			if (GameGlobals.roomsCompleted == 5)
			{
				//boss room
				mapIndex = 10;
			}

			//LEVEL 2
			if (GameGlobals.roomsCompleted == 6 || GameGlobals.roomsCompleted == 7)
			{
				if (mapLayoutIndex1.Count == 0)
				{
					mapLayoutIndex1 = new List<int> { 1, 2, 5 }.OrderBy(x => new Random().Next()).ToList();
				}
				mapIndex = mapLayoutIndex1[0];
				mapLayoutIndex1.RemoveAt(0);
			}
			if (GameGlobals.roomsCompleted == 8)
			{
				if (mapLayoutIndex2.Count == 0)
				{
					mapLayoutIndex2 = new List<int> { 3, 4, 6 }.OrderBy(x => new Random().Next()).ToList();
				}
				mapIndex = mapLayoutIndex2[0];
				mapLayoutIndex2.RemoveAt(0);
			}
			if (GameGlobals.roomsCompleted == 9)
			{
				//change to shop
				mapIndex = 9;
				//mapIndex = mapLayoutIndex2[0];
				//mapLayoutIndex2.RemoveAt(0);
			}
			if (GameGlobals.roomsCompleted == 10)
			{
				if (mapLayoutIndex2.Count == 0)
				{
					mapLayoutIndex2 = new List<int> { 3, 4, 6 }.OrderBy(x => new Random().Next()).ToList();
				}
				mapIndex = mapLayoutIndex2[0];
				mapLayoutIndex2.RemoveAt(0);
			}
			if (GameGlobals.roomsCompleted == 11)
			{
				//boss room
				mapIndex = 10;
			}

			MapLayouts mapLayouts = new MapLayouts();
			User user = GameGlobals.user;

			//	mapIndex = 10; //hardcode index/map for testing
			if (mapIndex == 0)
			{
				GameGlobals.user.wizard.pos = new Vector2(200, 200);
			}
			if (mapIndex == 1)
			{
				GameGlobals.user.wizard.pos = new Vector2(550, 480);
			}
			if (mapIndex == 2)
			{
				GameGlobals.user.wizard.pos = new Vector2(450, 500);
			}
			if (mapIndex == 3)
			{
				GameGlobals.user.wizard.pos = new Vector2(235, 300);
			}
			if (mapIndex == 4)
			{
				GameGlobals.user.wizard.pos = new Vector2(600, 275);
			}
			if (mapIndex == 5)
			{
				GameGlobals.user.wizard.pos = new Vector2(700, 250);
			}
			if (mapIndex == 6)
			{
				GameGlobals.user.wizard.pos = new Vector2(300, 830);
			}
			if (mapIndex == 7)
			{
				GameGlobals.user.wizard.pos = new Vector2(300, 830);
			}
			if (mapIndex == 8)
			{
				GameGlobals.user.wizard.pos = new Vector2(300, 830);
			}
			if (mapIndex == 9)
			{
				GameGlobals.user.wizard.pos = new Vector2(400, 500);
			}
			if (mapIndex == 10)
			{
				GameGlobals.user.wizard.pos = new Vector2(420, 470);
			}


			dungeon = new Dungeon(ResetDungeon, mapLayouts, mapIndex, ChangeGameState, user);
			dungeon.LoadContent(Globals.content);
		}
		/*
		to reshuffle list for index
		if (mapLayouts.Count == 0)
		{
			mapLayouts = new List<int> { 1, 2, 3, 4 }
				.OrderBy(x => rand.Next()).ToList();
		}
		*/

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
