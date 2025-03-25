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
		private MainMenu mainMenu;
		private CharSelectionScreen charSelectionScreen;
		private Dungeon dungeon;

		public GamePlay()
        {
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

			//System.Diagnostics.Debug.WriteLine("Gameplay is updating!");
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

		public void ChangeState(int newState)
		{
			Globals.gameState = newState;
			System.Diagnostics.Debug.WriteLine($"GamePlay state changed to: {Globals.gameState}");
		}

		public virtual void ExitGame(object INFO)
		{
			Globals.gameState = 0;
		}

		public virtual void ResetDungeon(object INFO)
        {
            dungeon = new Dungeon(ResetDungeon);
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
		}


    }
}
