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
        Chase();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
        }
    }

