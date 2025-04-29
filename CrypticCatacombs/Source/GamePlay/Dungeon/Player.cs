﻿using System;
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
    public class Player
    {

		public bool defeated;
        public int id, gold;
        public Wizard wizard;
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public Player(int ID)
        {
            id = ID;
			gold = 0;
			defeated = false;
        }

        public virtual void Update(Player ENEMY, Vector2 OFFSET, SquareGrid GRID)
        {
            if(wizard != null)
            {
				wizard.Update(OFFSET, ENEMY, GRID);
			}

			for (int i = 0; i < spawnPoints.Count; i++)
			{
				spawnPoints[i].Update(OFFSET, ENEMY, GRID);

			}


			for (int i = 0; i < units.Count; i++)
			{
				units[i].Update(OFFSET, ENEMY, GRID);

				if (units[i].dead)
				{
                    ChangeScore(0.5f);
					units.RemoveAt(i);
					i--;
				}
			}

			CheckIfDefeated();
		}

        public virtual void AddUnit(object INFO)
        {
			//error checking
			Unit tempUnit = (Unit)INFO;
			tempUnit.ownerId = id; 

            units.Add((Unit)INFO);
        }
		public virtual void AddSpawnPoint(object INFO)
		{
			SpawnPoint tempSpawnPoint = (SpawnPoint)INFO;
			tempSpawnPoint.ownerId = id;
			spawnPoints.Add(tempSpawnPoint);
		}


		public virtual void ChangeScore(float SCORE)
        {

        }



		public virtual List<AttackableObject> GetAllObjects()
		{
			List<AttackableObject> tempObjects = new List<AttackableObject>();
			tempObjects.AddRange(units.ToList<AttackableObject>());
			tempObjects.AddRange(spawnPoints.ToList<AttackableObject>());

			if (wizard != null)
			{
				tempObjects.Add(wizard);
			}

			return tempObjects;
		}

		public virtual void CheckIfDefeated()
		{

		}



		public virtual void Draw(Vector2 OFFSET)
        {
            if (wizard != null)
            {
				wizard.Draw(OFFSET);
            }

			for (int i = 0; i < units.Count; i++)
			{
				units[i].Draw(OFFSET);

			}

			for (int i = 0; i < spawnPoints.Count; i++)
			{
				spawnPoints[i].Draw(OFFSET);

			}

		}
    }
}
