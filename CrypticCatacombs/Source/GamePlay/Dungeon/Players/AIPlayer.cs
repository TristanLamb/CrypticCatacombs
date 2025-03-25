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
    public class AIPlayer : Player
    {


        public AIPlayer(int ID)
            : base(ID)
		{
			//set to true for 1 spawn
			spawnPoints.Add(new SpawnPoint("2d/Misc/circle", new Vector2(50, 50), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", false));

			spawnPoints.Add(new SpawnPoint("2d/Misc/circle", new Vector2(Globals.screenWidth / 2, 50), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
			spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500); //spawn delay for next mob

			spawnPoints.Add(new SpawnPoint("2d/Misc/circle", new Vector2(Globals.screenWidth - 50, 50), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
			spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);
		}

        public override void Update(Player ENEMY, Vector2 OFFSET)
        {
			base.Update(ENEMY, OFFSET);
			spawnPoints = spawnPoints.Where(sp => !sp.dead).ToList();
		}

        public override void ChangeScore(float SCORE)
        {
            GameGlobals.score += SCORE;
        }

		public override void CheckIfDefeated()
		{
			if(spawnPoints.Count <= 0 && units.Count <= 0)
			{
				defeated = true;
			}
		}

	}
}
