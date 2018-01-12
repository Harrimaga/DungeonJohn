using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ChasingMinion : Enemy
{
    public ChasingMinion(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/ChasingMinion", layer, id)
    {
        position = startPosition;
        basevelocity = new Vector2(1.0f, 0.8f);
        health = 50;
        maxhealth = 30;
        expGive = 10;
        attack = 1;
        drop = false;
        flying = true;
        bossenemy = true;
    }

    public override void Update(GameTime gameTime)
    {
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

        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Chase();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MiniChase"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


