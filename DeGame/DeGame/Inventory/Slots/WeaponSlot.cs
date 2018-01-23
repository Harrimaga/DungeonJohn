using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeaponSlot : Slot
{
    public WeaponSlot(Vector2 position, int layer = 0, string id = "WeaponSlot") : base ("Sprites/InventorySlots/WeaponSlot", layer, id)
    {
        this.position = position;

        // Gets the sprite of the slot
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/WeaponSlot");

        // If there's an item in the slot, it gets the sprite for that item
        if (Player.inventory.currentWeapon != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + Player.inventory.currentWeapon.itemName);
        }
    }

    public override void Update(GameTime gameTime)
    {
        // Resets the itemsprite, in case the item was swapped
        if (Player.inventory.currentWeapon != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + Player.inventory.currentWeapon.itemName);
        }

        item = Player.inventory.currentWeapon;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Draws the tooltips
        base.Draw(gameTime, spriteBatch);

        // Draws the slot
        spriteBatch.Draw(sprite, position);

        // If an item is in the slot, draw it with scaling based on the dimensions of the slot itself
        if (Player.inventory.currentWeapon != null)
        {
            InventorySlot.DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }
        
    }
}

