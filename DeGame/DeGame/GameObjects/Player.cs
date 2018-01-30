﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Player : SpriteGameObject
{
    public Texture2D playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
    public bool CoolBoots, SlimyBoots, CrestShield, HelicopterHat;
    public bool onWeb, onIce, onSolid;
    public bool startup;
    new public bool Mirror;
    public double damagereduction, damagemultiplier;
    public double attackspeedreduction;
    public double attackmultiplier;
    public double extraspeed;
    public double poisonchance;
    double speed;
    float velocitybase;
    public float attack, extraattack;
    public float health, maxhealth;
    public float exp;
    float shoottimer = 0, nextLevelExp;
    public int leveltokens;
    public int level;
    public int gold;
    public int ammo;
    public string lastUsedspeed;
    public GameObjectList bullets;
    public Rectangle collisionhitbox;
    public static InventoryManager inventory;

    public Player(int layer = 0, string id = "Player")
    : base("Sprites/Characters/PlayerDown", 0, "Player")
    {
        bullets = new GameObjectList();
        inventory = new InventoryManager();
        lastUsedspeed = "Down";
        CalculateDamage();
        CalculateAmmo();
        GameEnvironment.soundManager.loadSoundEffect("Shoot");
        GameEnvironment.soundManager.loadSoundEffect("Hit");
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(position.X - origin.X);
            int top = (int)(position.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (startup)
        {
            inventory.startUp();
            startup = false;
        }
        base.Update(gameTime);
        bullets.Update(gameTime);
        //A special hitbox for terrain, so that part of your sprite has no collision with walls, rocks, etc, which looks nicer
        collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Height - 20);
        NextLevel();
        if (health <= 0)
        {
            GameEnvironment.gameStateManager.SwitchTo("GameOver");
        }
        else if (health > maxhealth)
        {
            health = maxhealth;
        }

        if (shoottimer > 0)
        {
            shoottimer--;
        }
        else
        {
            shoottimer = 0;
        }

        if(!HelicopterHat)
        {
            //Makes sure your slidingspeed is back to normal if you are on ice and just got past a spiderweb
            if (onIce && !onWeb && speed != 0.3f * extraspeed)
            {
                speed = 0.3f * extraspeed;
            }
            if (onWeb)
            {
                velocitybase = 0.15f;
            }
            else
            {
                velocitybase = 0.3f;
            }
            speed = velocitybase * extraspeed;
        }
    }

    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        // Player movement
        if ((onIce && onSolid) || !onIce || SlimyBoots || HelicopterHat)
        {
            if (inputHelper.IsKeyDown(Keys.W))
            {
                position.Y -= (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds);
                lastUsedspeed = "Up";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerUp");
            }
            if (inputHelper.IsKeyDown(Keys.S))
            {
                position.Y += (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds);
                lastUsedspeed = "Down";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                position.X += (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds);
                lastUsedspeed = "Right";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerRight");
            }
            if (inputHelper.IsKeyDown(Keys.A))
            {
                position.X -= (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds);
                lastUsedspeed = "Left";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerLeft");
            }

            position.X += (float)(speed * inputHelper.currentGamePadState.ThumbSticks.Left.X);
            position.Y -= (float)(speed * inputHelper.currentGamePadState.ThumbSticks.Left.Y);
            switch (inputHelper.getThumpDirection("left"))
            {
                case "Up":
                    playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerUp");
                    break;
                case "Down":
                    playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
                    break;
                case "Right":
                    playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerRight");
                    break;
                case "Left":
                    playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerLeft");
                    break;
            }
        }
        else
        {
            //Decides which direction you should slide when on ice
            if (lastUsedspeed == "Up")
            {
                position.Y -= (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            else if (lastUsedspeed == "Down")
            {
                position.Y += (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds); ;
            }
            else if (lastUsedspeed == "Right")
            {
                position.X += (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds); ;
            }
            else if (lastUsedspeed == "Left")
            {
                position.X -= (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds); ;
            }
        }
        if (ammo > 0 || ammo == -1)
        {
            // Player shooting
            if (inputHelper.IsKeyDown(Keys.Up) || inputHelper.getThumpDirection("right") == "up")
            {
                Shoot(1, gameTime);
            }
            else if (inputHelper.IsKeyDown(Keys.Down) || inputHelper.getThumpDirection("right") == "down")
            {
                Shoot(2, gameTime);
            }
            else if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.getThumpDirection("right") == "left")
            {
                Shoot(3, gameTime);
            }
            else if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.getThumpDirection("right") == "right")
            {
                Shoot(4, gameTime);
            }
        }
        if (leveltokens > 0 && (inputHelper.KeyPressed(Keys.O) || inputHelper.ButtonPressed(Buttons.RightTrigger)))
        {
            leveltokens--;
            GameEnvironment.gameStateManager.SwitchTo("Leveling");
        }
    }

    /// <summary>
    /// Resets ALL player stats
    /// </summary>
    public override void Reset()
    {
        List<GameObject> RemoveBullets = new List<GameObject>();
        health = 100;
        maxhealth = 100;
        gold = 0;
        level = 1;
        exp = 0;
        nextLevelExp = 500;
        attackspeedreduction = 1;
        damagereduction = 1;
        extraspeed = 1;
        extraattack = 0;
        attackmultiplier = 1;
        poisonchance = 0;
        leveltokens = 0;
        velocitybase = 0.3f;
        CalculateAmmo();
        CalculateDamage();
        onIce = false; onWeb = false; onSolid = false;
        CoolBoots = false; SlimyBoots = false; Mirror = false; CrestShield = false;
        foreach (Bullet bullet in PlayingState.player.bullets.Children)
        {
            RemoveBullets.Add(bullet);
        }
        foreach (Bullet bullet in RemoveBullets)
        {
            PlayingState.player.bullets.Remove(bullet);
        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            inventory.removeItemFromInventory(inventory.items[i]);
        }
        inventory.currentArmour = null;
        inventory.currentBoots = null;
        inventory.currentHelmet = null;
        inventory.currentShield = null;
        inventory.currentPassives[0] = null;
        inventory.currentPassives[1] = null;
        inventory.currentWeapon = new StandardBow();
        ammo = -1;
        IWeapon weapon = (IWeapon)inventory.currentWeapon;
        attack = weapon.AddedDamage;
        startup = true;
    }

    /// <summary>
    /// Method used by enemies to damage the player, makes sure all damage sources can be tracked down to one method
    /// </summary>
    public void TakeDamage(float enemydamage)
    {
        if (enemydamage > 0)
            GameEnvironment.soundManager.playSoundEffect("Hit");
        health -= (float)(enemydamage * damagereduction);
    }

    /// <summary>
    /// Checks if the player level should be raised
    /// </summary>
    void NextLevel()
    {
        if (exp >= nextLevelExp)
        {
            exp -= nextLevelExp;
            nextLevelExp += 200;
            leveltokens++;
            level++;
        }
    }

    /// <summary>
    /// Applies a buff chosen by the player on levelup to his/her stats
    /// </summary>
    public void StatIncrease(int type)
    {
        if (type == 1)
        {
            attackmultiplier *= 1.05;
            CalculateDamage();
        }
        if (type == 2)
        {
            maxhealth += 20;
            health += 20;
        }
        if (type == 3)
        {
            extraspeed *= 1.10;
        }
        if (type == 4)
        {
            attackspeedreduction *= 0.90;
        }
    }

    /// <summary>
    /// Calls on the 'Attack' method of the currently equiped weapon
    /// </summary>
    void Shoot(int direction, GameTime gametime)
    {
        if (shoottimer == 0)
        {
            IWeapon weapon = (IWeapon)inventory.currentWeapon;
            if (weapon != null)
            {
                GameEnvironment.soundManager.playSoundEffect("Shoot");
                weapon.Attack(direction);
                if (ammo > 0)
                {
                    ammo--;
                }
                shoottimer += (float)(weapon.AttackSpeed * attackspeedreduction * gametime.ElapsedGameTime.Milliseconds);
            }
        }
    }

    /// <summary>
    /// Calculates how much damage your weapon should do
    /// </summary>
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
        attack = (float)(weapon.AddedDamage * attackmultiplier + extraattack);
    }

    /// <summary>
    /// Calculates how much ammo each weapon should have
    /// </summary>
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

    public void changeMaxHealth(float changeAmout)
    {
        float currentHealthPercentage = health / maxhealth;
        maxhealth += changeAmout;
        health = maxhealth * currentHealthPercentage;
    }

    /// <summary>
    /// Draws the player and all items the player has equiped
    /// If no items are equiped, draw a normal set of clothes
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(playersprite, position, null, Color.White);
        if (ammo < 0)
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: infinite!", new Vector2(PlayingState.hud.screenwidth - 275 + (Camera.Position.X - PlayingState.hud.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.hud.screenheight / 2)), Color.White);
        else
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: " + Convert.ToString(ammo), new Vector2(PlayingState.hud.screenwidth - 275 + (Camera.Position.X - PlayingState.hud.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.hud.screenheight / 2)), Color.White);

        if (inventory.currentBoots != null)
            inventory.currentBoots.DrawOnPlayer(gameTime, spriteBatch);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/Boots" + lastUsedspeed), PlayingState.player.position, Color.White);

        if (inventory.currentArmour != null)
            inventory.currentArmour.DrawOnPlayer(gameTime, spriteBatch);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/Torso" + lastUsedspeed), PlayingState.player.position, Color.White);

        if (inventory.currentHelmet != null)
            inventory.currentHelmet.DrawOnPlayer(gameTime, spriteBatch);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/StandardHelmet" + lastUsedspeed), PlayingState.player.position, Color.White);

        if (inventory.currentPassives[0] != null)
            inventory.currentPassives[0].DrawOnPlayer(gameTime, spriteBatch);

        if (inventory.currentPassives[1] != null)
            inventory.currentPassives[1].DrawOnPlayer(gameTime, spriteBatch);

        if (inventory.currentWeapon != null)
            inventory.currentWeapon.DrawOnPlayer(gameTime, spriteBatch);

        if (inventory.currentShield != null)
            inventory.currentShield.DrawOnPlayer(gameTime, spriteBatch);

        bullets.Draw(gameTime, spriteBatch);
        if (leveltokens > 0)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/HUD/LevelUp"), new Vector2(position.X, position.Y - 30), Color.White);
    }
}