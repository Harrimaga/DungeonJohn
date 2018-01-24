using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PauseMenuState : IGameObject
{
    public static Vector2 BasisPosition;
    public IGameObject playingState;
    WornItems wornItems;
    List<InventorySlot> inventory, oldInventory;
    bool startup = true;

    public PauseMenuState()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
    }

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (inputHelper.KeyPressed(Keys.P) || inputHelper.ButtonPressed(Buttons.Start))
        {
            startup = true;
            GameEnvironment.gameStateManager.SwitchTo("Playing"); 
        }

        wornItems.HandleInput(inputHelper, gameTime);
        try
        {
            foreach (InventorySlot slot in oldInventory)
            {
                slot.HandleInput(inputHelper, gameTime);
            }
        }
        catch (NullReferenceException nre)
        {
            Console.WriteLine("Can't find inventory in PauseMenuState, error code: 3225716");
        }
    }

    public void startUp()
    {
        inventory = new List<InventorySlot>();
        for (int i = 0; i < Player.inventory.items.Count; i++)
        {
            Vector2 slotPosition;
            int x, y;
            y = (int)Math.Floor((double)i / 9);
            x = i % 9;
            //slotPosition = wornItems.position + new Vector2(0, 200) + new Vector2(x * 74, y * 74);
            slotPosition = BasisPosition + new Vector2(500 + x * 74, 450 + y * 74);

            inventory.Add(new InventorySlot(slotPosition, Player.inventory.items[i]));
        }

        oldInventory = inventory;

        startup = false;
    }

    public virtual void Update(GameTime gameTime)
    {
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
        wornItems = PlayingState.currentFloor.wornItems;
        wornItems.Update(gameTime);

        try
        {
            foreach (InventorySlot slot in oldInventory)
            {
                slot.Update(gameTime);
            }
        }
        catch (NullReferenceException nre)
        {
            Console.WriteLine("Inventory not found in PauseMenuState, error code: 3697589");
        }

        PlayingState.player.Update(gameTime);
    }
    public virtual void Reset()
    {
    }


    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/PauseMenu/PauseMenu"), BasisPosition);
        wornItems = PlayingState.currentFloor.wornItems;
        wornItems.Position = BasisPosition + new Vector2(500, 150);
        wornItems.Draw(gameTime, spriteBatch);

        if (startup)
        {
            startUp();
        }

        inventory = new List<InventorySlot>();
        for (int i = 0; i < Player.inventory.items.Count; i++)
        {
            Vector2 slotPosition;
            int x, y;
            y = (int)Math.Floor((double)i / 9);
            x = i % 9;
            //slotPosition = wornItems.position + new Vector2(0, 200) + new Vector2(x * 74, y * 74);
            slotPosition = BasisPosition + new Vector2(500 + x * 74, 450 + y * 74);

            inventory.Add(new InventorySlot(slotPosition, Player.inventory.items[i]));
        }

        if (!checkInventories(inventory, oldInventory))
        {
            oldInventory = inventory;
        }

        foreach (InventorySlot slot in oldInventory)
        {
            slot.Draw(gameTime, spriteBatch);
        }
    }

    bool checkInventories(List<InventorySlot> inventory, List<InventorySlot> oldInventory)
    {
        if (inventory.Count != oldInventory.Count)
        {
            return false;
        }

        for (int i = 0; i < oldInventory.Count; i++)
        {
            if (inventory[i].item != oldInventory[i].item)
            {
                return false;
            }
        }

        return true;
    }
}
