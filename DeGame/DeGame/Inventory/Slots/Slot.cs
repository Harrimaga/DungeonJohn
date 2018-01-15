using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Slot : SpriteGameObject
{
    public Texture2D itemSprite;
    public Item item;
    public bool text = false;

    public Slot(string name, int layer = 0, string id = "slot") : base(name, layer, id)
    {
    }

    public override void Update(GameTime gameTime)
    {
        
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(text)
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), item.itemName, position, Color.White);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (BoundingBox.Contains(inputHelper.MousePosition))
        {
            text = true;
        }
        else
        {
            text = false;
        }
    }
}
