using Cryptic_Catacombs;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class RangePlayer : Player
{
    public RangePlayer()
    {
        playerSpeed = 175f;
        health = 125;
    }

    public override void LoadContent(ContentManager content)
    {
        playerIdleTexture = content.Load<Texture2D>("Archer-Idle");
        playerWalkingTexture = content.Load<Texture2D>("Archer-Walk");
    }
}