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
    protected float ammo = 20;
    bool next = false;
    public SpriteEffects Effect;
    public Vector2 velocityRightDown, velocityLeftUp, velocitybase;

    public GameObjectList bullets;
    HealthBar healthbar;

    public Player(int layer = 0, string id = "Player")
    : base("Sprites/Random", 0, "Player")
    {
        bullets = new GameObjectList();
        //position = new Vector2(100, 100);
        velocityRightDown = new Vector2(5, 5);
        velocityLeftUp = new Vector2(5, 5);
        velocitybase = new Vector2(5, 5);
        healthbar = new HealthBar(health, maxhealth, position);
    }

    // Update player and bullets
    public override void Update(GameTime gameTime)
    {
        healthbar.Update(gameTime, health, maxhealth,position);
        bullets.Update(gameTime);
        //Console.WriteLine(position);
        base.Update(gameTime);


        //if (health <= 0)
        //    GameEnvironment.gameStateManager.SwitchTo("GameOver");

    }

    // Draw player and bullets
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        string Ammo = Convert.ToString(ammo);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: " + Ammo, new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);
        bullets.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch, Vector2.Zero);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // Player movement
        if (inputHelper.IsKeyDown(Keys.W))
        {
            position.Y -= velocityLeftUp.Y;
        }
        if (inputHelper.IsKeyDown(Keys.S))
        {
            position.Y += velocityRightDown.Y;
        }
        if (inputHelper.IsKeyDown(Keys.D))
        {
            position.X += velocityRightDown.X;
            Effect = SpriteEffects.None;
        }
        if (inputHelper.IsKeyDown(Keys.A))
        {
            position.X -= velocityLeftUp.X;
            Effect = SpriteEffects.FlipHorizontally;
        }
        if (ammo > 0)
        {
            // Player shooting
            if (inputHelper.KeyPressed(Keys.Down))
            {
                Shoot(0);
            }
            else if (inputHelper.KeyPressed(Keys.Left))
            {
                Shoot(1);
            }
            else if (inputHelper.KeyPressed(Keys.Up))
            {
                Shoot(2);
            }
            else if (inputHelper.KeyPressed(Keys.Right))
            {
                Shoot(3);
            }
        }
    }

    // Shoot a bullet
    public void Shoot(int direction)
    {
        Bullet bullet = new Bullet(position, direction);
        bullets.Add(bullet);
        ammo--;
    }
}


