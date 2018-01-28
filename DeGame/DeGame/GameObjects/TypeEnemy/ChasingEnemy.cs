using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ChasingEnemy : Enemy
{
    public ChasingEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/BearEnemyPixel", Difficulty, layer, id)
    {
        basevelocity = new Vector2(0.08f, 0.08f);
        health = 120 * statmultiplier;
        maxhealth = 120 * statmultiplier;
        expGive = 120 * statmultiplier;
        contactdamage = 10 * statmultiplier;
    }

    public override void Update(GameTime gameTime)
    {
        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }

        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Chase();
        base.Update(gameTime);
        CheckAlive();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/BearEnemyPixel"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


   