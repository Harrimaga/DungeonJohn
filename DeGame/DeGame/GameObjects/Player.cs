using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Player : SpriteGameObject
{
    public bool state = false, onWeb = false, onIce = false, onSolid = false, next = false;
    Texture2D playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
    public bool CoolBoots = false, SlimyBoots = false, VialOfPoison = false, CrestShield = false;
    new public bool Mirror = false;
    public float health = 100, maxhealth = 100;
    public float exp = 0, nextLevelExp = 100;
    public float attackspeedreduction = 0;
    public double damagereduction = 1;
    public float extraspeed = 1;
    public float attack;
    public float attackspeed;
    public float speed;
    public float range;
    public float extraattack = 0;
    public int ammo;
    public int gold = 0;
    public int level = 0;
    public int leveltokens = 0;
    public SpriteEffects Effect;
    public float velocitybase = 0.3f;
    HealthBar healthbar;
    public GameObjectList bullets;
    public static InventoryManager inventory;
    float shoottimer = 0;
    public string lastUsedspeed;
    public Rectangle collisionhitbox;
    bool startup = true;

    public Player(int layer = 0, string id = "Player")
    : base("Sprites/Characters/PlayerDown", 0, "Player")
    {
        bullets = new GameObjectList();
        healthbar = new HealthBar(health, maxhealth, position, true);
        inventory = new InventoryManager();
        lastUsedspeed = "Down";
        CalculateDamage();
        CalculateAmmo();
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
        collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Height - 20);
        healthbar.Update(gameTime, health, maxhealth, position);
        bullets.Update(gameTime);
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

    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        // Player movement
        if ((onIce && onSolid) || !onIce || SlimyBoots)
        {
            if (inputHelper.IsKeyDown(Keys.W))
            {
                position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                lastUsedspeed = "Up";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerUp");
            }
            if (inputHelper.IsKeyDown(Keys.S))
            {
                position.Y += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                lastUsedspeed = "Down";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                lastUsedspeed = "Right";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerRight");
            }
            if (inputHelper.IsKeyDown(Keys.A))
            {
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                lastUsedspeed = "Left";
                playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerLeft");
            }

            position.X += speed * inputHelper.currentGamePadState.ThumbSticks.Left.X;
            position.Y -= speed * inputHelper.currentGamePadState.ThumbSticks.Left.Y;
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
                position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (lastUsedspeed == "Down")
            {
                position.Y += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (lastUsedspeed == "Right")
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (lastUsedspeed == "Left")
            {
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
        if (ammo > 0 || ammo == -1)
        {
            // Player shooting
            if (inputHelper.IsKeyDown(Keys.Up) || inputHelper.getThumpDirection("right") == "up")
            {
                Shoot(1);
            }
            else if (inputHelper.IsKeyDown(Keys.Down) || inputHelper.getThumpDirection("right") == "down")
            {
                Shoot(2);
            }
            else if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.getThumpDirection("right") == "left")
            {
                Shoot(3);
            }
            else if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.getThumpDirection("right") == "right")
            {
                Shoot(4);
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
        nextLevelExp = 100;
        attackspeedreduction = 0;
        damagereduction = 1;
        extraspeed = 1;
        extraattack = 0;
        leveltokens = 0;
        velocitybase = 0.3f;
        CalculateAmmo();
        CalculateDamage();
        onIce = false;
        onWeb = false;
        onSolid = false;
        foreach (Bullet bullet in PlayingState.player.bullets.Children)
        {
            RemoveBullets.Add(bullet);
        }
        foreach (Bullet bullet in RemoveBullets)
        {
            PlayingState.player.bullets.Remove(bullet);
        }
        startup = true;
    }

    public void NextLevel()
    {
        if (exp >= nextLevelExp)
        {
            exp -= nextLevelExp;
            nextLevelExp += 20;
            leveltokens++;
            level++;
            state = true;
        }
    }

    public void StatIncrease(int type)
    {
        if (type == 1)
        {
            extraattack += 5;
            CalculateDamage();
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
            if (weapon != null)
            {
                weapon.Attack(direction);
                if (ammo > 0)
                {
                    ammo--;
                }
                shoottimer += weapon.AttackSpeed - attackspeedreduction;
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

        if (passives[0] == null && weapon != null)
        {
            attack = weapon.AddedDamage * weapon.DamageMultiplier + extraattack;
        }
        else if (passives[0] != null && passives[1] == null && weapon != null)
        {
            attack = weapon.AddedDamage * Math.Max(weapon.DamageMultiplier, passives[0].DamageMultiplier) + extraattack;
        }
        else if (passives[1] != null && weapon != null)
        {
            attack = weapon.AddedDamage * Math.Max(weapon.DamageMultiplier, Math.Max(passives[0].DamageMultiplier, passives[1].DamageMultiplier)) + extraattack;
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

    public void changeMaxHealth(float changeAmout)
    {
        float currentHealthPercentage = health / maxhealth;
        maxhealth += changeAmout;
        health = maxhealth * currentHealthPercentage;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(playersprite, position, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
        if (ammo < 0)
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: infinite!", new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);
        else
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Ammo: " + Convert.ToString(ammo), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 175 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);

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

        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Player Level: " + Convert.ToString(level), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 200 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Damage: " + Convert.ToString(attack), new Vector2(PlayingState.currentFloor.screenwidth - 275 + (Camera.Position.X - PlayingState.currentFloor.screenwidth / 2), 225 + (Camera.Position.Y - PlayingState.currentFloor.screenheight / 2)), Color.Red);
        bullets.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch);
        if (leveltokens > 0)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/HUD/LevelUp"), new Vector2(position.X, position.Y - 30), Color.White);
    }
}