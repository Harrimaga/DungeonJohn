using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Enemy : SpriteGameObject
{
    protected float health = 100;
    protected float maxhealth = 100;
    protected float attack;
    protected float attackspeed;
    protected float range;
    int roomlistIndex, A, B, b, a, o;
    protected Vector2 basevelocity = new Vector2(1, 1);
    public Room room; //?
    Texture2D playersprite;
    HealthBar healthbar;

    public Enemy(Vector2 startPosition, int layer = 0, string id = "Enemy")
    : base("Sprites/BearEnemy", layer, id)
    {
        healthbar = new HealthBar(health, maxhealth, position);
        room = new Room(roomlistIndex, A, B, 0, "Room");
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
        position = startPosition;
        velocity = basevelocity;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        CheckSurround();
        if (CollidesWith(PlayingState.player))
        {
            velocity = Vector2.Zero;
            PlayingState.player.health -= 1;
        }
        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }

        List<GameObject> RemoveBullets = new List<GameObject>();

        foreach (Bullet bullet in PlayingState.player.bullets.Children)
        {
            if (CollidesWith(bullet))
            {
                health -= 20;
                RemoveBullets.Add(bullet);
            }
        }

        foreach (Bullet bullet in RemoveBullets)
        {
            PlayingState.player.bullets.Remove(bullet);
        }

        RemoveBullets.Clear();

        healthbar.Update(gameTime, health, maxhealth, position);
        if (health <= 0)
        {
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        healthbar.Draw(spriteBatch, position);
    }

    public void  CheckSurround()
    {
        foreach (Rock rock in Room.rocks.Children)
            if (CollidesWith(rock))
            {
               // Die = true;
                velocity = new Vector2(0, 0);
            }
           
       

   
    }

    public virtual void Chase()
    {
       
            if (position.Y + playersprite.Height > PlayingState.player.position.Y + 1)
            {
                position.Y -= velocity.Y;
            }
            if (position.Y - playersprite.Height < PlayingState.player.position.Y - 1)
            {
                position.Y += velocity.Y;
            }
            if (position.X + playersprite.Width > PlayingState.player.position.X + 1)
            {
                position.X -= velocity.X;
            }
            if (position.X - playersprite.Width < PlayingState.player.position.X - 1)
            {
                position.X += velocity.X;
            }
    }
}



