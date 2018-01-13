using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Crafting : IGameObject
{
    List<InventorySlot> inventory;
    Vector2 BasisPosition;
    public Crafting()
    {
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        if(inputHelper.IsKeyDown(Keys.Space))
        {
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        GameEnvironment.gameStateManager.GetGameState("Playing").Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/PauseMenu/CraftingMenu"), BasisPosition);

        inventory = new List<InventorySlot>();
        for (int i = 0; i < Player.inventory.items.Count; i++)
        {
            Vector2 slotPosition;
            int x, y;
            y = (int)Math.Floor((double)i / 9);
            x = i % 9;
            slotPosition = BasisPosition + new Vector2(500 + x * 74, 450 + y * 74);

            inventory.Add(new InventorySlot(slotPosition, Player.inventory.items[i]));
        }

        foreach (InventorySlot slot in inventory)
        {
            slot.Draw(gameTime, spriteBatch);
        }
    }
    public virtual void Update(GameTime gameTime)
    {
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    }
    public virtual void Reset()
    {
    }

}
