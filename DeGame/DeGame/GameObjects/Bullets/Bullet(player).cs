using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

class Bullet : SpriteGameObject
{
    Texture2D Bulletsprite, Playersprite, CurrentPlayersprite, PlayerSpriteLeft, PlayerSpriteRight;
    SpriteEffects Effect = SpriteEffects.None;
    float counter = 0;
    int direction;
    public bool poisonbullet = false;
    Random random = new Random();
    Color color = Color.White;

    public Bullet(Vector2 Startposition, int Direction, int layer = 0, string id = "bullet")
    : base("", layer, id)
    {
        IWeapon weapon = (IWeapon)Player.inventory.currentWeapon;
        // Determine the direction of the bullets
        direction = Direction;
        CurrentPlayersprite = PlayingState.player.playersprite;
        PlayerSpriteLeft = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerLeft");
        PlayerSpriteRight = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerRight");
        if (direction == 1)
        {
            Playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerUp");
            velocity.Y = -weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteUp;
            position = PlayingState.player.position + new Vector2((Playersprite.Width / 2) - (Bulletsprite.Width / 2), (-Bulletsprite.Width / 2));
        }
        else if (direction == 2)
        {
            Playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
            velocity.Y = weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteUp;
            Effect = SpriteEffects.FlipVertically;
            position = PlayingState.player.position + new Vector2((Playersprite.Width / 2) - (Bulletsprite.Width / 2), (Playersprite.Height - Bulletsprite.Height / 2));
        }
        else if (direction == 3 && CurrentPlayersprite == PlayerSpriteLeft || direction == 3 && CurrentPlayersprite == PlayerSpriteRight)
        {
            Playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerLeft");
            velocity.X = -weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteLeft;
            position = PlayingState.player.position + new Vector2(0, (Playersprite.Height / 2) - (Bulletsprite.Height / 2));
        }
        else if (direction == 3)
        {
            Playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerLeft");
            velocity.X = -weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteLeft;
            position = PlayingState.player.position + new Vector2(-Bulletsprite.Width / 3, (Playersprite.Height / 2) - (Bulletsprite.Height / 2));
        }
        else if (direction == 4 && CurrentPlayersprite == PlayerSpriteRight || direction == 4 && CurrentPlayersprite == PlayerSpriteLeft)
        {
            Playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerRight");
            velocity.X = weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteLeft;
            Effect = SpriteEffects.FlipHorizontally;
            position = PlayingState.player.position + new Vector2((Playersprite.Width - Bulletsprite.Width), (Playersprite.Height / 2) - (Bulletsprite.Height / 2));
        }
        else if (direction == 4)
        {
            Playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerRight");
            velocity.X = weapon.Projectile_Velocity;
            Bulletsprite = weapon.BulletSpriteLeft;
            Effect = SpriteEffects.FlipHorizontally;
            position = PlayingState.player.position + new Vector2((Playersprite.Width - Bulletsprite.Width / 2), (Playersprite.Height / 2) - (Bulletsprite.Height / 2));
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
        if (counter + weapon.Projectile_Velocity * gameTime.ElapsedGameTime.Milliseconds >= weapon.Range)
        {
            if (direction == 1)
                position.Y -= weapon.Range - counter;
            if (direction == 2)
                position.Y += weapon.Range - counter;
            if (direction == 3)
                position.X -= weapon.Range - counter;
            if (direction == 4)
                position.X += weapon.Range - counter;

            GameObjectList.RemovedObjects.Add(this);
        }
        else
            counter += weapon.Projectile_Velocity * gameTime.ElapsedGameTime.Milliseconds;
        position.X += velocity.X * gameTime.ElapsedGameTime.Milliseconds;
        position.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
        CheckCollision();
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int topx = (int)position.X;
            int topy = (int)position.Y;
            int Width = Bulletsprite.Width;
            int Height = Bulletsprite.Height;
            return new Rectangle(topx, topy, Width, Height);
        }
    }
    public void CheckCollision()
    {
        foreach (Solid solid in PlayingState.currentFloor.currentRoom.solid.Children)
            if (CollidesWith(solid) && solid.hittable)
                GameObjectList.RemovedObjects.Add(this);
        foreach (Door door in Room.door.Children)
        {
            if (CollidesWith(door))
            {
                GameObjectList.RemovedObjects.Add(this);
                return;
            }
        }
    }

    // Draw the bullets
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Bulletsprite, position, null, color, 0f, Vector2.Zero, 1f, Effect, 0f);
    }
}
