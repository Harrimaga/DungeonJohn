using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Boss :  SpriteGameObject
{
    protected float health = 100;
    protected float maxhealth = 100;
    //protected float attack;
    //protected float attackspeed;
    //protected float range;
    protected Vector2 basevelocity = new Vector2((float)0.5, (float)0.5);
    public SpriteEffects Effects;
    HealthBar healthbar;

    public Boss(Vector2 startPosition, int layer = 0, string id = "Boss") : base("Sprites/Boss", layer, id)
    {
        position = startPosition;
        healthbar = new HealthBar(health, maxhealth, position);
    }

    public override void Update(GameTime gameTime)
    {
        healthbar.Update(gameTime, health, maxhealth, position);

        List<GameObject> RemoveBullets = new List<GameObject>();

        foreach (Bullet bullet in PlayingState.player.bullets.Children)
            if (CollidesWith(bullet))
            {
                health -= PlayingState.player.attack;
                RemoveBullets.Add(bullet);
            }


        foreach (Bullet bullet in RemoveBullets)
            PlayingState.player.bullets.Remove(bullet);

        if (health <= 0)
        {
            GameObjectList.RemovedObjects.Add(this);
        }

        RemoveBullets.Clear();
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch, position);
    }
}
