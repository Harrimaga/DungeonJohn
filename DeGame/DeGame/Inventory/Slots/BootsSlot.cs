using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BootsSlot : Slot
{
    public BootsSlot(Vector2 position, int layer = 0, string id = "BootsSlot") : base ("Sprites/InventorySlots/BootsSlot", layer, id)
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
        if (Player.inventory.currentBoots != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + Player.inventory.currentBoots.itemName);
        }

        item = Player.inventory.currentBoots;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(sprite, position);
        if (Player.inventory.currentBoots != null)
        {
            InventorySlot.DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }
    }
}

