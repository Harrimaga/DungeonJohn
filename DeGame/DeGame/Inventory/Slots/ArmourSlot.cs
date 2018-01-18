﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ArmourSlot : Slot
{

    public ArmourSlot(Vector2 position, int layer = 0, string id = "ArmourSlot") : base ("Sprites/InventorySlots/ArmourSlot", layer, id)
    {
        this.position = position;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/ArmourSlot");
        if (Player.inventory.currentArmour != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + Player.inventory.currentArmour.itemName);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Player.inventory.currentArmour != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + Player.inventory.currentArmour.itemName);
        }

        item = Player.inventory.currentArmour;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(sprite, position);
        if (Player.inventory.currentArmour != null)
        {
            InventorySlot.DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }
    }
}

