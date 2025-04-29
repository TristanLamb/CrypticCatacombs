using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrypticCatacombs
{
    public class AttackableObject : Animated2d
    {
        public bool dead;

        public int ownerId, killValue;

        public float speed, hitDist, health, healthMax, mana, manaMax;


		public AttackableObject(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(PATH, POS, DIMS, FRAMES, Color.White)
        {
            ownerId = OWNERID;
            dead = false;
            speed = 2.0f;

            health = 1;
            healthMax = health;

            mana = 100;
            manaMax = mana;

			killValue = 1;

            hitDist = 35.0f;

		}

        public virtual void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
			

			base.Update(OFFSET);
        }

		public virtual void GetHit(AttackableObject ATTACKER, float DAMAGE)
        {
			health -= DAMAGE;

            if (health <= 0)
            {
                dead = true;

                GameGlobals.PassGold(new PlayerValuePacket(ATTACKER.ownerId, killValue)); //gives attacker killvalue in gold
            }
        }


		public override void Draw(Vector2 OFFSET, SpriteEffects spriteEffects)
        {
            base.Draw(OFFSET, spriteEffects);
        }
    }
}
