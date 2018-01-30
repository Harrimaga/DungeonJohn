using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class TwoPartEnemy : Enemy
{
    int Stage = 1, NextStage;
    static Random random = new Random();

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
                NextStage = random.Next(2);
                switch (NextStage)
                {
                    case 0:
                        Stage = 2;
                        break;
                    case 1:
                        Stage = 3;
                        break;
                }
            }
        }

        if (Stage == 2)
        {
            maxhealth = 75;
            health = 75;
            killable = true;
            expGive = 80 * statmultiplier;
            basevelocity = new Vector2(0.03f, 0.03f);
        }

        if (Stage == 3)
        {
            killable = true;
            HalfEnemy HalfEnemyLeft = new HalfEnemy(position + new Vector2(-50, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyLeft").Height * 0.8f), Roomposition, 1, PlayingState.currentFloor.displayint);
            HalfEnemy HalfEnemyRight = new HalfEnemy(position + new Vector2(50, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyRight").Height * 0.8f), Roomposition, 2, PlayingState.currentFloor.displayint);
            PlayingState.currentFloor.currentRoom.addedenemies.Add(HalfEnemyLeft);
            PlayingState.currentFloor.currentRoom.addedenemies.Add(HalfEnemyRight);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (Stage == 1)
        {
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyFull"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);     
        }
        if (Stage == 2)
        {
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyBottom"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
        }
    }
}