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
    public class Skill
    {
        protected bool active;
        public bool done;

        public AttackableObject owner;

        public Effect2d targetEffect;

        public Skill(AttackableObject OWNER)
        {
			active = false;
			done = false;

            owner = OWNER;

			targetEffect = new TargettingCircle(new Vector2(0, 0), new Vector2(100, 100));
		}

		#region Properties
		public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                if(value && !active)
                {
                    targetEffect.done = false;
                    GameGlobals.PassEffect(targetEffect);
				}

                active = value;
            }
        }
        #endregion

        public virtual void Update(Vector2 OFFSET, Player ENEMY)
        {

            if(active && !done)
            {
                Targetting(OFFSET, ENEMY);
			}
        }

        public virtual void Reset()
        {
            active = false;
			done = false;
		}

        public virtual void Targetting(Vector2 OFFSET, Player ENEMY)
        {

            if(Globals.mouse.LeftClickRelease())
            {
				Active = false;
				done = false;
			}
        }

    }
}
