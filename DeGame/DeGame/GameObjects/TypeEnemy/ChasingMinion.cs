using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ChasingMinion : Enemy
{
    public ChasingMinion(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/ChasingMinion", Difficulty, layer, id)
    {
        position = startPosition;
        basevelocity = new Vector2(0.08f, 0.08f);
        health = 30 * statmultiplier;
        maxhealth = 30 * statmultiplier;
        drop = false;
        flying = true;
        bossenemy = true;
        expGive = 0;
        contactdamage = 10 * statmultiplier;
    }

    /// <summary>
    /// If it doesnt collide with player it can move; If it is in the currently used room it wil execute chase.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        velocity = basevelocity;
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Chase();
    }

    /// <summary>
    /// Draws the enemy.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/ChasingMinion"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


