﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BootsSlot : SpriteGameObject
{
    Texture2D sprite, itemSprite;
    public Vector2 position;

    public BootsSlot(Vector2 position, int layer = 0, string id = "WeaponSlot") : base ("Sprites/InventorySlots/BootsSlot", layer, id)
    {
        this.position = position;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/BootsSlot");
        if (Player.inventory.currentBoots != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + Player.inventory.currentBoots.itemName);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position);
        if (Player.inventory.currentBoots != null)
        {
            InventorySlot.DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }
    }
}

