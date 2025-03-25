using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
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

        //public Map map;

        public List<Projectile2d> projectiles = new List<Projectile2d>();
		public List<Effect2d> effects = new List<Effect2d>();
		public List<AttackableObject> allObjects = new List<AttackableObject>();
		PassObject ResetDungeon, ChangeGameState;

		private MapLayouts mapLayouts;


		public Dungeon(PassObject RESETDUNGEON, MapLayouts MAPLAYOUTS, int layoutIndex, PassObject CHANGEGAMESTATE)
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

			user = new User(1);
            aIPlayer = new AIPlayer(2);

			offset = new Vector2(0, 0);

			//characterMenu = new Menu2d(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(350, 500), null);
			characterMenu = new CharacterMenu(user.wizard);

            ui = new UI();

			GameGlobals.map = new Map(mapLayouts.GetLayout(layoutIndex), 31);
		}

		public void LoadContent(ContentManager content)
		{
			GameGlobals.map.LoadContent(content);
		}


		public virtual void Update()
        {

			if (!DontUpdate())
            {
				allObjects.Clear();
				allObjects.AddRange(user.GetAllObjects());
				allObjects.AddRange(aIPlayer.GetAllObjects());

				user.Update(aIPlayer, offset);
                aIPlayer.Update(user, offset);



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
                    ResetDungeon(null);
                }
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

			if (aIPlayer.defeated)
			{
				Globals.msgList.Add(new DismissableMessage(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(150, 130), "You Win!", Color.Black, true, WinConfirm));
				//WinConfirm(null);
			}

			ui.Update(this);
		}

		public virtual void AddGold(object INFO)
		{
			PlayerValuePacket packet = (PlayerValuePacket)INFO;

			if (user.id == packet.playerId)
			{
				user.gold += (int)packet.value;
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
			ResetDungeon(null);
			ChangeGameState(0);
		}

        public virtual void Draw(Vector2 OFFSET, SpriteBatch spriteBatch)
        {
			GameGlobals.map.Draw(spriteBatch);

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

			ui.Draw(this);

			characterMenu.Draw();
		}


	}
}
