using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class TwoPartEnemy : Enemy
{
    int Stage = 1, NextStage;
    static Random random = new Random();
    bool Two = true;

    public TwoPartEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/2PartEnemyFull", Difficulty, layer, id)
    {
        basevelocity = new Vector2(0.12f, 0.12f);
        health = 100 * statmultiplier;
        maxhealth = 100 * statmultiplier;
        expGive = 0 * statmultiplier;
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

        base.Update(gameTime);

        if (Stage == 1)
        {
            if (PlayingState.currentFloor.currentRoom.position == Roomposition)
                Chase();

            if (health <= 0)
            {
                Stage = 2;
            }
        }
        //else if (Stage == 2)
        //{
        //    killable = true;
        //    maxhealth = 75;
        //    health = 75;
        //    expGive = 80 * statmultiplier;
        //    basevelocity = new Vector2(0.03f, 0.03f);            
        //}
        else if (Stage == 2)
        {
            if (Two == true)
            {
                killable = true;
                HalfEnemy HalfEnemyLeft = new HalfEnemy(position + new Vector2(-50, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyLeft").Height * 0.8f), Roomposition, 1, PlayingState.currentFloor.displayint);
                HalfEnemy HalfEnemyRight = new HalfEnemy(position + new Vector2(50, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyRight").Height * 0.8f), Roomposition, 2, PlayingState.currentFloor.displayint);
                PlayingState.currentFloor.currentRoom.addedenemies.Add(HalfEnemyLeft);
                PlayingState.currentFloor.currentRoom.addedenemies.Add(HalfEnemyRight);
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemycounter += 2;
                Two = false;
            }
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
        //if (Stage == 2)
        //{
        //    spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyBottom"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
        //}
    }
}