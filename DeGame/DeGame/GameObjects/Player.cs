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
    public float health = 100, maxhealth = 200;
    public float exp = 0,nextLevelExp = 100;
    public int level = 0;
    public float attack;
    public float attackspeed;
    public float range;
    public int ammo;
    public bool state = false;
    bool next = false;
    public SpriteEffects Effect;
    public Vector2 velocitybase;
    HealthBar healthbar;
    public int gold = 0;
    public GameObjectList bullets;
    public static InventoryManager inventory;

    public Player(int layer = 0, string id = "Player")
    : base("Sprites/Random", 0, "Player")
    {
        bullets = new GameObjectList();
        velocitybase = new Vector2(5, 5);
        healthbar = new HealthBar(health, maxhealth, position, true);
        velocity = velocitybase;
        inventory = new InventoryManager();
        CalculateDamage();
        CalculateAmmo();
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(position.X - origin.X);
            int top = (int)(position.Y - origin.Y);
            return new Rectangle(left , top , Width, Height);
        }
    }

    public override void Reset()
    {
        List<GameObject> RemoveBullets = new List<GameObject>();
        maxhealth = 100;
        health = 100;
        //ammo = 20;
        gold = 0;
        level = 1;
        exp = 0;
        CalculateAmmo();
        CalculateDamage();
        foreach (Bullet bullet in PlayingState.player.bullets.Children)
            RemoveBullets.Add(bullet);        
        foreach (Bullet bullet in RemoveBullets)        
            PlayingState.player.bullets.Remove(bullet);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        healthbar.Update(gameTime, health, maxhealth,position);
        bullets.Update(gameTime);
        if (health > maxhealth)
        {
            health = maxhealth;
        }
        if (health <= 0)
        {
            GameEnvironment.gameStateManager.SwitchTo("GameOver");
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
        if (ammo < 0)
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: infinite!", new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);

        }
        else
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: " + Convert.ToString(ammo), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);

        }
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Player Level: " + Convert.ToString(level), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 200 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Damage: " + Convert.ToString(attack), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 225 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);
        bullets.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch, Vector2.Zero);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // Player movement
        if (inputHelper.IsKeyDown(Keys.W))
        {
            position.Y -= velocity.Y;
        }
        if (inputHelper.IsKeyDown(Keys.S))
        {
            position.Y += velocity.Y;
        }
        if (inputHelper.IsKeyDown(Keys.D))
        {
            position.X += velocity.X;
            Effect = SpriteEffects.None;
        }
        if (inputHelper.IsKeyDown(Keys.A))
        {
            position.X -= velocity.X;
            Effect = SpriteEffects.FlipHorizontally;
        }
        if (ammo > 0 || ammo == -1)
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
        if(state==true)
        {
            GameEnvironment.gameStateManager.SwitchTo("Leveling");
            /*if (inputHelper.currentKeyboardState.IsKeyDown(Keys.N))
            {
                StateIncrease(1);
                state = false;
            }
            if (inputHelper.currentKeyboardState.IsKeyDown(Keys.M))
            {
                StateIncrease(2);
                state = false;
            }*/
        }
    }

    public void NextLevel()
    {
        if(exp >= nextLevelExp)
        {
            exp -= nextLevelExp;
            nextLevelExp += 20;
            level++;
            state = true;
        }
    }
    public void StateIncrease(int type)
    {
        if (type == 1)
        {
            attack+= 5;
        }
        if (type == 2)
        {
            maxhealth += 50;
            health += 50;
        }
    }
    public void Shoot(int direction)
    {
        Bullet bullet = new Bullet(position, direction);
        bullets.Add(bullet);
        if (ammo > 1)
        {
            ammo--;
        }
    }

    public void CalculateDamage()
    {
        IWeapon weapon = (IWeapon)inventory.currentWeapon;
        IPassive[] passives = new IPassive[2];
        
        if (inventory.currentPassives[0] != null)
        {
            passives[0] = (IPassive)inventory.currentPassives[0];
        }
        if (inventory.currentPassives[1] != null)
        {
            passives[1] = (IPassive)inventory.currentPassives[1];
        }

        if (passives[0] == null)
        {
            attack = weapon.AddedDamage * weapon.DamageMultiplier;
        }
        else if (passives[0] != null && passives[1] == null)
        {
            attack = weapon.AddedDamage * Math.Max(weapon.DamageMultiplier, passives[0].DamageMultiplier);
        }
        else if (passives[1] != null)
        {
            attack = weapon.AddedDamage * Math.Max(weapon.DamageMultiplier, Math.Max(passives[0].DamageMultiplier, passives[1].DamageMultiplier));
        }
    }

    public void CalculateAmmo()
    {
        IWeapon weapon = (IWeapon)inventory.currentWeapon;
        ammo = weapon.Ammo;
    }
}