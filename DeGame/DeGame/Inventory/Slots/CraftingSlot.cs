using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CraftingSlot : SpriteGameObject
{
    Texture2D itemSprite;
    Item item;

    public CraftingSlot(Vector2 position, Item item = null, int layer = 0, string id = "CraftingSlot") : base("Sprites/InventorySlots/CraftingSlot1", layer, id)
    {
        this.position = position;
        this.item = item;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/PassiveSlot");
        if (item != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (item != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position);
        if (item != null)
        {
            InventorySlot.DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }
    }
}
