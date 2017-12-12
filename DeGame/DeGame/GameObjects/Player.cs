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
    public float health = 100;
    protected float maxhealth = 100;
    protected float attack;
    protected float attackspeed;
    protected float range;
    bool next = false;

    GameObjectList bullets;
    HealthBar healthbar;

    public Player(int layer = 0, string id = "Player")
    : base("Sprites/Random", 0, "Player")
    {
        bullets = new GameObjectList();
        position = new Vector2(100, 100);
        healthbar = new HealthBar(health, maxhealth);
    }

    public override void Update(GameTime gameTime)
    {
        healthbar.Update(gameTime, health, maxhealth);
        bullets.Update(gameTime);
        Console.WriteLine(position);
        base.Update(gameTime);
        //if (health <= 0)
            //GameEnvironment.gameStateManager.SwitchTo("GameOver");

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
        bullets.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.W))
            position.Y -= 5;
        if (inputHelper.IsKeyDown(Keys.S))
            position.Y += 5;
        if (inputHelper.IsKeyDown(Keys.D))
            position.X += 5;
        if (inputHelper.IsKeyDown(Keys.A))
            position.X -= 5;

        if (inputHelper.KeyPressed(Keys.Down))
        {
            Bullet bullet = new Bullet(position, 0);
            bullets.Add(bullet);
        }     
        if (inputHelper.KeyPressed(Keys.Left))
        {
            Bullet bullet = new Bullet(position, 1);
            bullets.Add(bullet);
        }
        if (inputHelper.KeyPressed(Keys.Up))
        {
            Bullet bullet = new Bullet(position, 2);
            bullets.Add(bullet);
        }
        if (inputHelper.KeyPressed(Keys.Right))
        {
            Bullet bullet = new Bullet(position, 3);
            bullets.Add(bullet);
        }
    }
}


