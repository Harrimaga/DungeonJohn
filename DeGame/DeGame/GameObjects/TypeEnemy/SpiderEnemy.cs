using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class SpiderEnemy : Enemy
{
    int ChargeCounter = 0;
    int ChargeDirection;
    Random random = new Random();
    public float ChargeSpeed = 2f;

    public SpiderEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/SpiderEnemy", layer, id)
    {
        velocity = new Vector2(3f, 3f);
        direction = (PlayingState.player.position - position);
        range = 100;
        moving = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        ChargeCounter++;

        if (CollidesWith(PlayingState.player))
        {
            counter--;
            if (counter == 0)
            {
                PlayingState.player.health -= 10;
                counter = 100;
            }
        }
        Charge();
    }

    void Charge()
    {
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
        {
            if (ChargeCounter <= 20)
            {
                direction.Normalize();
                moving = true;
            }

            if (ChargeCounter >= 20)
            {
                moving = false;
                Rectangle attackrange = new Rectangle((int)position.X - (int)range, (int)position.Y - (int)range, BoundingBox.Width + 2 * (int)range, BoundingBox.Height + 2 * (int)range);
                if (attackrange.Intersects(PlayingState.player.BoundingBox))
                {
                    direction = (PlayingState.player.position - position);
                }
                else
                {
                    ChargeDirection = random.Next(8);
                    switch (ChargeDirection)
                    {
                        case 0:
                            direction = new Vector2(0, -1);
                            break;
                        case 1:
                            direction = new Vector2(1, -1);
                            break;
                        case 2:
                            direction = new Vector2(1, 0);
                            break;
                        case 3:
                            direction = new Vector2(1, 1);
                            break;
                        case 4:
                            direction = new Vector2(0, 1);
                            break;
                        case 5:
                            direction = new Vector2(-1, 1);
                            break;
                        case 6:
                            direction = new Vector2(-1, 0);
                            break;
                        case 7:
                            direction = new Vector2(-1, -1);
                            break;
                    }
                }
            }
            if (ChargeCounter >= 70)
            {
                ChargeCounter = 0;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/SpiderEnemy"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


