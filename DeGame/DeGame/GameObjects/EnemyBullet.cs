using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EnemyBullet : SpriteGameObject
    //Niewe Code hier en in RangedEnemy;
    //0:30 min;

{
    public EnemyBullet(Vector2 Startpositon, int layer = 0, string id = "EnemyBullet") :  base("Sprites/Random", layer, id)
    {
        position = Startpositon;
        velocity = (PlayingState.player.position - position)/50;
    }
    public override void Update(GameTime gameTime)
    {
        position += velocity;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }
}
