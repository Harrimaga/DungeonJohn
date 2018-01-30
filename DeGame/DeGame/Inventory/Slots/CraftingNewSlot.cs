using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CraftingNewSlot : InventorySlot
{
    public CraftingNewSlot(Vector2 position, Item item = null, int layer = 0, string id = "CraftingNewSlot") : base(position, item, layer, id)
    {
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
}