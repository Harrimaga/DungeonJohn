using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CraftingNewSlot : InventorySlot
{

    public CraftingNewSlot(Vector2 position, Item item = null, int layer = 0, string id = "CraftingNewSlot") : base("Sprites/InventorySlots/PassiveSlot", layer, id)
    {

    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
}