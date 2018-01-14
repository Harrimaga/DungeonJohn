using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Player : SpriteGameObject
{
    public bool state = false, onWeb = false, onIce = false, onSolid = false, next = false;
    Texture2D playersprite = GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront");
    public bool CoolBoots = false, SlimyBoots = false, Mirror = true;
    public float health = 100, maxhealth = 200;
    public float exp = 0,nextLevelExp = 100;
    public float attackspeedreduction = 0;
    public double damagereduction = 1;
    public float extraspeed = 0;
    public float attack;
    public float attackspeed;
    public float speed;
    public float range;
    public int ammo;
    public int gold = 0;
    public int level = 0;
    public SpriteEffects Effect;
    public float velocitybase = 5;
    HealthBar healthbar;
    public GameObjectList bullets;
    public static InventoryManager inventory;
    int leveltokens = 0;
    float shoottimer = 0;
    string lastUsedspeed;
    public Rectangle collisionhitbox;

    public Player(int layer = 0, string id = "Player")
    : base("Sprites/PlayerFront", 0, "Player")
    {
        bullets = new GameObjectList();
        healthbar = new HealthBar(health, maxhealth, position, true);
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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Height - 20);
        speed = velocitybase + extraspeed;
        healthbar.Update(gameTime, health, maxhealth,position);
        bullets.Update(gameTime);
        NextLevel();
        if (health > maxhealth)
        {
            health = maxhealth;
        }
        if (health <= 0)
        {
            GameEnvironment.gameStateManager.SwitchTo("GameOver");
        }

        if (shoottimer < 0)
        {
            shoottimer = 0;
        }

        if (shoottimer > 0)
        {
            shoottimer--;
        }
        if (onIce && !onWeb && speed != 5 + extraspeed)
        {
            speed = 5 + extraspeed;
        }
        if (onWeb)
        {
            velocitybase = 2.5f;
        }
        else
        {
            velocitybase = 5;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // Player movement
        if ((onIce && onSolid) || !onIce || SlimyBoots)
        {
            if (inputHelper.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
                lastUsedspeed = "up";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/PlayerBack");
            }
            if (inputHelper.IsKeyDown(Keys.S))
            {
                position.Y += speed;
                lastUsedspeed = "down";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront");
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                position.X += speed;
                lastUsedspeed = "right";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/PlayerRight");
            }
            if (inputHelper.IsKeyDown(Keys.A))
            {
                position.X -= speed;
                lastUsedspeed = "left";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/PlayerLeft");
            }
        }
        else
        {
            if(lastUsedspeed == "up")
            {
                position.Y -= speed;
            }
            else if (lastUsedspeed == "down")
            {
                position.Y += speed;
            }
            else if (lastUsedspeed == "right")
            {
                position.X += speed;
            }
            else if (lastUsedspeed == "left")
            {
                position.X -= speed;
            }
        }
        if (ammo > 0 || ammo == -1)
        {
            // Player shooting
            if (inputHelper.IsKeyDown(Keys.Up))
            {
                Shoot(1);
            }
            else if (inputHelper.IsKeyDown(Keys.Down))
            {
                Shoot(2);
            }
            else if (inputHelper.IsKeyDown(Keys.Left))
            {
                Shoot(3);
            }
            else if (inputHelper.IsKeyDown(Keys.Right))
            {
                Shoot(4);
            }
        }
        if(leveltokens > 0 && inputHelper.KeyPressed(Keys.O))
        {
            leveltokens--;
            GameEnvironment.gameStateManager.SwitchTo("Leveling");
        }
    }

    public override void Reset()
    {
        List<GameObject> RemoveBullets = new List<GameObject>();
        health = 100;
        maxhealth = 100;
        //ammo = 20;
        gold = 0;
        level = 1;
        exp = 0;
        nextLevelExp = 100;
        leveltokens = 0;
        CalculateAmmo();
        CalculateDamage();
        foreach (Bullet bullet in PlayingState.player.bullets.Children)
        {
            RemoveBullets.Add(bullet);
        }  
        foreach (Bullet bullet in RemoveBullets)
        {
            PlayingState.player.bullets.Remove(bullet);
        }   
    }

    public void NextLevel()
    {
        if(exp >= nextLevelExp)
        {
            exp -= nextLevelExp;
            nextLevelExp += 20;
            leveltokens++;
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
        if (shoottimer == 0)
        {
            IWeapon weapon = (IWeapon)inventory.currentWeapon;
            weapon.Attack(direction);
            if (ammo > 0)
            {
                ammo--;
            }
            shoottimer += weapon.AttackSpeed - attackspeedreduction;
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

        if (passives[0] == null && weapon != null)
        {
            attack = weapon.AddedDamage * weapon.DamageMultiplier;
        }
        else if (passives[0] != null && passives[1] == null && weapon != null)
        {
            attack = weapon.AddedDamage * Math.Max(weapon.DamageMultiplier, passives[0].DamageMultiplier);
        }
        else if (passives[1] != null && weapon != null)
        {
            attack = weapon.AddedDamage * Math.Max(weapon.DamageMultiplier, Math.Max(passives[0].DamageMultiplier, passives[1].DamageMultiplier));
        }
    }

    public void CalculateAmmo()
    {
        IWeapon weapon = (IWeapon)inventory.currentWeapon;
        try
        {
            ammo = weapon.Ammo;
        }
        catch (Exception e)
        {
            inventory.currentWeapon = new StandardBow();
            CalculateAmmo();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(playersprite, position, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
        if (ammo < 0)
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: infinite!", new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);

        }
        else
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: " + Convert.ToString(ammo), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);

        }
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Player Level: " + Convert.ToString(level), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 200 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Damage: " + Convert.ToString(attack), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 225 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.Red);
        bullets.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch);
    }
}