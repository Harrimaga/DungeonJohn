﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class EnemyBullet : E_Bullet
{
    Vector2 direction, actualvelocity;
    float speed, damage;
    Texture2D Bulletsprite;

    public EnemyBullet(float Damage, float Speed, Vector2 Startpositon, Vector2 Direction, Texture2D bulletsprite, int layer = 0, string id = "EnemyBullet") : base(Damage, Speed,"Sprites/Bullets/EnemyBullet", layer, id)
    {
        position = Startpositon;
        direction = (PlayingState.player.position - position);
        Position = Startpositon;
        speed = Speed;
        damage = Damage;
        direction = Direction;
        Bulletsprite = bulletsprite;
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
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 2);
        Vector2 newdirection = direction;
        if (position.X < MiddleofPlayer.X || position.X > MiddleofPlayer.X)
        {
            newdirection.X = -direction.X;
        }
        if (position.Y < MiddleofPlayer.Y|| position.Y > MiddleofPlayer.Y)
        {
            newdirection.Y = -direction.Y;
        }
        return newdirection;
    }
    
    /// <summary>
    /// Draws the bullet depending on the bullet used. 
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Bulletsprite, position);
    }
}
