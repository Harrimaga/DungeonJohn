using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Crafting : IGameObject
{
    //inventory item list
    List<CraftingInventorySlot> inventory;
    public static CraftingSlots craftingSlots;
    //position to know where everything is
    Vector2 BasisPosition;

    public Crafting()
    {
        //new instance foor craftingslot
        craftingSlots = new CraftingSlots(new Vector2(GameEnvironment.WindowSize.X - 300 + (Camera.Position.X - GameEnvironment.WindowSize.X / 2), (Camera.Position.Y - GameEnvironment.WindowSize.Y / 2) + 510));
    }

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        //leaving crafting state
        if(inputHelper.KeyPressed(Keys.Space))
        {
            //if you leave it will return the items you left in the crafting slots
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
        //gives handleinput for each inventory slot
        foreach (CraftingInventorySlot cs in inventory)
        {
            cs.HandleInput(inputHelper, gameTime);
        }
        //gives handleinput for craftingslots
        craftingSlots.HandleInput(inputHelper, gameTime);
    }

    public virtual void Update(GameTime gameTime)
    {
        //update position
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
        craftingSlots.Update(gameTime);
    }

    public virtual void Reset()
    {
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //draw basis stuff
        GameEnvironment.gameStateManager.GetGameState("Playing").Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/PauseMenu/CraftingMenu"), BasisPosition);
        craftingSlots.Position = BasisPosition + new Vector2(500, 150);
        craftingSlots.Draw(gameTime, spriteBatch);

        inventory = new List<CraftingInventorySlot>();
        //draws the inventory in the crafting state
        for (int i = 0; i < Player.inventory.items.Count; i++)
        {
            Vector2 slotPosition;
            int x, y;
            y = (int)Math.Floor((double)i / 9);
            x = i % 9;
            slotPosition = BasisPosition + new Vector2(500 + x * 74, 450 + y * 74);

            inventory.Add(new CraftingInventorySlot(slotPosition, Player.inventory.items[i]));
        }

        // draw tge crafting slots
        foreach (CraftingInventorySlot slot in inventory)
        {
            slot.Draw(gameTime, spriteBatch);
        }
        //draw leaving tekst
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Press spacebar to leave crafting", BasisPosition + new Vector2(700, 850), Color.White);
    }
}
