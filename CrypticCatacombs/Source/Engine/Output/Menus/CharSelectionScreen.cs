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
    public class CharSelectionScreen
    {

		public Basic2d bkg;
		public List<Button> buttons = new List<Button>();

		public CharSelectionScreen(GamePlay gamePlay)
		{

			System.Diagnostics.Debug.WriteLine("CharSelectionScreen Created");


			bkg = new Basic2d("2d/Backgrounds/CharSelectionBkg", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight));

			buttons.Add(new Button("2d/Misc/SimpleButton", new Vector2(10, -50), new Vector2(128, 40), "Fonts/Arial16", "Wizard", gamePlay.ChangeState, 2));
			buttons.Add(new Button("2d/Misc/SimpleButton", new Vector2(10, -40), new Vector2(128, 40), "Fonts/Arial16", "Swordsman", gamePlay.ChangeState, 2));
			buttons.Add(new Button("2d/Misc/SimpleButton", new Vector2(10, -30), new Vector2(128, 40), "Fonts/Arial16", "Archer", gamePlay.ChangeState, 2));
			buttons.Add(new Button("2d/Misc/SimpleButton", new Vector2(10, 20), new Vector2(128, 40), "Fonts/Arial16", "Back", gamePlay.ChangeState, 0));
		}

		public virtual void Update()
		{
			//System.Diagnostics.Debug.WriteLine("MainMenu is updating!");
			bkg.Update(Vector2.Zero);

			for(int i = 0; i < buttons.Count; i++)
			{
				buttons[i].Update(new Vector2(325, 600 + 45 * i));
			}
		}

		public virtual void Draw()
		{
			bkg.Draw();

			for (int i = 0; i < buttons.Count; i++)
			{
				buttons[i].Draw();
			}
		}


	}
}
