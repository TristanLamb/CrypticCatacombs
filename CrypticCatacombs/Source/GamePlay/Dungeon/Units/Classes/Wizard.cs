using System;
using System.Collections.Generic;
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
    public class Wizard : Unit
	{
		private SpriteEffects spriteEffect = SpriteEffects.None;
        public Wizard(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
            speed = 2.0f;

			health = 5;
			healthMax = health;

            frameAnimations = true;
            currentAnimation = 0;
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames,new Vector2(0, 0), 6, 66, 0, "Walk"));
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 66, 0, "Idle"));

            skills.Add(new FlameWall(this));
			skills.Add(new IceShards(this));
			skills.Add(new Teleport(this));
		}

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            bool checkScroll = false;
			if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyByName("Move Left")))
            {
				pos = new Vector2(pos.X - speed, pos.Y);
				spriteEffect = SpriteEffects.FlipHorizontally;
				checkScroll = true;
			}

            if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyByName("Move Right")))
            {
                pos = new Vector2(pos.X + speed, pos.Y);
				spriteEffect = SpriteEffects.None;
				checkScroll = true;
			}

            if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyByName("Move Up")))
            {
                pos = new Vector2(pos.X, pos.Y - speed);
				checkScroll = true;
			}

            if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyByName("Move Down")))
            {
                pos = new Vector2(pos.X, pos.Y + speed);
				checkScroll = true;
			}

			if (Globals.keyboard.GetSinglePress("D1"))
			{
				currentSkill = skills[0];
				currentSkill.Active = true;
			}
			if (Globals.keyboard.GetSinglePress("D2"))
			{
				currentSkill = skills[1];
				currentSkill.Active = true;
			}
			if (Globals.keyboard.GetSinglePress("D3"))
			{
				currentSkill = skills[2];
				currentSkill.Active = true;
			}

			if (checkScroll)
			{
				GameGlobals.CheckScroll(pos);

				SetAnimationByName("Walk");
			}
			else
			{
				SetAnimationByName("Idle");
			}

            //rotation = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET); //to add rotation to follow mouse

			if (currentSkill == null)
            {
                if (Globals.mouse.LeftClick() || Globals.keyboard.GetPress("Space"))
                {
                    GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
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

            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
