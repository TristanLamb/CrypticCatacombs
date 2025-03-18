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
    public class Unit : AttackableObject
    {
        public Skill currentSkill;
        public List<Skill> skills = new List<Skill>();

		public Unit(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {

        }

        public virtual void Update(Vector2 OFFSET, Player ENEMY)
        {

            base.Update(OFFSET, ENEMY);
        }

		public override void Draw(Vector2 OFFSET, SpriteEffects spriteEffects)
        {
            base.Draw(OFFSET, spriteEffects);
        }
    }
}
