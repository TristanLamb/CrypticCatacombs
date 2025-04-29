using System;
using System.Collections.Generic;
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
    public class Wizard : Unit
	{
		private Vector2 position;
		public string name;
		private Texture2D idleTexture;
		private Texture2D walkTexture;
		public bool purchasedTeleportSkill;


		private SpriteEffects spriteEffect = SpriteEffects.None;
        public Wizard(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
			speed = 2.0f;

			name = "Wizard";

			health = 5;
			healthMax = health;
			
			mana = 100;
			manaMax = mana;
			purchasedTeleportSkill = false;

			idleTexture = Globals.content.Load<Texture2D>("2d/Units/WizardIdleSheet");
			walkTexture = Globals.content.Load<Texture2D>("2d/Units/WizardWalkSheet");

			frameAnimations = true;
            currentAnimation = 0;
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 6, 66, 0, "Walk"));
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 6, 66, 0, "Idle"));

            skills.Add(new FlameWall(this));
			skills.Add(new IceShards(this));
			skills.Add(new Teleport(this));
		}

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
            bool checkScroll = false;
			Vector2 newPos = pos;

			if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyByName("Move Left")))
			{
				newPos.X -= speed;
				if (!GameGlobals.map.CheckCollision(newPos))
				{
					pos = newPos;
					spriteEffect = SpriteEffects.FlipHorizontally;
					checkScroll = true;
				}
			}
			if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyByName("Move Right")))
			{
				newPos.X += speed;
				if (!GameGlobals.map.CheckCollision(newPos))
				{
					pos = newPos;
					spriteEffect = SpriteEffects.None;
					checkScroll = true;
				}
			}
			if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyByName("Move Up")))
			{
				newPos.Y -= speed;
				if (!GameGlobals.map.CheckCollision(newPos))
				{
					pos = newPos;
					checkScroll = true;
				}
			}
			if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyByName("Move Down")))
			{
				newPos.Y += speed;
				if (!GameGlobals.map.CheckCollision(newPos))
				{
					pos = newPos;
					checkScroll = true;
				}
			}


			if (Globals.keyboard.GetSinglePress("D1")) //fire wall
			{
				if (mana >= 10)
				{
					currentSkill = skills[0];
					currentSkill.Active = true;
					mana -= 10;
				}
				
			}
			if (Globals.keyboard.GetSinglePress("D2")) //ice shards
			{
				if (mana >= 10)
				{
					currentSkill = skills[1];
					currentSkill.Active = true;
					mana -= 10;
				}
			}
			if (Globals.keyboard.GetSinglePress("D3")) //teleport
			{
				if (purchasedTeleportSkill && mana >= 15)
				{
					currentSkill = skills[2];
					currentSkill.Active = true;
					mana -= 15;
				}
				
			}

			if (checkScroll)
			{
				GameGlobals.CheckScroll(pos);

				if (myModel != walkTexture)
				{
					myModel = walkTexture;
					SetAnimationByName("Walk");
				}
			}
			else
			{
				if (myModel != idleTexture)
				{
					myModel = idleTexture;
					SetAnimationByName("Idle");
				}
			}

			//rotation = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET); //to add rotation to follow mouse

			if (currentSkill == null)
            {
				if (!(GameGlobals.currentMapLayout == 9))
				{
					if (Globals.mouse.LeftClick() || Globals.keyboard.GetSinglePress("Space"))
					{
						GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
					}
				}
				
            }
            else
            {
                currentSkill.Update(OFFSET, ENEMY);

                if(currentSkill.done)
                {
					currentSkill.Reset();
                    currentSkill = null;
				}
            }

            if(Globals.mouse.RightClick())
            {
                if(currentSkill != null)
                {
                    currentSkill.targetEffect.done = true;
					currentSkill.Reset();
					currentSkill = null;
				}
			}

			System.Diagnostics.Debug.WriteLine("Cursor Position: " + new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));


			base.Update(OFFSET, ENEMY, GRID);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
