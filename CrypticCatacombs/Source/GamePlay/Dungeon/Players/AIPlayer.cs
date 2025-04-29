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
			if (GameGlobals.currentMapLayout == 1)
			{
				spawnPoints.Add(new SpawnPoint("2d/Projectiles/Teleport", new Vector2(75, 125), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", false));

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth / 2, 100), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500); //spawn delay for next mob

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth - 50, 100), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth - 50, 575), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);
			}
			if (GameGlobals.currentMapLayout == 2)
			{
				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(78, 120), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(78, 820), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth - 270, 120), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth - 270, 475), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth - 270, 820), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);
			}
			if (GameGlobals.currentMapLayout == 3)
			{
				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(78, 820), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(20000);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(278, 820), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(20000);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth / 2 + 250, 550), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Projectiles/Teleport", new Vector2(Globals.screenWidth / 2 + 250, 665), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", false));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth / 2 + 250, 780), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);
			}
			if (GameGlobals.currentMapLayout == 4)
			{
				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(78, 820), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(20000);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(278, 820), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(20000);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth / 2 + 250, 550), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Projectiles/Teleport", new Vector2(Globals.screenWidth / 2 + 250, 665), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", false));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth / 2 + 250, 780), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);
			}
			if (GameGlobals.currentMapLayout == 5)
			{
				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(78, 820), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(40000);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(278, 820), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(40000);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth / 2 + 250, 550), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth / 2 + 390, 355), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(Globals.screenWidth / 2 + 230, 780), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);
			}
			if (GameGlobals.currentMapLayout == 6)
			{
				spawnPoints.Add(new SpawnPoint("2d/Projectiles/Teleport", new Vector2(1150, 100), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", false));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(70000);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(200, 500), new Vector2(35, 35), new Vector2(1, 1), id, "Slime", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(40000);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(200, 130), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(450, 150), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(1400, 300), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);

				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(1350, 400), new Vector2(35, 35), new Vector2(1, 1), id, "SkeletonArcher", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(0);
			}
			if (GameGlobals.currentMapLayout == 10)
			{
				spawnPoints.Add(new SpawnPoint("2d/Misc/nothing", new Vector2(1000, 480), new Vector2(35, 35), new Vector2(1, 1), id, "EliteOrc", true));
				spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(70000);

			}

		}

        public override void Update(Player ENEMY, Vector2 OFFSET, SquareGrid GRID)
        {
			base.Update(ENEMY, OFFSET, GRID);
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
