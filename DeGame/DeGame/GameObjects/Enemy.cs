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
    int roomlistIndex, A, B, b, a;
    protected Vector2 basevelocity = new Vector2(1, 1);
    public Room room; //?
    HealthBar healthbar;

    public Enemy(Vector2 startPosition, int layer = 0, string id = "Enemy")
    : base("Sprites/BearEnemy", layer, id)
    {
        healthbar = new HealthBar(health, maxhealth, position);
        room = new Room(roomlistIndex, A, B, 0, "Room");
        position = startPosition;
        velocity = basevelocity;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
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
            PlayingState.currentFloor.currentRoom.RemovedGameObjects.Add(this);
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        healthbar.Draw(spriteBatch, position);
    }

    public bool CheckSurround(int x, int y)
    {
        position.X = x;
        position.Y = y;
        if (room.roomarray[x, y] == "Wall");
        {
            return true;
        }
    }
    public virtual void Chase()
    {
        //if (CheckSurround(b + 1, a) == false)
        //{
            if (position.Y + sprite.Height > PlayingState.player.position.Y)
            {
                position.Y -= velocity.Y;
            }
            if (position.Y - sprite.Height < PlayingState.player.position.Y)
            {
                position.Y += velocity.Y;
            }
            if (position.X + sprite.Width > PlayingState.player.position.X)
            {
                position.X -= velocity.X;
            }
            if (position.X - sprite.Width < PlayingState.player.position.X)
            {
                position.X += velocity.X;
            }
        //}
    }
}



