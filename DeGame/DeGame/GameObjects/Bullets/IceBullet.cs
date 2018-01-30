using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class IceBullet : E_Bullet
{
    Vector2 direction, actualvelocity;
    float speed, damage;

    public IceBullet(float Damage, float Speed, Vector2 Startpositon, Vector2 Direction, int layer = 0, string id = "IceBullet") : base(Damage, Speed, "Sprites/Bullets/EnemyIceBullet", layer, id)
    {
        position = Startpositon;
        Position = Startpositon;
        speed = Speed;
        damage = Damage;
        direction = Direction;
    }

    /// <summary>
    /// Updates the bullets depending on if it is reflected or not
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Position = position;
        direction.Normalize();
        actualvelocity = direction * speed;
        position.X += actualvelocity.X * gameTime.ElapsedGameTime.Milliseconds;
        position.Y += actualvelocity.Y * gameTime.ElapsedGameTime.Milliseconds;
        if (changedirection)
        {
            direction = CalculateReflect(direction);
            changedirection = false;
        }
    }

    /// <summary>
    /// Calculates the direction of the reflected bullet.
    /// </summary>
    Vector2 CalculateReflect(Vector2 direction)
    {
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/Random").Height / 2);
        Vector2 newdirection = direction;
        if (position.X < MiddleofPlayer.X - GameEnvironment.assetManager.GetSprite("Sprites/Characters/Random").Width / 2 || position.X > MiddleofPlayer.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/Random").Width / 2)
        {
            newdirection.X = -direction.X;
        }
        if (position.Y < MiddleofPlayer.Y - GameEnvironment.assetManager.GetSprite("Sprites/Characters/Random").Height / 2 || position.Y > MiddleofPlayer.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/Random").Height / 2)
        {
            newdirection.Y = -direction.Y;
        }
        return newdirection;
    }

    /// <summary>
    /// Draws the bullet.
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyIceBullet"), position);
    }
}
