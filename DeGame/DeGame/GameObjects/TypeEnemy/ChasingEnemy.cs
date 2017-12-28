using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ChasingEnemy : Enemy
{
    public ChasingEnemy(Vector2 startPosition, int layer = 0, string id = "Enemy") : base(startPosition, layer, id)
    {
    }

    public override void Update(GameTime gameTime)
    {
         base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            counter--;
            if (counter == 0)
            {
               // velocity = Vector2.Zero;
                PlayingState.player.health -= 0;
                counter = 100;
            }
        }
        //if (!CollidesWith(PlayingState.player))
        //{
        //    velocity = basevelocity;
        //}

        Chase();
       
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


   