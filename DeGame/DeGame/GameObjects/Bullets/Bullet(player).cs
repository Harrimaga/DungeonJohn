using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

class Bullet : SpriteGameObject
{
    Texture2D Bulletsprite;
    SpriteEffects Effect = SpriteEffects.None;

    public Bullet(Vector2 Startposition, int Direction, float projectile_velocity, int layer = 0, string id = "bullet")
    : base("Sprites/Random", layer, id)
    {
        position = Startposition;

        // Determine the direction of the bullets
        if (Direction == 1)
        {
            velocity.Y = projectile_velocity;
            Bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/bulletup");
            Effect = SpriteEffects.FlipVertically;
        }
        else if (Direction == 2)
        {
            velocity.X = -projectile_velocity;
            Bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/bulletleft");
        }
        else if (Direction == 3)
        {
            velocity.Y = -projectile_velocity;
            Bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/bulletup");
        }
        else if (Direction == 4)
        {
            velocity.X = projectile_velocity;
            Bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/bulletleft");
            Effect = SpriteEffects.FlipHorizontally;
        }
    }

    // Update the bullets
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position += velocity;
        CheckCollision();
    }

    // Draw the bullets
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Bulletsprite, position, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
    }

    public void CheckCollision()
    {
        foreach(Solid solid in Room.solid.Children)        
            if (CollidesWith(solid))            
                GameObjectList.RemovedObjects.Add(this);

        foreach (Door door in Room.door.Children)
            if (CollidesWith(door))
                GameObjectList.RemovedObjects.Add(this);

        /*foreach (Wall wall in Room.solid.Children)
            if (CollidesWith(wall))            
                GameObjectList.RemovedObjects.Add(this);*/
    }

}
