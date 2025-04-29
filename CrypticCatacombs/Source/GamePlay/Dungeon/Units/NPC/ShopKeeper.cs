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
    public class ShopKeeper : Unit
	{
		private Vector2 position;
		public string name;
		private Texture2D idleTexture;


		private SpriteEffects spriteEffect = SpriteEffects.None;
        public ShopKeeper(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
			speed = 0.0f;

			name = "ShopKeeper";

			health = 500;
			healthMax = health;
			
			mana = 100;
			manaMax = mana;

			idleTexture = Globals.content.Load<Texture2D>("2d/Units//NPC/ShopKeeperIdle");

			frameAnimations = true;
            currentAnimation = 0;
			frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 6, 198, 0, "Idle"));

            skills.Add(new FlameWall(this));
			skills.Add(new IceShards(this));
			skills.Add(new Teleport(this));
		}

        public override void Update(Vector2 OFFSET)
        {
			if (myModel != idleTexture)
			{
				myModel = idleTexture;
				SetAnimationByName("Idle");
			}


			base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET, spriteEffect);
        }
    }
}
