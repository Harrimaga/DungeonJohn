using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class InventorySlot : SpriteGameObject
{
    Item item;
    Texture2D itemSprite;

    public InventorySlot(Vector2 position, Item item, int layer = 0, string id = "InventorySlot") : base("Sprites/InventorySlots/EmptySlot", layer, id)
    {
        this.position = position;
        this.item = item;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/EmptySlot");
        itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position, Color.White);
        float scale = (float)(sprite.Height * sprite.Width) / (float)(itemSprite.Height * itemSprite.Width);

        Vector2 itemSpritePosition;
        if (itemSprite.Height * scale == sprite.Height)
        {
            itemSpritePosition = position + new Vector2(itemSprite.Width * scale / 2, 0);
        }
        else if(itemSprite.Width * scale == sprite.Width)
        {
            itemSpritePosition = position + new Vector2(0, itemSprite.Height * scale / 2);
        }
        else
        {
            itemSpritePosition = position;
        }

        spriteBatch.Draw(itemSprite, itemSpritePosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}