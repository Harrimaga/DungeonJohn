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
    public HealthBar(float healthObject, float maxhealthObject)
    {
       health = healthObject;
       maxhealth = maxhealthObject;
       newhealth = health;
       healthbar = new Rectangle(20, 10, healthbarwidth, 20);
    }
    public void Update(GameTime gameTime, float healthUpdate, float maxhealthUpdate)
    {
        newhealth = healthUpdate;

        if (newhealth > health)
        {
            health++;
            healthbar = new Rectangle(20, 10, healthbarwidth, 20);
        }

        if (newhealth < health)
        {
            health--;
            healthbar = new Rectangle(20, 10, healthbarwidth, 20);
        }
        healthbarwidth = (int)(((float)health / maxhealth) * 200);
    }
    public void Draw(SpriteBatch spriteBatch/*, Vector2 position*/)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/healthbar"), healthbar, Color.White);
    }
}
    

