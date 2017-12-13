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
    Rectangle healthbar;
    public HealthBar(float healthObject, float maxhealthObject, Vector2 position)
    {
       health = healthObject;
       maxhealth = maxhealthObject;
       newhealth = health;
       healthbar = new Rectangle((int)position.X -30, (int)position.Y- 30, healthbarwidth, 20);
    }
    public void Update(GameTime gameTime, float healthUpdate, float maxhealthUpdate, Vector2 positionNow)
    {
        newhealth = healthUpdate;

        if (newhealth > health)
        {
            health++;
        }

        if (newhealth < health)
        {
            health--;
        }
        healthbarwidth = (int)(((float)health / maxhealth) * 200);
        healthbar = new Rectangle((int)positionNow.X - 30, (int)positionNow.Y - 30, healthbarwidth, 20);
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/healthbar"), healthbar, Color.White);
    }
}
    

