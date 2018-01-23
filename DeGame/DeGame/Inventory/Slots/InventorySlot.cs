using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Class for items that are in the inventory and not equipped
/// </summary>
public class InventorySlot : Slot
{
    public InventorySlot(Vector2 position, Item item, int layer = 0, string id = "InventorySlot") : base("Sprites/InventorySlots/EmptySlot", layer, id)
    {
        this.position = position;
        this.item = item;

        // Gets the slot sprite
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/EmptySlot");

        // If there's and item in the slot, it gets the sprite
        if (item != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
        }
    }

    /// <summary>
    /// For normal inventory slots, this draw is only called when an item is actually
    /// present in the slot. But the check is there for consistency
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {     
        // Draws the slot
        spriteBatch.Draw(sprite, position, Color.White);

        // If an item is in the slot, draws it
        if (item != null)
        {
            DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }

        // Draws tooltips if hovered over
        base.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Handles the equipping of items, and checks for hovering
    /// </summary>
    /// <param name="inputHelper"></param>
    /// <param name="gameTime"></param>
    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        // If clicked on, equip the item in it's approperiate slot
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition)) 
        {
            Player.inventory.equip(item);
        }
        if (BoundingBox.Contains(inputHelper.MousePosition))
        {
            hover = true;
        }
        else
        {
            hover = false;
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    /// <summary>
    /// Adds an item to the player's inventory
    /// </summary>
    /// <param name="item"></param>
    public void ToInventory(Item item)
    {
        Player.inventory.addItemToInventory(item);
    }
   

    /// <summary>
    /// Draws an item scaled to a specified slot
    /// </summary>
    /// <param name="sprite">The slot sprite</param>
    /// <param name="itemSprite">The item sprite</param>
    /// <param name="position">The position of the slot</param>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public static void DrawItem(Texture2D sprite, Texture2D itemSprite, Vector2 position, GameTime gameTime, SpriteBatch spriteBatch)
    {
        Vector2 itemSpritePosition;

        // Calculate the scale of the item, so that it fits in the slot
        float scale = CalculateScale(sprite, itemSprite);

        // Sets the correct position for the sprite in the slot
        if (itemSprite.Height * scale == sprite.Height && itemSprite.Width * scale == sprite.Width)
        {
            itemSpritePosition = position;
        }
        else if ((int)(itemSprite.Width * scale) == sprite.Width)
        {
            itemSpritePosition = position;
        }
        else if ((int)(itemSprite.Height * scale) == sprite.Height)
        {
            itemSpritePosition = position + new Vector2(itemSprite.Width * scale / 2, 0);
        }
        else
        {
            itemSpritePosition = position;
        }

        // Draws the item
        spriteBatch.Draw(itemSprite, itemSpritePosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    /// <summary>
    /// Calculates the scale of a sprite, so that it fits in a specified other sprite
    /// </summary>
    /// <param name="sprite">Sprite to be draw in</param>
    /// <param name="itemSprite">Sprite to be drawn in the other sprite</param>
    /// <returns></returns>
    public static float CalculateScale(Texture2D sprite, Texture2D itemSprite)
    {
        // Set the scale to the height of the main sprite / the height of the item sprite
        float scale = (float)sprite.Height / itemSprite.Height;

        // If this scale doesn't hold, set the scale to the width of the main sprite / the width of the item sprite
        // This should always return a correct scale, as either a sprite has a bigger h:w or w:h ratio
        if (itemSprite.Width * scale > sprite.Width)
        {
            scale = (float)sprite.Width / itemSprite.Width;
        }
        return scale;
    }
}