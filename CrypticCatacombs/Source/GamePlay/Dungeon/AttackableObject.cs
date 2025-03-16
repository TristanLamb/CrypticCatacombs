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
    public class AttackableObject : Basic2d
    {
        public bool dead;

        public int ownerId;

        public float speed, hitDist, health, healthMax;
        public AttackableObject(string PATH, Vector2 POS, Vector2 DIMS, int OWNERID)
            : base(PATH, POS, DIMS)
        {
            ownerId = OWNERID;
            dead = false;
            speed = 2.0f;

            health = 1;
            healthMax = health;

            hitDist = 35.0f;
        }

        public virtual void Update(Vector2 OFFSET, Player ENEMY)
        {
            

            base.Update(OFFSET);
        }

		public virtual void GetHit(float DAMAGE)
        {
            health -= DAMAGE;

            if(health <= 0)
            {
				dead = true;
			}
        }

		public override void Draw(Vector2 OFFSET, SpriteEffects spriteEffects)
        {
            base.Draw(OFFSET, spriteEffects);
        }
    }
}
