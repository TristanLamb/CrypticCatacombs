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
    public class User : Player
    {


        public User(int ID) : base(ID)
        {
			wizard = new Wizard("2d/Units/WizardIdleSheet", new Vector2(300, 300), new Vector2(212, 212), new Vector2(6, 1), id);
		}

        public override void Update(Player ENEMY, Vector2 OFFSET, SquareGrid GRID)
        {
            base.Update(ENEMY, OFFSET, GRID);
        }

    }
}
