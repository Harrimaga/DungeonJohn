using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CraftingNewSlot : Slot
{
    Item item;

    public CraftingNewSlot(Vector2 position, Item item = null, int layer = 0, string id = "CraftingNewSlot") : base("Sprites/InventorySlots/PassiveSlot", layer, id)
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