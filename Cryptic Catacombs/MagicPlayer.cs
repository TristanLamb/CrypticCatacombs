using Cryptic_Catacombs;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class MagicPlayer : Player
{
    public MagicPlayer()
    {
        playerSpeed = 130f;
        health = 110;
    }

    public override void LoadContent(ContentManager content)
    {
        playerIdleTexture = content.Load<Texture2D>("Wizard-Idle");
        playerWalkingTexture = content.Load<Texture2D>("Wizard-Walk");
    }
}