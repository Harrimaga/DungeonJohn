using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

class Bullet : SpriteGameObject
{
    Texture2D Bulletsprite;
    SpriteEffects Effect = SpriteEffects.None;
    float counter = 0;
    int direction;
    public bool poisonbullet = false;
    Random random = new Random();
    Color color = Color.White;

    public Bullet(Vector2 Startposition, int Direction, int layer = 0, string id = "bullet")
    : base("Sprites/Characters/PlayerFront", layer, id)
    {
        IWeapon weapon = (IWeapon)Player.inventory.currentWeapon;
        position = Startposition;
        // Determine the direction of the bullets
        direction = Direction;
        if (direction == 1)
        {
            velocity.Y = -weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteUp;
        }
        else if (direction == 2)
        {
            velocity.Y = weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteUp;
            Effect = SpriteEffects.FlipVertically;
        }
        else if (direction == 3)
        {
            velocity.X = -weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteLeft;
        }
        else if (direction == 4)
        {
            velocity.X = weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteLeft;
            Effect = SpriteEffects.FlipHorizontally;
        }
        if (PlayingState.player.VialOfPoison && random.Next(100) < 25)
        {
            poisonbullet = true;
            color = Color.YellowGreen;
        }
    }

    // Update the bullets
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        IWeapon weapon = (IWeapon)Player.inventory.currentWeapon;
        if (counter + weapon.Projectile_Velocity >= weapon.Range)
        {
            GameObjectList.RemovedObjects.Add(this);
        }
        else
            counter += weapon.Projectile_Velocity;
        position.X += velocity.X * gameTime.ElapsedGameTime.Milliseconds;
        position.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
        CheckCollision();
    }

    public void CheckCollision()
    {
        foreach (Door door in PlayingState.currentFloor.currentRoom.Children)
            if (CollidesWith(door))
                GameObjectList.RemovedObjects.Add(this);

        foreach (Solid solid in PlayingState.currentFloor.currentRoom.solid.Children)
            if (CollidesWith(solid))
                GameObjectList.RemovedObjects.Add(this);
    }

    // Draw the bullets
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Bulletsprite, position, null, color, 0f, Vector2.Zero, 1f, Effect, 0f);
    }
}
