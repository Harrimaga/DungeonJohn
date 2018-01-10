using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SpoederEnemy : Enemy
{
    int ChaseCounter = 0;

    public SpoederEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {

    }

    public override void Update(GameTime gameTime)
    {
        ChaseCounter++;
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            counter--;
            if (counter == 0)
            {
                // velocity = Vector2.Zero;
                PlayingState.player.health -= 0;
                counter = 100;
            }
        }

        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }

        if (PlayingState.currentFloor.currentRoom.position == Roomposition && ChaseCounter >= 25)
        {
            Chase();
            velocity = basevelocity;
        }

        if (ChaseCounter >= 75)
        {
            ChaseCounter = 0;
        }
    }

    public void SpoederChace()
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/SpoederEnemy"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


