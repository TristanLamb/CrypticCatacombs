using System;
using System.Collections.Generic;
using System.Data;
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
        private string mobType;
        public bool spawnOnce;

		public CustomTimer spawnTimer = new CustomTimer(2200); //setting spawn delay
        public SpawnPoint(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID, string mobType = "Slime", bool spawnOnce = false)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
            this.mobType = mobType;
			this.spawnOnce = spawnOnce;
			dead = false;

			health = 3;
			healthMax = health;
            
			hitDist = 35.0f;
		}

        public override void Update(Vector2 OFFSET)
        {
            if (!spawnOnce || !dead)
            {
				spawnTimer.UpdateTimer();
				if (spawnTimer.Test()) //testing timer
				{
					SpawnMob();
					if (spawnOnce)
                    {
						dead = true;
					}
                    else
                    {
						spawnTimer.ResetToZero();
					}
				}
			}
			
            base.Update(OFFSET);
        }

        public virtual void SpawnMob()
        {
            if (mobType == "Slime")
            {
                GameGlobals.PassMob(new Slime(new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId));
            }
            else if (mobType == "SkeletonArcher")
            {
                GameGlobals.PassMob(new SkeletonArcher(new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId));
            }
		}

		public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
