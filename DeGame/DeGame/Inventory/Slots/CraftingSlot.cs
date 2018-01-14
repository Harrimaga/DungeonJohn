﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CraftingSlot : InventorySlot
{
    public CraftingSlot(Vector2 position, Item item, int layer = 0, string id = "CraftingSlot") : base(position, item, layer, id)
    {
        // Voor een andere sprite dan de standaard
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/CraftingSlot");
    }
    void AddItem(Item item)
    {
        this.item = item;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        // Handle input for crafting slot
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition))
        {
            AddItem(new HardHelmet());
            Console.WriteLine("Clicked a craftingslot button!");
        }
    }
}
