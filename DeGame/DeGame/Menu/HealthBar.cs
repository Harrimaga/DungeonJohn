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
    public float health2;
    float maxhealth;
    float newhealth;
    int healthbarwidth = 200, healthbarwidth2 = 200;
    bool isPlayer;
    string HP = "N/A";
    Rectangle healthbar, healthbar2;

    public HealthBar(float healthObject, float maxhealthObject, Vector2 position, bool IsPlayer = false)
    {
        health = healthObject;
        maxhealth = maxhealthObject;
        newhealth = health;
        isPlayer = IsPlayer;
    }
    public void Update(GameTime gameTime, float healthUpdate, float maxhealthUpdate, Vector2 positionNow)
    {
        newhealth = healthUpdate;
        /*if (newhealth == 100)
            health2 = 100;*/
        if (newhealth < 0)
            newhealth = 0;
        if (newhealth > health)
        {
            health = newhealth;
        }

        if (newhealth < health)
        {
            health -= 0.4f;
            //health2 = newhealth;
        }
        healthbarwidth = (int)((health / maxhealthUpdate) * 200);
        healthbarwidth2 = (int)((newhealth / maxhealthUpdate) * 200);
        healthbar = new Rectangle((int)positionNow.X - 30, (int)positionNow.Y - 30, healthbarwidth, 20);

        if (isPlayer)
        {
            healthbar = new Rectangle(GameEnvironment.WindowSize.X + (int)Camera.Position.X - GameEnvironment.WindowSize.X / 2 - 300, (int)Camera.Position.Y - GameEnvironment.WindowSize.Y / 2 + 300, healthbarwidth, 20);
            healthbar2 = new Rectangle(GameEnvironment.WindowSize.X + (int)Camera.Position.X - GameEnvironment.WindowSize.X / 2 - 300, (int)Camera.Position.Y - GameEnvironment.WindowSize.Y / 2 + 300, healthbarwidth2, 20);
        }
        else
        {
            healthbar = new Rectangle((int)positionNow.X - 30, (int)positionNow.Y - 30, healthbarwidth, 20);
        }
        HP = "HP " + newhealth;
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/HealthbarBackground"), new Vector2(GameEnvironment.WindowSize.X - 5 + (int)Camera.Position.X - GameEnvironment.WindowSize.X / 2 - 300, (int)Camera.Position.Y - 5 - GameEnvironment.WindowSize.Y / 2 + 300) , Color.White);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/damagehealthbar"), healthbar, Color.White);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/healthbar"), healthbar2, Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), HP, new Vector2(healthbar.X + 40, healthbar.Y), Color.White);
    }
}


