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
    public class MainMenu
    {

		public Basic2d bkg;
		public List<Button> buttons = new List<Button>();

		public MainMenu(GamePlay gamePlay)
		{
			//System.Diagnostics.Debug.WriteLine("Main Menu Created");


			bkg = new Basic2d("2d/Backgrounds/MainMenu", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight));

			buttons.Add(new Button("2d/Misc/SimpleButton", new Vector2(10, 10), new Vector2(128, 40), new Vector2(1, 1), "Fonts/Arial16", "Play", gamePlay.ChangeState, 1));
			buttons.Add(new Button("2d/Misc/SimpleButton", new Vector2(10, 20), new Vector2(128, 40), new Vector2(1, 1), "Fonts/Arial16", "Exit", gamePlay.ChangeState, -1));
			
		}

		public virtual void Update()
		{
			//System.Diagnostics.Debug.WriteLine("MainMenu is updating!");
			bkg.Update(Vector2.Zero);

			for(int i = 0; i < buttons.Count; i++)
			{
				//System.Diagnostics.Debug.WriteLine($"Updating button {i}: {buttons[i].text}");
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
