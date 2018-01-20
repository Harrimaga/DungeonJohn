using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CraftingInventorySlot : InventorySlot
{
    public CraftingInventorySlot(Vector2 position, Item item, int layer = 0, string id = "CraftingInventorySlot") : base(position, item, layer, id)
    {
        
    }

    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition))
        {
            CraftingSlot open = Crafting.craftingSlots.CheckForEmptySlot();

            if (open != null)
            {
                open.item = item;
                Crafting.craftingSlots.FillSlot(open, item);
                Player.inventory.removeItemFromInventory(item);
            }
            else
            {
                Player.inventory.addItemToInventory(Crafting.craftingSlots.itemSlot1.item);
                Crafting.craftingSlots.FillSlot(Crafting.craftingSlots.itemSlot1,item);
                Player.inventory.removeItemFromInventory(item);
            }
        } 
    }
}
