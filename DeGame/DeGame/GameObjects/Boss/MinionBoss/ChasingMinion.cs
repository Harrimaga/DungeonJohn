using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ChasingMinion : Enemy
{
    public ChasingMinion(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/ChasingMinion", Difficulty, layer, id)
    {
        position = startPosition;
        basevelocity = new Vector2(1.2f, 1.2f);
        health = 30 * statmultiplier;
        maxhealth = 30 * statmultiplier;
        attack = 5 * statmultiplier;
        drop = false;
        flying = true;
        bossenemy = true;
        expGive = 0;
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
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/ChasingMinion"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


