using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

class PauseMenuState : IGameObject
{
    public static Vector2 BasisPosition;
    IGameObject playingState;
    WornItems wornItems;
    Button restartB, mainMenuB;
    List<InventorySlot> inventory, oldInventory;
    bool startup = true;

    public PauseMenuState()
    {
        restartB = new Button(new Vector2(10, 140), "Restart", "Restart", "RestartPressed", true, 1);
        mainMenuB = new Button(new Vector2(10, 240), "MainMenu2", "MainMenu2", "MainMenu2Pressed", true, 1);
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
    }

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        restartB.HandleInput(inputHelper, gameTime);
        mainMenuB.HandleInput(inputHelper, gameTime);
        if (restartB.Pressed)
        {
            PlayingState.currentFloor.ResetFloor();
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }
        if (mainMenuB.Pressed)
        {
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
        }
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

    void startUp()
    {
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

        oldInventory = inventory;
        startup = false;
    }

    public virtual void Update(GameTime gameTime)
    {
        restartB.Update(gameTime);
        mainMenuB.Update(gameTime);
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
        wornItems = PlayingState.hud.wornItems;
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

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        restartB.Draw(gameTime, spriteBatch);
        mainMenuB.Draw(gameTime, spriteBatch);
        playingState.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/PauseMenu/PauseMenu"), BasisPosition);
        wornItems = PlayingState.hud.wornItems;
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
        restartB.Draw(gameTime, spriteBatch);
        mainMenuB.Draw(gameTime, spriteBatch);
    }
}
