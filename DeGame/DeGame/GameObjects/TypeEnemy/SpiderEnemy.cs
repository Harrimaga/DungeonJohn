using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class SpiderEnemy : Enemy
{
    int ChargeCounter = 0;
    int ChargeDirection;
    Random random = new Random();
    Vector2 direction;

    public SpiderEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        basevelocity = new Vector2(5f, 5f);
        direction = (PlayingState.player.position - position);
        range = 500;
    }

    public override void Update(GameTime gameTime)
    {
        ChargeCounter++;
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            counter--;
            if (counter == 0)
            {
                // velocity = Vector2.Zero;
                PlayingState.player.health -= 10;
                counter = 100;
            }
        }

        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }

        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
        {
            if (ChargeCounter <= 50)
            {
                Charge();
            }

            if (ChargeCounter == 50)
            {
                if (PlayingState.player.position.X + range < position.X || PlayingState.player.position.X - range > position.X ||
                PlayingState.player.position.Y + range < position.Y || PlayingState.player.position.Y - range > position.Y)
                {
                    ChargeDirection = random.Next(8);
                    if (ChargeDirection == 0)
                    {
                        direction = new Vector2(0, -1);
                    }
                    if (ChargeDirection == 1)
                    {
                        direction = new Vector2(1, -1);
                    }
                    if (ChargeDirection == 2)
                    {
                        direction = new Vector2(1, 0);
                    }
                    if (ChargeDirection == 3)
                    {
                        direction = new Vector2(1, 1);
                    }
                    if (ChargeDirection == 4)
                    {
                        direction = new Vector2(0, 1);
                    }
                    if (ChargeDirection == 5)
                    {
                        direction = new Vector2(-1, 1);
                    }
                    if (ChargeDirection == 6)
                    {
                        direction = new Vector2(-1, 0);
                    }
                    if (ChargeDirection == 6)
                    {
                        direction = new Vector2(-1, -1);
                    }
                }
                else
                {
                    direction = (PlayingState.player.position - position);
                }
            }
            if (ChargeCounter >= 100)
            {
                ChargeCounter = 0;
            }
        }

        
    }

    public void Charge()
    {
        Position = position;
        direction.Normalize();
        position += direction * ChargeSpeed;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/SpiderEnemy"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


