using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


//TODO: Bullets;
//TODO: Stay at a distance; Range;
//TODO: Sprite;
//TODO: Damage
//TODO: Health
class RangedEnemy : Enemy
{
    public RangedEnemy(Vector2 startPosition, int layer = 0, string id = "Enemy") : base(startPosition, layer, id)
    {

    }

    public override void Chase()
    {        
        if(PlayingState.player.position.X + 100 < position.X || PlayingState.player.position.X - 100 > position.X ||
            PlayingState.player.position.Y + 100 < position.Y || PlayingState.player.position.Y - 100 > position.Y)
            {
                base.Chase();
            }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Die == false)
        {
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
        }
    }
    public override void Update(GameTime gameTime)
    {
        Chase();
        base.Update(gameTime);
    }
}
