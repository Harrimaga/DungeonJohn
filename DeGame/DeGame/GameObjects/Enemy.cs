using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Enemy : SpriteGameObject
{
    Player player;
    protected float health;
    protected float maxhealth;
    protected float attack;
    protected float attackspeed;
    protected float range;
    protected Vector2 position;

    public Enemy(int layer = 0, string id = "bullet")
    : base("Sprites/BearEnemy", layer, id)
    {
        position = new Vector2(100, 50);
        player = new Player();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(position.Y > player.position.Y)
        {
            position.Y--;
        }
        if (position.Y < player.Position.Y)
        {
            position.Y++;
        }
        if (position.X > player.position.X)
        {
            position.X--;
        }
        if (position.X < player.position.X)
        {
            position.X++;
        }
        player.Update(gameTime);
    }
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
    }
}



