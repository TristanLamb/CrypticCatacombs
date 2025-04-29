using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrypticCatacombs
{
    public class SquareGrid
    {

		public bool showGrid;
		public Vector2 slotDims, gridDims, physicalStartPos, totalPhysicalDims, currentHoverSlot; //slotDims = size of each grid
		public Basic2d gridImg;
		public bool enemiesDefeated = false;

		public List<GridItem> gridItems = new List<GridItem>();
		public List<List<GridLocation>> slots = new List<List<GridLocation>>();

		public SquareGrid(Vector2 SLOTDIMS, Vector2 STARTPOS, Vector2 TOTALDIMS)
        {
			showGrid = false;

			slotDims = SLOTDIMS;

			physicalStartPos = new Vector2((int)STARTPOS.X, (int)STARTPOS.Y);
			totalPhysicalDims = new Vector2((int)TOTALDIMS.X, (int)TOTALDIMS.Y);

			currentHoverSlot = new Vector2(-1, -1);

			SetBaseGrid();

			gridImg = new Basic2d("2d\\Misc\\shade", slotDims / 2, new Vector2(slotDims.X - 2, slotDims.Y - 2));

		}

        public virtual void Update(Vector2 OFFSET)
        {
            currentHoverSlot = GetSlotFromPixel(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), -OFFSET);

			float tileSize = 31f;
			Vector2 wizardGridCoords = new Vector2(
				(float)Math.Floor(GameGlobals.user.wizard.pos.X / tileSize),
				(float)Math.Floor(GameGlobals.user.wizard.pos.Y / tileSize)
			);
			//System.Diagnostics.Debug.WriteLine($"Wizard Grid Position: {wizardGridCoords}");
			if(enemiesDefeated == true)
			{
				if (GameGlobals.currentMapLayout == 0)
				{
					if (wizardGridCoords.X == 48 && wizardGridCoords.Y == 1
					|| wizardGridCoords.X == 49 && wizardGridCoords.Y == 1
					|| wizardGridCoords.X == 50 && wizardGridCoords.Y == 1
					|| wizardGridCoords.X == 3 && wizardGridCoords.Y == 28
					|| wizardGridCoords.X == 4 && wizardGridCoords.Y == 28
					|| wizardGridCoords.X == 5 && wizardGridCoords.Y == 28)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}

				}
				if (GameGlobals.currentMapLayout == 1)
				{
					if (wizardGridCoords.X == 24 && wizardGridCoords.Y == 0
					|| wizardGridCoords.X == 25 && wizardGridCoords.Y == 0
					|| wizardGridCoords.X == 26 && wizardGridCoords.Y == 0)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}

				}
				if (GameGlobals.currentMapLayout == 2)
				{
					if (wizardGridCoords.X == 24 && wizardGridCoords.Y == 10
					|| wizardGridCoords.X == 25 && wizardGridCoords.Y == 10
					|| wizardGridCoords.X == 26 && wizardGridCoords.Y == 10
					|| wizardGridCoords.X == 24 && wizardGridCoords.Y == 22
					|| wizardGridCoords.X == 25 && wizardGridCoords.Y == 22
					|| wizardGridCoords.X == 26 && wizardGridCoords.Y == 2)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}
				}
				if (GameGlobals.currentMapLayout == 3)
				{
					if (wizardGridCoords.X == 38 && wizardGridCoords.Y == 21
					|| wizardGridCoords.X == 38 && wizardGridCoords.Y == 22
					|| wizardGridCoords.X == 38 && wizardGridCoords.Y == 23)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}
				}
				if (GameGlobals.currentMapLayout == 4)
				{
					if (wizardGridCoords.X == 0 && wizardGridCoords.Y == 13
					|| wizardGridCoords.X == 0 && wizardGridCoords.Y == 14
					|| wizardGridCoords.X == 0 && wizardGridCoords.Y == 15
					|| wizardGridCoords.X == 53 && wizardGridCoords.Y == 13
					|| wizardGridCoords.X == 53 && wizardGridCoords.Y == 14
					|| wizardGridCoords.X == 53 && wizardGridCoords.Y == 15)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}
				}
				if (GameGlobals.currentMapLayout == 5)
				{
					if (wizardGridCoords.X == 31 && wizardGridCoords.Y == 0
					|| wizardGridCoords.X == 32 && wizardGridCoords.Y == 0
					|| wizardGridCoords.X == 33 && wizardGridCoords.Y == 0
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 24
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 25
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 26)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}
				}
				if (GameGlobals.currentMapLayout == 6)
				{
					if (wizardGridCoords.X == 3 && wizardGridCoords.Y == 4
					|| wizardGridCoords.X == 3 && wizardGridCoords.Y == 5
					|| wizardGridCoords.X == 3 && wizardGridCoords.Y == 6
					|| wizardGridCoords.X == 43 && wizardGridCoords.Y == 20
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 20
					|| wizardGridCoords.X == 45 && wizardGridCoords.Y == 20)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}
				}
				if (GameGlobals.currentMapLayout == 9)
				{
					if (wizardGridCoords.X == 44 && wizardGridCoords.Y == 13
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 14
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 15
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 16)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}
				}
				if (GameGlobals.currentMapLayout == 10)
				{
					if (wizardGridCoords.X == 44 && wizardGridCoords.Y == 13
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 14
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 15
					|| wizardGridCoords.X == 44 && wizardGridCoords.Y == 16)
					{
						// The wizard is touching a door
						GameGlobals.nextRoom = true;
					}
				}
			}
		}

		public virtual void LoadDoor()
		{
			enemiesDefeated = true;
			if (GameGlobals.currentMapLayout == 0)
			{
				AddGridItem("2d/Map/doorTopOpenL", new Vector2(48, 1));
				AddGridItem("2d/Map/floor", new Vector2(49, 1));
				AddGridItem("2d/Map/doorTopOpenR", new Vector2(50, 1));
				AddGridItem("2d/Map/doorBottomOpenL", new Vector2(3, 28));
				AddGridItem("2d/Map/floor", new Vector2(4, 28));
				AddGridItem("2d/Map/doorBottomOpenR", new Vector2(5, 28));
			}
			if (GameGlobals.currentMapLayout == 1)
			{
				AddGridItem("2d/Map/doorTopOpenL", new Vector2(24, 0));
				AddGridItem("2d/Map/floor", new Vector2(25, 0));
				AddGridItem("2d/Map/doorTopOpenR", new Vector2(26, 0));
			}
			if (GameGlobals.currentMapLayout == 2)
			{
				AddGridItem("2d/Map/doorTopOpenL", new Vector2(24, 10));
				AddGridItem("2d/Map/floor", new Vector2(25, 10));
				AddGridItem("2d/Map/doorTopOpenR", new Vector2(26, 10));
				AddGridItem("2d/Map/doorTopOpenL", new Vector2(24, 22));
				AddGridItem("2d/Map/floor", new Vector2(25, 22));
				AddGridItem("2d/Map/doorTopOpenR", new Vector2(26, 22));
			}
			if (GameGlobals.currentMapLayout == 3)
			{
				AddGridItem("2d/Map/doorRightOpenT", new Vector2(38, 20));
				AddGridItem("2d/Map/floor", new Vector2(38, 21));
				AddGridItem("2d/Map/doorRightOpenB", new Vector2(38, 22));
			}
			if (GameGlobals.currentMapLayout == 4)
			{
				AddGridItem("2d/Map/doorLeftOpenT", new Vector2(0, 13));
				AddGridItem("2d/Map/floor", new Vector2(0, 14));
				AddGridItem("2d/Map/doorLeftOpenB", new Vector2(0, 15));
				AddGridItem("2d/Map/doorRightOpenT", new Vector2(53, 13));
				AddGridItem("2d/Map/floor", new Vector2(53, 14));
				AddGridItem("2d/Map/doorRightOpenB", new Vector2(53, 15));
			}
			if (GameGlobals.currentMapLayout == 5)
			{
				AddGridItem("2d/Map/doorTopOpenL", new Vector2(31, 0));
				AddGridItem("2d/Map/floor", new Vector2(32, 0));
				AddGridItem("2d/Map/doorTopOpenR", new Vector2(33, 0));
				AddGridItem("2d/Map/doorRightOpenT", new Vector2(44, 24));
				AddGridItem("2d/Map/floor", new Vector2(44, 25));
				AddGridItem("2d/Map/doorRightOpenB", new Vector2(44, 26));
			}
			if (GameGlobals.currentMapLayout == 6)
			{
				AddGridItem("2d/Map/doorLeftOpenT", new Vector2(3, 4));
				AddGridItem("2d/Map/floor", new Vector2(3, 5));
				AddGridItem("2d/Map/doorLeftOpenB", new Vector2(3, 6));
				AddGridItem("2d/Map/doorBottomOpenL", new Vector2(43, 20));
				AddGridItem("2d/Map/floor", new Vector2(44, 20));
				AddGridItem("2d/Map/doorBottomOpenR", new Vector2(45, 20));
			}
			if (GameGlobals.currentMapLayout == 9)
			{
				AddGridItem("2d/Map/doorRightOpenT", new Vector2(44, 13));
				AddGridItem("2d/Map/floor", new Vector2(44, 14));
				AddGridItem("2d/Map/floor", new Vector2(44, 15));
				AddGridItem("2d/Map/doorRightOpenB", new Vector2(44, 16));
				//AddGridItem("2d/Misc/Heart", new Vector2(12, 11));
			}
			if (GameGlobals.currentMapLayout == 10)
			{
				AddGridItem("2d/Map/doorRightOpenT", new Vector2(44, 13));
				AddGridItem("2d/Map/floor", new Vector2(44, 14));
				AddGridItem("2d/Map/floor", new Vector2(44, 15));
				AddGridItem("2d/Map/doorRightOpenB", new Vector2(44, 16));
			}

		}

		public virtual void AddGridItem(string PATH, Vector2 LOC)
		{
			gridItems.Add(new GridItem(PATH, GetPosFromLoc(LOC) + slotDims / 2, new Vector2(slotDims.X, slotDims.Y), new Vector2(1, 1)));

			var slot = GetSlotFromLocation(LOC);
			if (slot == null)
			{
				System.Diagnostics.Debug.WriteLine($"Slot at location {LOC} is null!");
				return;
			}

			slot.SetToFilled(true);
		}

		public virtual Vector2 GetPosFromLoc(Vector2 LOC)
		{
			return physicalStartPos + new Vector2((int)LOC.X * slotDims.X, (int)LOC.Y * slotDims.Y);
		}


		public virtual GridLocation GetSlotFromLocation(Vector2 LOC)
		{
			if (LOC.X >= 0 && LOC.Y >= 0 && LOC.X < slots.Count && LOC.Y < slots[(int)LOC.X].Count)
			{
				return slots[(int)LOC.X][(int)LOC.Y];
			}

			return null;
		}

		public virtual Vector2 GetSlotFromPixel(Vector2 PIXEL, Vector2 OFFSET)
		{
			Vector2 adjustedPos = PIXEL - physicalStartPos + OFFSET;

			Vector2 tempVec = new Vector2(Math.Min(Math.Max(0, (int)(adjustedPos.X / slotDims.X)), slots.Count - 1), Math.Min(Math.Max(0, (int)(adjustedPos.Y / slotDims.Y)), slots[0].Count - 1));

			return tempVec;
		}

		public virtual void SetBaseGrid()
        {
            gridDims = new Vector2((int)(totalPhysicalDims.X / slotDims.X), (int)(totalPhysicalDims.Y / slotDims.Y));
            slots.Clear();
            for(int i = 0; i < gridDims.X; i++)
            {
                slots.Add(new List<GridLocation>());
                for(int j = 0; j < gridDims.Y; j++)
                {
                    slots[i].Add(new GridLocation(1, false));
                }
            }
		}

		#region A* (A Star)

		public List<Vector2> GetPath(Vector2 START, Vector2 END, bool ALLOWDIAGNALS)
		{
			List<GridLocation> viewable = new List<GridLocation>(), used = new List<GridLocation>();

			List<List<GridLocation>> masterGrid = new List<List<GridLocation>>();


			bool impassable = false;
			float cost = 1;
			for (int i = 0; i < slots.Count; i++)
			{
				masterGrid.Add(new List<GridLocation>());
				for (int j = 0; j < slots[i].Count; j++)
				{
					impassable = slots[i][j].impassable;

					if (slots[i][j].impassable || slots[i][j].filled)
					{
						if (i != (int)END.X || j != (int)END.Y)
						{
							impassable = true;
						}
					}

					cost = slots[i][j].cost;

					masterGrid[i].Add(new GridLocation(new Vector2(i, j), cost, impassable, 99999999));
				}
			}

			viewable.Add(masterGrid[(int)START.X][(int)START.Y]);

			while (viewable.Count > 0 && !(viewable[0].pos.X == END.X && viewable[0].pos.Y == END.Y))
			{
				TestAStarNode(masterGrid, viewable, used, END, ALLOWDIAGNALS);
			}


			List<Vector2> path = new List<Vector2>();

			if (viewable.Count > 0)
			{
				int currentViewableStart = 0;
				GridLocation currentNode = viewable[currentViewableStart];

				path.Clear();
				Vector2 tempPos;


				while (true)
				{

					//Add the difference between the actual grid and the custom grid back in...
					tempPos = GetPosFromLoc(currentNode.pos) + slotDims / 2;
					path.Add(new Vector2(tempPos.X, tempPos.Y));

					if (currentNode.pos == START)
					{
						break;
					}
					else
					{

						if ((int)currentNode.parent.X != -1 && (int)currentNode.parent.Y != -1)
						{
							if (currentNode.pos.X == masterGrid[(int)currentNode.parent.X][(int)currentNode.parent.Y].pos.X && currentNode.pos.Y == masterGrid[(int)currentNode.parent.X][(int)currentNode.parent.Y].pos.Y)
							{
								//Current node points to its self
								currentNode = viewable[currentViewableStart];
								currentViewableStart++;
							}


							currentNode = masterGrid[(int)currentNode.parent.X][(int)currentNode.parent.Y];
						}
						else
						{
							//Node is off grid...
							currentNode = viewable[currentViewableStart];
							currentViewableStart++;
						}


					}

				}



				path.Reverse();

				if (path.Count > 1)
				{
					path.RemoveAt(0);
				}
			}

			return path;
		}

		public void TestAStarNode(List<List<GridLocation>> masterGrid, List<GridLocation> viewable, List<GridLocation> used, Vector2 end, bool ALLOWDIAGNALS)
		{

			GridLocation currentNode;
			bool up = true, down = true, left = true, right = true;

			//Above
			if (viewable[0].pos.Y > 0 && viewable[0].pos.Y < masterGrid[0].Count && !masterGrid[(int)viewable[0].pos.X][(int)viewable[0].pos.Y - 1].impassable)
			{
				currentNode = masterGrid[(int)viewable[0].pos.X][(int)viewable[0].pos.Y - 1];
				up = currentNode.impassable;
				SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].pos.X, viewable[0].pos.Y), viewable[0].currentDist, end, 1);
			}

			//Below
			if (viewable[0].pos.Y >= 0 && viewable[0].pos.Y + 1 < masterGrid[0].Count && !masterGrid[(int)viewable[0].pos.X][(int)viewable[0].pos.Y + 1].impassable)
			{
				currentNode = masterGrid[(int)viewable[0].pos.X][(int)viewable[0].pos.Y + 1];
				down = currentNode.impassable;
				SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].pos.X, viewable[0].pos.Y), viewable[0].currentDist, end, 1);
			}

			//Left
			if (viewable[0].pos.X > 0 && viewable[0].pos.X < masterGrid.Count && !masterGrid[(int)viewable[0].pos.X - 1][(int)viewable[0].pos.Y].impassable)
			{
				currentNode = masterGrid[(int)viewable[0].pos.X - 1][(int)viewable[0].pos.Y];
				left = currentNode.impassable;
				SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].pos.X, viewable[0].pos.Y), viewable[0].currentDist, end, 1);
			}

			//Right
			if (viewable[0].pos.X >= 0 && viewable[0].pos.X + 1 < masterGrid.Count && !masterGrid[(int)viewable[0].pos.X + 1][(int)viewable[0].pos.Y].impassable)
			{
				currentNode = masterGrid[(int)viewable[0].pos.X + 1][(int)viewable[0].pos.Y];
				right = currentNode.impassable;
				SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].pos.X, viewable[0].pos.Y), viewable[0].currentDist, end, 1);
			}

			if (ALLOWDIAGNALS)
			{
				// Up and Right
				if (viewable[0].pos.X >= 0 && viewable[0].pos.X + 1 < masterGrid.Count && viewable[0].pos.Y > 0 && viewable[0].pos.Y < masterGrid[0].Count && !masterGrid[(int)viewable[0].pos.X + 1][(int)viewable[0].pos.Y - 1].impassable && (!up || !right))
				{
					currentNode = masterGrid[(int)viewable[0].pos.X + 1][(int)viewable[0].pos.Y - 1];

					SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].pos.X, viewable[0].pos.Y), viewable[0].currentDist, end, (float)Math.Sqrt(2));
				}

				//Down and Right
				if (viewable[0].pos.X >= 0 && viewable[0].pos.X + 1 < masterGrid.Count && viewable[0].pos.Y >= 0 && viewable[0].pos.Y + 1 < masterGrid[0].Count && !masterGrid[(int)viewable[0].pos.X + 1][(int)viewable[0].pos.Y + 1].impassable && (!down || !right))
				{
					currentNode = masterGrid[(int)viewable[0].pos.X + 1][(int)viewable[0].pos.Y + 1];

					SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].pos.X, viewable[0].pos.Y), viewable[0].currentDist, end, (float)Math.Sqrt(2));
				}

				//Down and Left
				if (viewable[0].pos.X > 0 && viewable[0].pos.X < masterGrid.Count && viewable[0].pos.Y >= 0 && viewable[0].pos.Y + 1 < masterGrid[0].Count && !masterGrid[(int)viewable[0].pos.X - 1][(int)viewable[0].pos.Y + 1].impassable && (!down || !left))
				{
					currentNode = masterGrid[(int)viewable[0].pos.X - 1][(int)viewable[0].pos.Y + 1];

					SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].pos.X, viewable[0].pos.Y), viewable[0].currentDist, end, (float)Math.Sqrt(2));
				}

				// Up and Left
				if (viewable[0].pos.X > 0 && viewable[0].pos.X < masterGrid.Count && viewable[0].pos.Y > 0 && viewable[0].pos.Y < masterGrid[0].Count && !masterGrid[(int)viewable[0].pos.X - 1][(int)viewable[0].pos.Y - 1].impassable && (!up || !left))
				{
					currentNode = masterGrid[(int)viewable[0].pos.X - 1][(int)viewable[0].pos.Y - 1];

					SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].pos.X, viewable[0].pos.Y), viewable[0].currentDist, end, (float)Math.Sqrt(2));
				}
			}


			viewable[0].hasBeenUsed = true;
			used.Add(viewable[0]);
			viewable.RemoveAt(0);



			// sort
			/*viewable.Sort(delegate(AStarNode n1, AStarNode n2)
            {
                return n1.FScore.CompareTo(n2.FScore);
            });*/
		}

		public void SetAStarNode(List<GridLocation> viewable, List<GridLocation> used, GridLocation nextNode, Vector2 nextParent, float d, Vector2 target, float DISTMULT)
		{
			float f = d;
			float addedDist = (nextNode.cost * DISTMULT);




			//Add item
			if (!nextNode.isViewable && !nextNode.hasBeenUsed)
			{
				//viewable.Add(new AStarNode(nextParent, f, new Vector2(nextNode.Pos.X, nextNode.Pos.Y), nextNode.CurrentDist + 1, nextNode.Cost, nextNode.Impassable));

				nextNode.SetNode(nextParent, f, d + addedDist);
				nextNode.isViewable = true;

				SetAStarNodeInsert(viewable, nextNode);
			}
			//Node is in viewable, so check if Fscore needs revised
			else if (nextNode.isViewable)
			{

				if (f < nextNode.fScore)
				{
					nextNode.SetNode(nextParent, f, d + addedDist);
				}
			}
		}

		public virtual void SetAStarNodeInsert(List<GridLocation> LIST, GridLocation NEWNODE)
		{
			bool added = false;
			for (int i = 0; i < LIST.Count; i++)
			{
				if (LIST[i].fScore > NEWNODE.fScore)
				{
					//Cant insert at 0, because that would take up the looking at node...
					LIST.Insert(Math.Max(1, i), NEWNODE);
					added = true;
					break;
				}
			}

			if (!added)
			{
				LIST.Add(NEWNODE);
			}
		}

		#endregion


		public Vector2? FindDoorLocation()
		{
			for (int x = 0; x < slots.Count; x++)
			{
				for (int y = 0; y < slots[x].Count; y++)
				{
					if (slots[x][y].door)
					{
						return new Vector2(x, y);
					}
				}
			}
			return null;
		}



		public virtual void DrawGrid(Vector2 OFFSET)
		{
			if (showGrid)
			{
				//Vector2 topLeft = GetSlotFromPixel((new Vector2(0, 0)) / Globals.zoom  - OFFSET, Vector2.Zero);
				//Vector2 botRight = GetSlotFromPixel((new Vector2(Globals.screenWidth, Globals.screenHeight)) / Globals.zoom  - OFFSET, Vector2.Zero);
				Vector2 topLeft = GetSlotFromPixel(new Vector2(0, 0), Vector2.Zero);
				Vector2 botRight = GetSlotFromPixel(new Vector2(Globals.screenWidth, Globals.screenHeight), Vector2.Zero);

				for (int j = (int)topLeft.X; j <= botRight.X && j < slots.Count; j++)
				{
					for (int k = (int)topLeft.Y; k <= botRight.Y && k < slots[0].Count; k++)
					{
						Vector2 position = OFFSET + physicalStartPos + new Vector2(j * slotDims.X, k * slotDims.Y);
						Color color = (currentHoverSlot.X == j && currentHoverSlot.Y == k) ? Color.Red : (slots[j][k].door) ? Color.Blue : (slots[j][k].filled) ? Color.Orange : Color.White;
						gridImg.Draw(position, color);
					}
				}
			}

			for(int i = 0; i < gridItems.Count; i++)
			{
				gridItems[i].Draw(OFFSET);
			}
		}
	}
}
