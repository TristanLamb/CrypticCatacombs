using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CrypticCatacombs
{
    public class GridLocation
    {
        public bool filled, impassable, unPathable, hasBeenUsed, isViewable, door;
        public float fScore, cost, currentDist;
        public Vector2 parent, pos;

		public GridLocation(float COST, bool FILLED)
        {
            cost = COST;
            filled = FILLED;

            hasBeenUsed = false;
			isViewable = false;
			unPathable = false;
            impassable = false;
			door = false;
        }

		public GridLocation(Vector2 POS, float COST, bool FILLED, float FSCORE)
		{
			cost = COST;
			filled = FILLED;
			impassable = FILLED;
			unPathable = false;
			hasBeenUsed = false;
			isViewable = false;

			pos = POS;

			fScore = FSCORE;
		}

		public void SetNode(Vector2 PARENT, float FSCORE, float CURRENTDIST)
		{
			parent = PARENT;
			fScore = FSCORE;
			currentDist = CURRENTDIST;
		}

		public virtual void SetToFilled(bool IMPASSABLE)
        {
            filled = true;
            impassable = IMPASSABLE;
		}

		public virtual void SetToDoor(bool IMPASSABLE)
		{
			filled = true;
			impassable = IMPASSABLE;
			door = true;
		}
	}
}
