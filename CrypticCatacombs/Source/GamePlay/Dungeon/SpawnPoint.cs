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
    public class SpawnPoint : AttackableObject
    {

        public CustomTimer spawnTimer = new CustomTimer(2200);
        public SpawnPoint(string PATH, Vector2 POS, Vector2 DIMS, int OWNERID)
            : base(PATH, POS, DIMS, OWNERID)
        {
            dead = false;

			health = 3;
			healthMax = health;

			hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET)
        {
            spawnTimer.UpdateTimer();
            if(spawnTimer.Test()) //testing timer
            {
                SpawnMob();
                spawnTimer.ResetToZero();
            }


            base.Update(OFFSET);
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Slime(new Vector2(pos.X, pos.Y), ownerId));
        }

		public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
