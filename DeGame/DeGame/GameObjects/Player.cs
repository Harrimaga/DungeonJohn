using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Player : SpriteGameObject
{
    public Texture2D playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
    public bool CoolBoots, SlimyBoots, VialOfPoison, CrestShield, HelicopterHat;
    public bool onWeb, onIce, onSolid;
    public bool startup;
    new public bool Mirror;
    public double damagereduction, damagemultiplier;
    public double attackspeedreduction;
    public double attackmultiplier;
    public double extraspeed;
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
    SoundEffect shootsound, hitsound;

    public Player(int layer = 0, string id = "Player")
    : base("Sprites/Characters/PlayerDown", 0, "Player")
    {
        bullets = new GameObjectList();
        inventory = new InventoryManager();
        lastUsedspeed = "Down";
        CalculateDamage();
        CalculateAmmo();
        shootsound = GameEnvironment.assetManager.GetSound("SoundEffects/Shoot");
        hitsound = GameEnvironment.assetManager.GetSound("SoundEffects/Hit");
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
            //krijg weer originele snelheid als je over ijs glijdt en van een spinnenweb afkomt
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

    public override void Reset()
    {
        List<GameObject> RemoveBullets = new List<GameObject>();
        health = 100;
        maxhealth = 100;
        gold = 0;
        level = 1;
        exp = 0;
        nextLevelExp = 400;
        attackspeedreduction = 1;
        damagereduction = 1;
        extraspeed = 1;
        extraattack = 0;
        attackmultiplier = 1;
        leveltokens = 0;
        velocitybase = 0.3f;
        CalculateAmmo();
        CalculateDamage();
        onIce = false; onWeb = false; onSolid = false;
        CoolBoots = false; SlimyBoots = false; Mirror = false; VialOfPoison = false; CrestShield = false;
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
        IWeapon weapon = (IWeapon)inventory.currentWeapon;
        attack = weapon.AddedDamage;
        startup = true;
    }

    public void TakeDamage(float enemydamage)
    {
        hitsound.Play();
        health -= (float)(enemydamage * damagereduction);
    }

    void NextLevel()
    {
        if (exp >= nextLevelExp)
        {
            exp -= nextLevelExp;
            nextLevelExp += 100;
            leveltokens++;
            level++;
        }
    }

    public void StatIncrease(int type)
    {
        if (type == 1)
        {
            attackmultiplier *= 1.05;
            CalculateDamage();
        }
        if (type == 2)
        {
            maxhealth += 50;
            health += 50;
        }
    }

    void Shoot(int direction, GameTime gametime)
    {
        if (shoottimer == 0)
        {
            IWeapon weapon = (IWeapon)inventory.currentWeapon;
            if (weapon != null)
            {
                shootsound.Play();
                weapon.Attack(direction);
                if (ammo > 0)
                {
                    ammo--;
                }
                shoottimer += (float)(weapon.AttackSpeed * attackspeedreduction * gametime.ElapsedGameTime.Milliseconds);
            }
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
        attack = (float)(weapon.AddedDamage * attackmultiplier + extraattack);
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

    public void changeMaxHealth(float changeAmout)
    {
        float currentHealthPercentage = health / maxhealth;
        maxhealth += changeAmout;
        health = maxhealth * currentHealthPercentage;
    }

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