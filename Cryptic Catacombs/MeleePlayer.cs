using Cryptic_Catacombs;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class MeleePlayer : Player
{
    public MeleePlayer()
    {
        playerSpeed = 150f;
        health = 150;
    }

    public override void LoadContent(ContentManager content)
    {
        playerIdleTexture = content.Load<Texture2D>("swordsmanIdle");
        playerWalkingTexture = content.Load<Texture2D>("swordsmanWalking");
    }
}