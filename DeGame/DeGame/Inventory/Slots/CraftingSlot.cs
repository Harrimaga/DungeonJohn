using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CraftingSlot : InventorySlot
{
    bool New, hover = false;
    public CraftingSlot(Vector2 position, Item item,bool New, int layer = 0, string id = "CraftingSlot") : base(position, item, layer, id)
    {
        // Voor een andere sprite dan de standaard
        this.New = New;

        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/CraftingSlot");
    }
    public void AddItem(Item item)
    {
        this.item = item;
        itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
    }
    public override void Update(GameTime gameTime)
    {
        if (item != null)
        {
            sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/CraftingSlotEmpty");
        }
        else
        {
            if (!New)
            {
                sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/CraftingSlot");
            }
            if(New)
            {
                sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/CraftingNewSlot");
            }
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        
        if (hover && item != null)
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), item.itemDescription, position + new Vector2(-60,130), Color.White);
        }
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        // Handle input for crafting slot
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition) && !New)
        {
            if (item != null)
            {
                Player.inventory.addItemToInventory(item);
                item = null;
            }
        }
        if(BoundingBox.Contains(inputHelper.MousePosition))
        {
            hover = true;
        }
        else
        {
            hover = false;
        }
    }
}
