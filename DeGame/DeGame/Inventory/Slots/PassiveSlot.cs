using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PassiveSlot : Slot
{
    public int slotno;

    public PassiveSlot(Vector2 position, int slotno, Item item = null, int layer = 0, string id = "PassiveSlot") : base ("Sprites/InventorySlots/PassiveSlot", layer, id)
    {
        this.position = position;
        this.item = item;
        this.slotno = slotno;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/PassiveSlot");
        if (item != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (item != null)
        {
            itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position);
        if (item != null)
        {
            InventorySlot.DrawItem(sprite, itemSprite, position, gameTime, spriteBatch);
        }
        base.Draw(gameTime, spriteBatch);
    }
}

