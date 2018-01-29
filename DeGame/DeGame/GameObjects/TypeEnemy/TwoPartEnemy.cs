using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TwoPartEnemy : Enemy
{
    int Stage = 1;
    public TwoPartEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/2PartEnemyFull", Difficulty, layer, id)
    {
        basevelocity = new Vector2(0.08f, 0.08f);
        health = 100 * statmultiplier;
        maxhealth = 100 * statmultiplier;
        expGive = 120 * statmultiplier;
        contactdamage = 10 * statmultiplier;
        killable = false;
        hpdisplay = true;
    }

    public override void Update(GameTime gameTime)
    {
        if (CollidesWith(PlayingState.player))
        {
            velocity = Vector2.Zero;
            counter--;
            if (counter == 0)
            {

                PlayingState.player.health -= 10;
                counter = 100;
            }
        }

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
                maxhealth = 75;
                health = 75;
                Stage = 2;
                TwoPartEnemy TwoPartEnemy = new TwoPartEnemy(position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyFull").Height * 0.8f), Roomposition, PlayingState.currentFloor.displayint);
                //PlayingState.currentFloor.currentRoom.addedenemies.Add(TwoPartEnemy);
                //PlayingState.currentFloor.currentRoom.addedenemies.Add(TwoPartEnemy);
            }
        }
        if (Stage == 2)
        {
            killable = true;
            basevelocity = new Vector2(0.03f, 0.03f);
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