using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HalfEnemy : Enemy
{
    int Side;
   
    /// <summary>
    /// Gives both the halves their own velocity
    /// </summary>
    public HalfEnemy(Vector2 startPosition, Vector2 roomposition, int side, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/2PartEnemyLeft", Difficulty, layer, id)
    {
        Side = side;
        basevelocity = new Vector2(0.03f, 0.03f);
        health = 100 * statmultiplier;
        maxhealth = 100 * statmultiplier;
        expGive = 50 * statmultiplier;
        contactdamage = 10 * statmultiplier;
        killable = true;
        hpdisplay = true;
        contactdamage = 10 * statmultiplier;
        switch (Side)
        {
            case 1:
                direction = new Vector2(-1, 0);
                break;
            default:
                direction = new Vector2(1, 0);
                break;
        }
    }

    /// <summary>
    /// If in currentroom: Chase
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }
        

        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Chase();
    }

    /// <summary>
    /// Draws the different sides of the enemy.
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        switch (Side)
        {
            case 1:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyLeft"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
                break;
            default:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/2PartEnemyRight"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
                break;
        }
    }
}