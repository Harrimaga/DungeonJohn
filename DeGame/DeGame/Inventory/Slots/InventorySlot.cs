using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class InventorySlot : SpriteGameObject
{
    public Item item;
    protected Texture2D itemSprite;

    public InventorySlot(Vector2 position, Item item, int layer = 0, string id = "InventorySlot") : base("Sprites/InventorySlots/EmptySlot", layer, id)
    {
        this.position = position;
        this.item = item;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/EmptySlot");
        if (item != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position, Color.White);
        if (item != null)
        {
            DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition)) 
        {
            Player.inventory.equip(item);
        }
    }
    public void ToInventory(Item item)
    {
        Player.inventory.addItemToInventory(item);
    }
   
    public static void DrawItem(Texture2D sprite, Texture2D itemSprite, Vector2 position, GameTime gameTime, SpriteBatch spriteBatch)
    {
        Vector2 itemSpritePosition;
        float scale = CalculateScale(sprite, itemSprite);
        if (itemSprite.Height * scale == sprite.Height && itemSprite.Width * scale == sprite.Width)
        {
            itemSpritePosition = position;
        }
        else if ((int)(itemSprite.Width * scale) == sprite.Width)
        {
            itemSpritePosition = position /*+ new Vector2(0, itemSprite.Height * scale / 2)*/;
        }
        else if ((int)(itemSprite.Height * scale) == sprite.Height)
        {
            itemSpritePosition = position + new Vector2(itemSprite.Width * scale / 2, 0);
        }
        else
        {
            itemSpritePosition = position;
        }
        spriteBatch.Draw(itemSprite, itemSpritePosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
    public static float CalculateScale(Texture2D sprite, Texture2D itemSprite)
    {
        float scale = (float)sprite.Height / itemSprite.Height;
        if (itemSprite.Width * scale > sprite.Width)
        {
            scale = (float)sprite.Width / itemSprite.Height;
        }
        return scale;
    }
}