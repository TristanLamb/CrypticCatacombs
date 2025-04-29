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
	public class ConfirmMessage : Message
	{
		public Basic2d bkg;
		public Button confirmButton, denyButton;

		public PassObject ConfirmFunction, DenyFunction;

		public List<Button> buttons = new List<Button>();

		public ConfirmMessage(Vector2 POS, Vector2 DIMS, string MSG, Color COLOR, bool LOCKSCREEN, PassObject CONFIRMFUNCTION, PassObject DENYFUNCTION)
			: base(POS, DIMS, MSG, 5000, COLOR, LOCKSCREEN)
		{
			DIMS = new Vector2(320, 160);
			bkg = new Basic2d("2d/Misc/MessageBKG", POS, DIMS);

			//confirmButton = new Button("2d/Misc/shade", new Vector2(pos.X - 50, pos.Y + 40), new Vector2(70, 35), new Vector2(1, 2), "Fonts/Arial16", "Yes", new PassObject(ConfirmClick), 2);
			//denyButton = new Button("2d/Misc/shade", new Vector2(pos.X + 50, pos.Y + 40), new Vector2(70, 35), new Vector2(1, 2), "Fonts/Arial16", "No", new PassObject(DenyClick), 2);
			buttons.Add(new Button("2d/Misc/shade", new Vector2(440, -90), new Vector2(70, 35), new Vector2(1, 1), "Fonts/Arial16", "Yes", new PassObject(ConfirmClick), 1));
			buttons.Add(new Button("2d/Misc/shade", new Vector2(560, -135), new Vector2(70, 35), new Vector2(1, 1), "Fonts/Arial16", "No", new PassObject(DenyClick), -1));

			ConfirmFunction = CONFIRMFUNCTION;
			DenyFunction = DENYFUNCTION;
		}

		public ConfirmMessage(Vector2 POS, Vector2 DIMS, string MSG, Color COLOR, bool LOCKSCREEN, PassObject DENYFUNCTION)
			: base(POS, DIMS, MSG, 5000, COLOR, LOCKSCREEN)
		{
			DIMS = new Vector2(320, 160);
			bkg = new Basic2d("2d/Misc/MessageBKG", POS, DIMS);

			buttons.Add(new Button("2d/Misc/shade", new Vector2(490, -100), new Vector2(70, 35), new Vector2(1, 1), "Fonts/Arial16", "OK", new PassObject(DenyClick), -1));

			DenyFunction = DENYFUNCTION;
		}

		public override void Update()
		{
			//confirmButton.Update(Vector2.Zero);
			//denyButton.Update(Vector2.Zero);

			for (int i = 0; i < buttons.Count; i++)
			{
				//System.Diagnostics.Debug.WriteLine($"Updating button {i}: {buttons[i].text}");
				buttons[i].Update(new Vector2(325, 600 + 45 * i));
			}
		}

		public virtual void ConfirmClick(object INFO)
		{
			ConfirmFunction(INFO);
			done = true;
		}

		public virtual void DenyClick(object INFO)
		{
			DenyFunction(INFO);
			done = true;
		}

		public override void Draw()
		{

			bkg.Draw(Vector2.Zero);
			//confirmButton.Draw(Vector2.Zero);
			//denyButton.Draw(Vector2.Zero);

			textZone.color = color;
			textZone.Draw(new Vector2(pos.X - textZone.dims.X / 2, pos.Y - 30));

			for (int i = 0; i < buttons.Count; i++)
			{
				buttons[i].Draw();
			}
		}
	}

}
