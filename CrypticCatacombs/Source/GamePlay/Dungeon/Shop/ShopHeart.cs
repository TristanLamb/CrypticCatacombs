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
    public class ShopHeart : Animated2d
    {
        public bool done;
		public DismissableMessage message;
		public UI ui;


		public ShopHeart(string PATH, Vector2 POS, UI ui)
            : base(PATH, POS, new Vector2(35, 35), new Vector2(1, 1), Color.White)
        {
            done = false;
			message = null;
			this.ui = ui;
		}

        public virtual void Update(Vector2 OFFSET)
        {
			if (Globals.mouse.LeftClick() && Hover(OFFSET))
			{
				if (GameGlobals.user.gold >= 15)
				{
					Globals.msgList.Add(new ConfirmMessage(
					new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2),
					new Vector2(300, 150),
					"Are you sure you want to buy this HEART CRYSTAL for 15G?",
					Color.Black,
					true,
					new PassObject(ConfirmPurchase),
					new PassObject(DenyPurchase)
				));
				}
				else
				{
					Globals.msgList.Add(new ConfirmMessage(
					new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2),
					new Vector2(300, 150),
					"Not enough gold!",
					Color.Black,
					true,
					new PassObject(DenyPurchase)
					));
				}
			}
		}

		public virtual void ConfirmPurchase(object INFO)
		{
			int healthGained = 5;
			done = true;
			GameGlobals.user.wizard.health += healthGained;
			GameGlobals.user.wizard.healthMax += healthGained;
			ui.NewHealthBar(healthGained);
			GameGlobals.user.gold -= 15;
		}

		public void DenyPurchase(object info)
		{
			// do nothing
		}

		public virtual void Draw(Vector2 OFFSET)
		{
			if (!done)
			{
				base.Draw(OFFSET);
			}

			if (message != null)
			{
				message.Draw();
			}
		}

	}
}
