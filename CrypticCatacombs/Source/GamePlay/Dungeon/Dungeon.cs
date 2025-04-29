using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrypticCatacombs
{
    public class Dungeon
    {
        public Vector2 offset;

		public CharacterMenu characterMenu;

        public UI ui;

        public User user;
        public AIPlayer aIPlayer;

		private ShopKeeper shopKeeper;

		public SquareGrid grid;

        //public Map map;

        public List<Projectile2d> projectiles = new List<Projectile2d>();
		public List<Effect2d> effects = new List<Effect2d>();
		public List<AttackableObject> allObjects = new List<AttackableObject>();
		PassObject ResetDungeon, ChangeGameState;
		private CustomTimer nextRoomTimer = new CustomTimer(1000);

		private MapLayouts mapLayouts;

		bool shopGenerated = false;
		ShopHeart shopHeart;
		TeleportSkill teleportSkill;
		ManaPotion manaPotion;
		HealthPotion healthPotion;



		public Dungeon(PassObject RESETDUNGEON, MapLayouts MAPLAYOUTS, int layoutIndex, PassObject CHANGEGAMESTATE, User user)
        {
			ResetDungeon = RESETDUNGEON;
			this.mapLayouts = MAPLAYOUTS;
			ChangeGameState = CHANGEGAMESTATE;


			GameGlobals.PassProjectile = AddProjectile;
			GameGlobals.PassEffect = AddEffect;
			GameGlobals.PassMob = AddMob;
            GameGlobals.PassSpawnPoint = AddSpawnPoint;
            GameGlobals.CheckScroll = CheckScroll;
			GameGlobals.PassGold = AddGold;
			GameGlobals.paused = false;

			GameGlobals.map = new Map(mapLayouts.GetLayout(layoutIndex), 31);

			this.user = user;
			
            aIPlayer = new AIPlayer(2);

			offset = new Vector2(0, 0);

			//characterMenu = new Menu2d(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(350, 500), null);
			characterMenu = new CharacterMenu(user.wizard);

			//grid = new SquareGrid(new Vector2(35, 35), new Vector2(-100, -100), new Vector2(Globals.screenWidth + 200, Globals.screenHeight + 200));
			grid = new SquareGrid(new Vector2(31, 31), new Vector2(0, 0), new Vector2(Globals.screenWidth, Globals.screenHeight));

			ui = new UI();

			
			int[,] mapData = mapLayouts.GetLayout(layoutIndex);
			for (int i = 0; i < mapData.GetLength(0); i++)
			{
				for (int j = 0; j < mapData.GetLength(1); j++)
				{
					if (mapData[i, j] == 1
						|| mapData[i, j] == 8
                        || mapData[i, j] == 9
                        || mapData[i, j] == 10
                        || mapData[i, j] == 11
						|| mapData[i, j] == 12
						|| mapData[i, j] == 13
						|| mapData[i, j] == 14
						|| mapData[i, j] == 15
						|| mapData[i, j] == 16
						|| mapData[i, j] == 17
						|| mapData[i, j] == 18)
					{
						Vector2 gridPos = new Vector2(j, i);
						GridLocation loc = grid.GetSlotFromLocation(gridPos);
						if (loc != null)
						{
							loc.SetToFilled(true);       // now filled
							loc.impassable = true;       // now pathfinding won't go through
						}
						else
						{
							System.Diagnostics.Debug.WriteLine($"No location found for {gridPos}");
						}
					}
					if (mapData[i, j] == 2)
					{
						Vector2 gridPos = new Vector2(j, i);
						GridLocation loc = grid.GetSlotFromLocation(gridPos);
						if (loc != null)
						{
							loc.SetToDoor(true);       // now filled
							loc.impassable = true;       // now pathfinding won't go through
						}
						else
						{
							System.Diagnostics.Debug.WriteLine($"No location found for {gridPos}");
						}
					}
				}
			}


			if (GameGlobals.currentMapLayout == 9)
			{
				shopKeeper = new ShopKeeper("2d/Units/NPC/ShopKeeperIdle", new Vector2(450, 305), new Vector2(212, 212), new Vector2(6, 1), 9);

				shopHeart = new ShopHeart("2d/Misc/Heart", new Vector2(375, 356), ui);

				teleportSkill = new TeleportSkill("2d/Misc/TeleportPng", new Vector2(435, 343));

				manaPotion = new ManaPotion("2d/Misc/ManaPotion", new Vector2(495, 356));
				
				healthPotion = new HealthPotion("2d/Misc/HealthPotion", new Vector2(555, 356));

			}

		}

		public void LoadContent(ContentManager content)
		{
			GameGlobals.map.LoadContent(content);
		}


		public virtual void Update()
        {
			//System.Diagnostics.Debug.WriteLine($"Rooms Completed: {GameGlobals.roomsCompleted}");
			//System.Diagnostics.Debug.WriteLine($"Current MapLayout: {GameGlobals.currentMapLayout}");
			if (!DontUpdate())
            {
				allObjects.Clear();
				allObjects.AddRange(user.GetAllObjects());
				allObjects.AddRange(aIPlayer.GetAllObjects());

				user.Update(aIPlayer, offset, grid);
                aIPlayer.Update(user, offset, grid);



                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(offset, allObjects);

                    //removing a projectile once it hit something or expired
                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }

				for (int i = 0; i < effects.Count; i++)
				{
					effects[i].Update(offset);
					if (effects[i].done)
					{
						effects.RemoveAt(i);
						i--;
					}
				}
			}
            else
            {
                if(Globals.keyboard.GetPress("Enter") && user.wizard.dead)
                {
					GameGlobals.roomsCompleted = 0;
					ResetDungeon(null);
					ChangeGameState(0);
				}
            }

			if(grid != null)
			{
				grid.Update(offset);
			}

			if (Globals.keyboard.GetSinglePress("Escape") && !user.wizard.dead)
			{
				GameGlobals.paused = !GameGlobals.paused;
			}

			characterMenu.Update();

			if(Globals.keyboard.GetSinglePress("C"))
			{
				characterMenu.Active = true;
			}
			if (Globals.keyboard.GetSinglePress("G"))
			{
				grid.showGrid = !grid.showGrid;
			}

			if (aIPlayer.defeated)
			{
				grid.LoadDoor();
				GameGlobals.enemiesDefeated = true;
				if (GameGlobals.nextRoom == true)
				{
					//Globals.msgList.Add(new DismissableMessage(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(150, 130), "You Win!", Color.Black, true, WinConfirm));
					WinConfirm(null);
					GameGlobals.nextRoom = false;
					nextRoomTimer.AddToTimer(4000);
				}
				//WinConfirm(null);


			}
			if(GameGlobals.currentMapLayout == 9)
			{
				if (shopGenerated == true)
				{
					shopKeeper.Update(offset);
					shopHeart.Update(Vector2.Zero);
					teleportSkill.Update(Vector2.Zero);
					manaPotion.Update(Vector2.Zero);
					healthPotion.Update(Vector2.Zero);
				}
				else
				{
					GenerateShop();
				}
				
			}
			

			ui.Update(this);
		}

		public virtual void GenerateShop()
		{
			shopKeeper = new ShopKeeper("2d/Units/NPC/ShopKeeperIdle", new Vector2(450, 305), new Vector2(212, 212), new Vector2(6, 1), 9);

			shopHeart = new ShopHeart("2d/Misc/Heart", new Vector2(375, 356), ui);

			teleportSkill = new TeleportSkill("2d/Misc/TeleportPng", new Vector2(435, 343));

			manaPotion = new ManaPotion("2d/Misc/ManaPotion", new Vector2(495, 356));

			healthPotion = new HealthPotion("2d/Misc/HealthPotion", new Vector2(555, 356));

			shopGenerated = true;
		}


		public virtual void AddGold(object INFO)
		{
			PlayerValuePacket packet = (PlayerValuePacket)INFO;

			if (user.id == packet.playerId)
			{
				user.gold += (int)packet.value;
				GameGlobals.user.gold = user.gold;
			}
			else if (aIPlayer.id == packet.playerId)
			{
				aIPlayer.gold += (int)packet.value;
			}
		}

		public virtual void AddMob(object INFO)
        {
            Unit tempUnit = (Unit)INFO;

            if(user.id == tempUnit.ownerId)
            {
                user.AddUnit(tempUnit);
            }
            else if(aIPlayer.id == tempUnit.ownerId)
            {
                aIPlayer.AddUnit(tempUnit);
            }

            aIPlayer.AddUnit((Mob)INFO);
        }


        //only pass in projectiles
        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2d)INFO);
        }

		public virtual void AddEffect(object INFO)
		{
			effects.Add((Effect2d)INFO);
		}

		public virtual void AddSpawnPoint(object INFO)
		{
			SpawnPoint tempSpawnPoint = (SpawnPoint)INFO;

			if (user.id == tempSpawnPoint.ownerId)
			{
				user.AddSpawnPoint(tempSpawnPoint);
			}
			else if (aIPlayer.id == tempSpawnPoint.ownerId)
			{
				aIPlayer.AddSpawnPoint(tempSpawnPoint);
			}

			aIPlayer.AddUnit((Mob)INFO);
		}

		public virtual void CheckScroll(object INFO)
        {
            Vector2 tempPos = (Vector2)INFO;
            bool screenScrollingEnabled = false; //Enable or Disable screenscrolling

            if(screenScrollingEnabled == true)
            {
				if (tempPos.X < -offset.X + (Globals.screenWidth * .4f))
				{
					offset = new Vector2(offset.X + user.wizard.speed * 2, offset.Y);
				}
				if (tempPos.X > -offset.X + (Globals.screenWidth * .6f))
				{
					offset = new Vector2(offset.X - user.wizard.speed * 2, offset.Y);
				}
				if (tempPos.Y < -offset.Y + (Globals.screenHeight * .4f))
				{
					offset = new Vector2(offset.X, offset.Y + user.wizard.speed * 2);
				}
				if (tempPos.Y > -offset.Y + (Globals.screenHeight * .6f))
				{
					offset = new Vector2(offset.X, offset.Y - user.wizard.speed * 2);
				}
			}
            
		}

		public virtual bool DontUpdate()
		{
			if(user.wizard.dead || GameGlobals.paused || characterMenu.Active)
			{
				return true;
			}	
			return false;
		}


		public virtual void WinConfirm(object INFO)
		{
			GameGlobals.roomsCompleted++;
			ResetDungeon(null);
			ChangeGameState(2);
		}

        public virtual void Draw(Vector2 OFFSET, SpriteBatch spriteBatch)
        {
			
			GameGlobals.map.Draw(spriteBatch);
			grid.DrawGrid(offset);

			user.Draw(offset);
            aIPlayer.Draw(offset);

			for (int i = 0; i < projectiles.Count; i++)
			{
				projectiles[i].Draw(offset);
			}

			for (int i = 0; i < effects.Count; i++)
			{
				effects[i].Draw(offset);
			}

			if (GameGlobals.currentMapLayout == 9)
			{
				shopKeeper.Draw(offset);
				shopHeart.Draw(Vector2.Zero);
				teleportSkill.Draw(Vector2.Zero);
				manaPotion.Draw(Vector2.Zero);
				healthPotion.Draw(Vector2.Zero);
			}

			ui.Draw(this);

			characterMenu.Draw();
		}


	}
}
