using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Player : SpriteGameObject
{
    protected float health;
    protected float maxhealth;
    protected float attack;
    protected float attackspeed;
    protected float range;
    public Vector2 position;

    GameObjectList bullets;

    public Player(int layer = 0, string id = "Player")
    : base("Sprites/Random", 0, "Player")
    {
        bullets = new GameObjectList();
        position = new Vector2(100, 100);
    }

    // Update player and bullets
    public override void Update(GameTime gameTime)
    {
        bullets.Update(gameTime);
        base.Update(gameTime);    
    }

    // Draw player and bullets
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
        bullets.Draw(gameTime, spriteBatch);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // Player movement
        if (inputHelper.IsKeyDown(Keys.W))
            position.Y = position.Y - 5;
        if (inputHelper.IsKeyDown(Keys.S))
            position.Y += 5;
        if (inputHelper.IsKeyDown(Keys.D))
            position.X = position.X + 5;
        if (inputHelper.IsKeyDown(Keys.A))
            position.X = position.X - 5;

        // Player shooting
        if (inputHelper.KeyPressed(Keys.Down))
            Shoot(0);    
        if (inputHelper.KeyPressed(Keys.Left))
            Shoot(1);
        if (inputHelper.KeyPressed(Keys.Up))
            Shoot(2);
        if (inputHelper.KeyPressed(Keys.Right))
            Shoot(3);
    }

    // Shoot a bullet
    public void Shoot(int direction)
    {
        Bullet bullet = new Bullet(position, direction);
        bullets.Add(bullet);
    }
}


