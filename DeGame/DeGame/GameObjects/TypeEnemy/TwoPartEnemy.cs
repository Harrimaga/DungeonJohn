using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class TwoPartEnemy : Enemy
{
    int Stage = 1;
    bool Start2 = false;
    public TwoPartEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/2PartEnemyFull", Difficulty, layer, id)
    {
        basevelocity = new Vector2(0.12f, 0.12f);
        health = 100 * statmultiplier;
        maxhealth = 100 * statmultiplier;
        expGive = 80 * statmultiplier;
        contactdamage = 10 * statmultiplier;
        killable = false;
        hpdisplay = true;
        contactdamage = 10 * statmultiplier;
    }

    /// <summary>
    /// If it isnt colliding with player it is allowed to move; If in the current room and Stage 1 Chase, depending on which stage handle the right variables and methods
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }

        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Chase();
        base.Update(gameTime);

        if (Stage == 1)
        {
            if (health <= 0)
            {
                Stage = 2;
                Start2 = true;
            }
        }
        if (Stage == 2 && Start2)
        {
            Start2 = false;
            maxhealth = 75 * statmultiplier;
            health = 75 * statmultiplier;
            basevelocity = new Vector2(0.03f, 0.03f);
            killable = true;
        }
    }

    /// <summary>
    /// Draw the enemy based upon if it is still whole or not.
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (Stage == 1)
        {
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyFull"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);     
        }
        if(Stage == 2)
        {
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyBottom"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
        }
    }
}