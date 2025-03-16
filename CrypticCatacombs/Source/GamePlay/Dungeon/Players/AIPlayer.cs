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
			spawnPoints.Add(new SpawnPoint("2d/Misc/circle", new Vector2(50, 50), new Vector2(35, 35), id));

			spawnPoints.Add(new SpawnPoint("2d/Misc/circle", new Vector2(Globals.screenWidth / 2, 50), new Vector2(35, 35), id));
			spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500); //spawn delay for next mob

			spawnPoints.Add(new SpawnPoint("2d/Misc/circle", new Vector2(Globals.screenWidth - 50, 50), new Vector2(35, 35), id));
			spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);
		}

        public override void Update(Player ENEMY, Vector2 OFFSET)
        {
			base.Update(ENEMY, OFFSET);
		}

        public override void ChangeScore(int SCORE)
        {
            GameGlobals.score += SCORE;
        }

    }
}
