﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Bullet : SpriteGameObject
{
    public Bullet(Vector2 Startposition, int Direction, int layer = 0, string id = "bullet")
    : base("Sprites/Random", layer, id)
    {
        position = Startposition;

        if (Direction == 0)
            velocity.Y = 5;
        else if (Direction == 1)
            velocity.X = -5;
        else if (Direction == 2)
            velocity.Y = -5;
        else if (Direction == 3)
            velocity.X = 5;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position += velocity;
    }

    public void CheckCollisionEnemies()
    {
        // Als enemies in de room class zitten, en ze hebben op een of andere manier t goede ID, zou dit moeten werken
        GameObjectList enemies = Floor.currentRoom.Find("enemies") as GameObjectList;

        if (enemies != null)
        {
            foreach (SpriteGameObject enemy in enemies.Children)
            {
                if (CollidesWith(enemy))
                {
                    // Collission logic
                }
            }
        }
    }
}
