using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SlimeEnemy : Enemy
{
    int Counter = 0, Directioncount;
    float speed = 0.2f;
    Vector2 BulletPosition;

    /// <summary>
    /// Based on the direction parameter shoots in a pre-determined direction
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="roomposition"></param>
    /// <param name="directioncount"></param>
    /// <param name="Difficulty"></param>
    /// <param name="layer"></param>
    /// <param name="id"></param>
    public SlimeEnemy(Vector2 startPosition, Vector2 roomposition, int directioncount, int Difficulty = 0, int layer = 0, string id = "SlimeEnemy") : base(startPosition, roomposition, "Sprites/Enemies/SlimeEnemy", Difficulty, layer, id)
    {
        Directioncount = directioncount;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/Enemies/SlimeEnemy");
        position = startPosition;
        basevelocity = new Vector2(0, 0);
        health = 150 * statmultiplier;
        maxhealth = 150 * statmultiplier;
        expGive = 60 * statmultiplier;
        bulletdamage = 5 * statmultiplier;
        switch (Directioncount)
        {
            case 1:
                direction = new Vector2(-1, 0);
                BulletPosition = new Vector2(0, sprite.Height / 2 - 15);
                break;
            default:
                direction = new Vector2(1, 0);
                BulletPosition = new Vector2(sprite.Width, sprite.Height / 2 - 15);
                break;
        }        
    }

    /// <summary>
    /// Shoots the bullets in the already determined direction.
    /// </summary>
    /// <param name="gameTime"></param>
    public void Shoot(GameTime gameTime)
    {
        Counter += gameTime.ElapsedGameTime.Milliseconds;

        if (Counter >= 1200)
        {
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + BulletPosition, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/SlimeBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
            Counter = 0;
        }
    }

    /// <summary>
    /// If in the current Room: Shoot.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Shoot(gameTime);
    }

    /// <summary>
    /// Draw the enemy based upon earlier received parameters.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        switch (Directioncount)
        {
            case 1:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/SlimeEnemy2"), position, color);
                break;
            default:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/SlimeEnemy"), position, color);
                break;
        }
    }
}

