using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ChasingEnemy : Enemy
{
    public ChasingEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/BearEnemyPixel", layer, id)
    {
        basevelocity = new Vector2(0.9f, 0.9f);
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
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/BearEnemyPixel"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


   