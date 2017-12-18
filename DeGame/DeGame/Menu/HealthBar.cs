using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HealthBar
{
    float health;
    float maxhealth;
    float newhealth;
    int healthbarwidth = 200;
    bool isPlayer;
    string HP = "N/A";
    Rectangle healthbar;

    public HealthBar(float healthObject, float maxhealthObject, Vector2 position, bool IsPlayer = false)
    {
        health = healthObject;
        maxhealth = maxhealthObject;
        newhealth = health;
        healthbar = new Rectangle((int)position.X - 30, (int)position.Y - 30, healthbarwidth, 20);
        isPlayer = IsPlayer;
    }
    public void Update(GameTime gameTime, float healthUpdate, float maxhealthUpdate, Vector2 positionNow)
    {
        newhealth = healthUpdate;
        if (newhealth < 0)
            newhealth = 0;

        if (newhealth > health)
        {
            healthbar = new Rectangle((int)positionNow.X - 30, (int)positionNow.Y - 30, healthbarwidth, 20);
            health = newhealth;
        }

        if (newhealth < health)
        {
            health--;
        }
        healthbarwidth = (int)((health / maxhealth) * 200);
        if (isPlayer)
            healthbar = new Rectangle(GameEnvironment.WindowSize.X + (int)Camera.Position.X - GameEnvironment.WindowSize.X / 2 - 300, (int)Camera.Position.Y - GameEnvironment.WindowSize.Y / 2 + 300, healthbarwidth, 20);
        else
            healthbar = new Rectangle((int)positionNow.X - 30, (int)positionNow.Y - 30, healthbarwidth, 20);
        HP = "HP " + newhealth;
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/healthbar"), healthbar, Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), HP, new Vector2(healthbar.X + 40, healthbar.Y), Color.White);
    }
}


