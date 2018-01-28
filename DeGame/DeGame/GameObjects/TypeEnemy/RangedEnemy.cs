using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class RangedEnemy : Enemy
{
    int Counter = 175;
    float speed = 0.13f;
    new Vector2 direction;
    
    public RangedEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/ShootingEnemy1", Difficulty, layer, id)
    {
        position = startPosition;
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet");
        velocity = new Vector2(0.05f, 0.05f);
        bulletdamage = 7 * statmultiplier;
        health = 90 * statmultiplier;
        maxhealth = 100 * statmultiplier;
        expGive = 100 * statmultiplier;
        contactdamage = 2 * statmultiplier;
        basevelocity = new Vector2(0.08f, 0.08f);
    }

    public void Range(GameTime gameTime)
    {
        Counter += 1 * gameTime.ElapsedGameTime.Milliseconds;
        if ((PlayingState.player.position.X + range < position.X || PlayingState.player.position.X - range > position.X ||
           PlayingState.player.position.Y + range < position.Y || PlayingState.player.position.Y - range > position.Y))
        {
            Chase();
        }
        else
        direction = Vector2.Zero;

        if(Counter >= 175)
        {
            Shoot();
            Counter = 0;
        }
    }

    public void Shoot()
    {
        Vector2 middleofsprite = new Vector2(sprite.Width / 2, sprite.Height / 2);
        direction = (PlayingState.player.position - position);
        EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + middleofsprite, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
        PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
              Range(gameTime);
        direction = (PlayingState.player.position - position);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/ShootingEnemy1"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}
