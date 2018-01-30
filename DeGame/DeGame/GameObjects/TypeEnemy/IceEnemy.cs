using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class IceEnemy : Enemy
{
    int Counter = 75;
    float speed = 0.1f;
    Vector2 direction;

    public IceEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition,"Sprites/Enemies/IceEnemy", Difficulty, layer, id)
    {
        position = startPosition;
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyIceBullet");
        velocity = new Vector2(0.5f, 0.5f);
        bulletdamage = 5 * statmultiplier;
        contactdamage = 2 * statmultiplier;
        Console.WriteLine("Playerposition" + PlayingState.player.position);
        Console.WriteLine("position = " + position);
        Console.WriteLine("direction =" + direction);
        killable = false;
    }

    /// <summary>
    /// If Enemy is in within Range shoot.
    /// </summary>
    /// <param name="gameTime"></param>
    public void Range(GameTime gameTime)
    {
        Counter += 1 *gameTime.ElapsedGameTime.Milliseconds;
        if (Counter >= 1000)
        {
            if (PlayingState.player.position.X + range > position.X || PlayingState.player.position.X - range < position.X ||
            PlayingState.player.position.Y + range > position.Y || PlayingState.player.position.Y - range < position.Y)
            {
                Shoot();
                Counter = 0;
            }
        }
    }

    /// <summary>
    /// Shoots the bullet at the player
    /// </summary>
    public void Shoot()
    {
        Vector2 MiddenOfSprite = new Vector2(sprite.Width / 4 - 10, sprite.Height / 4);
        IceBullet bullet = new IceBullet(bulletdamage, speed, position + MiddenOfSprite, direction);
        PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
    }

    /// <summary>
    /// Executes Range & calculates the direction
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Range(gameTime);
        direction = (PlayingState.player.position - position);
    }

    /// <summary>
    /// Draws Enemy.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/IceEnemy"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}