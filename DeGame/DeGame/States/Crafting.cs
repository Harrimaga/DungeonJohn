using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Crafting : IGameObject
{
    List<CraftingInventorySlot> inventory;
    public static CraftingSlots craftingSlots;
    Vector2 BasisPosition;

    public Crafting()
    {
        craftingSlots = new CraftingSlots(new Vector2(GameEnvironment.WindowSize.X - 300 + (Camera.Position.X - GameEnvironment.WindowSize.X / 2), (Camera.Position.Y - GameEnvironment.WindowSize.Y / 2) + 510));
    }

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if(inputHelper.KeyPressed(Keys.Space))
        {
            if (craftingSlots.itemSlot1.item != null)
            {
                Player.inventory.addItemToInventory(craftingSlots.itemSlot1.item);
                craftingSlots.itemSlot1.item = null;
            }

            if (craftingSlots.itemSlot2.item != null)
            {
                Player.inventory.addItemToInventory(craftingSlots.itemSlot2.item);
                craftingSlots.itemSlot2.item = null;
            }
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }

        foreach (CraftingInventorySlot cs in inventory)
        {
            cs.HandleInput(inputHelper, gameTime);
        }

        craftingSlots.HandleInput(inputHelper, gameTime);
    }

    public virtual void Update(GameTime gameTime)
    {
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
        craftingSlots.Update(gameTime);
    }

    public virtual void Reset()
    {
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        GameEnvironment.gameStateManager.GetGameState("Playing").Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/PauseMenu/CraftingMenu"), BasisPosition);
        craftingSlots.Position = BasisPosition + new Vector2(500, 150);
        craftingSlots.Draw(gameTime, spriteBatch);

        inventory = new List<CraftingInventorySlot>();
        for (int i = 0; i < Player.inventory.items.Count; i++)
        {
            Vector2 slotPosition;
            int x, y;
            y = (int)Math.Floor((double)i / 9);
            x = i % 9;
            slotPosition = BasisPosition + new Vector2(500 + x * 74, 450 + y * 74);

            inventory.Add(new CraftingInventorySlot(slotPosition, Player.inventory.items[i]));
        }

        foreach (CraftingInventorySlot slot in inventory)
        {
            slot.Draw(gameTime, spriteBatch);
        }
    }
}
