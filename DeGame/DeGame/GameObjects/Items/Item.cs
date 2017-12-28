using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Item : SpriteGameObject
{
    Vector2 position;

    public Item(Vector2 position, int layer = 0, string id = "item") : base("Sprites/OldRock", layer, id)
    {
        this.position = position;
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
}

