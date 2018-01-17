using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HelmetSlot : Slot
{
    public HelmetSlot(Vector2 position, int layer = 0, string id = "HelmetSlot") : base ("Sprites/InventorySlots/HelmetSlot", layer, id)
    {
        this.position = position;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/HelmetSlot");
        if (Player.inventory.currentHelmet != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + Player.inventory.currentHelmet.itemName);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Player.inventory.currentHelmet != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + Player.inventory.currentHelmet.itemName);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position);
        if (Player.inventory.currentHelmet != null)
        {
            InventorySlot.DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }
    }
}