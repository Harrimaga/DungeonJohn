using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class BossBullet : E_Bullet
{
    public BossBullet(Vector2 Startposition, int layer = 0, string id = "BossBullet") : base(Startposition, 0, "BossBullet") 
    {
        position = Startposition;
        velocity = new Vector2(10, 0);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position += velocity;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BossBullet"), position);
    }
}
