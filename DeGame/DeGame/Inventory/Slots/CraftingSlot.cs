using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CraftingSlot : InventorySlot
{
    public CraftingSlot(Vector2 position, Item item, int layer = 0, string id = "InventorySlot") : base(position, item, layer, id)
    {
        
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // Handle input for crafting slot
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition))
        {
            Console.WriteLine("Clicked a crafting interface inventory button!");
        } 
    }
}
