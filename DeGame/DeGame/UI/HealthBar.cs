using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class HealthBar
{
    float health;
    float maxhealth;
    float newhealth;
    int healthbarwidth = 200, healthbarwidth2 = 200;
    bool isPlayer, isBoss;
    string HP = "N/A";
    Rectangle healthbar, healthbar2;

    public HealthBar(float healthObject, float maxhealthObject, Vector2 position, bool IsPlayer = false, bool IsBoss = false)
    {
        health = healthObject;
        maxhealth = maxhealthObject;
        newhealth = health;
        isPlayer = IsPlayer;
        isBoss = IsBoss;
    }
    public void Update(GameTime gameTime, float newhealthUpdate, float maxhealthUpdate, Vector2 positionNow)
    {
        newhealth = newhealthUpdate;
        maxhealth = maxhealthUpdate;
        if (newhealth < 0)
        {
            newhealth = 0;
        }

        /// When the player gains health, the gained health cant give you more health then your maxhealth
        if (newhealth > health)
        {
            health = newhealth;
        }

        /// The player cant have more health then its maxhealth
        if (health > maxhealth)
        {
            health = maxhealth;
        }

        /// The gained health is being added to the health of the player
        if (newhealth < health)
        {
            health -= Math.Max((health - newhealth) / 50 + 0.1f, 0.4f);
        }

        /// The healthbar cant go outside of the ring around it
        if (healthbarwidth2 > healthbarwidth)
        {
            healthbarwidth2 = healthbarwidth;
        }

        /// The the size of the bosses healthbar
        if (isBoss)
        {
            healthbarwidth = (int)((health / maxhealth) * GameEnvironment.WindowSize.X / 1.6);
            healthbarwidth2 = (int)((newhealth / maxhealth) * GameEnvironment.WindowSize.X / 1.6);
            healthbar = new Rectangle((int)positionNow.X + 130, (int)positionNow.Y + 70, healthbarwidth, 40);
            healthbar2 = new Rectangle((int)positionNow.X + 130, (int)positionNow.Y + 70, healthbarwidth2, 40);
        }
        /// The size of the enemyhealthbar
        else
        {
            healthbarwidth = (int)((health / maxhealth) * 200);
            healthbarwidth2 = (int)((newhealth / maxhealth) * 200);
            healthbar = new Rectangle((int)positionNow.X - 30, (int)positionNow.Y - 30, healthbarwidth, 20);
        }
        HP = "HP " + (int)newhealth;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        /// The size of the player's healthbar
        if (isPlayer)
        {
            healthbar = new Rectangle(GameEnvironment.WindowSize.X + (int)Camera.Position.X - GameEnvironment.WindowSize.X / 2 - 300, (int)Camera.Position.Y - GameEnvironment.WindowSize.Y / 2 + 300, healthbarwidth, 20);
            healthbar2 = new Rectangle(GameEnvironment.WindowSize.X + (int)Camera.Position.X - GameEnvironment.WindowSize.X / 2 - 300, (int)Camera.Position.Y - GameEnvironment.WindowSize.Y / 2 + 300, healthbarwidth2, 20);
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/HUD/HealthbarBackground"), new Vector2(GameEnvironment.WindowSize.X - 5 + (int)Camera.Position.X - GameEnvironment.WindowSize.X / 2 - 300, (int)Camera.Position.Y - 5 - GameEnvironment.WindowSize.Y / 2 + 300), Color.White);
        }

        /// Here the healthbars are being drawn
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/HUD/damagehealthbar"), healthbar, Color.White);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/HUD/healthbar"), healthbar2, Color.White);
        if (isBoss)
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), HP, new Vector2(healthbar.X + 450, healthbar.Y), Color.White);
        else
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), HP, new Vector2(healthbar.X + 40, healthbar.Y), Color.White);
    }
}


