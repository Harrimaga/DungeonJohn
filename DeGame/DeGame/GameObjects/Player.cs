using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Player : SpriteGameObject
{
    protected float health;
    protected float maxhealth;
    protected float attack;
    protected float attackspeed;
    protected float range;
    public Vector2 position;

    public Player()
    {
        position = new Vector2();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.W))
        {
            position.Y -= 5;
        }
        if (inputHelper.IsKeyDown(Keys.S))
        {
            position.Y += 5;
        }
        if (inputHelper.IsKeyDown(Keys.A))
        {
            position.X -= 5;
        }
        if (inputHelper.IsKeyDown(Keys.D))
        {
            position.X += 5;
        }

    }
}


