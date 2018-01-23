using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Superclass for all different kinds of slots
/// Subclass documentation is found in WeaponSlot.cs, InventorySlot.cs and CraftingSlot.cs
/// </summary>
public class Slot : SpriteGameObject
{
    public Texture2D itemSprite;
    public Item item;
    public bool text = false, hover;

    public Slot(string name, int layer = 0, string id = "slot") : base(name, layer, id)
    {
        
    }

    /// <summary>
    /// Update method should be overridden in the subclasses
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        
    }

    /// <summary>
    /// Draws the item name and description if you hover over the item in the pause menu
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (hover && item != null && GameEnvironment.gameStateManager.currentGameState == GameEnvironment.gameStateManager.GetGameState("PauseMenu")) 
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), item.itemName, PauseMenuState.BasisPosition + new Vector2(800,150), Color.White);
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), item.itemDescription, PauseMenuState.BasisPosition + new Vector2(800, 200), Color.White);
        }
    }

    /// <summary>
    /// Handles the unequipping of items, and checks if the mouse is hovering over the slot
    /// </summary>
    /// <param name="inputHelper"></param>
    /// <param name="gameTime"></param>
    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition))
        {
            try
            {
                switch (id)
                {
                    // Finds out what kind of slot this is and
                    // calls the unequip (changes player stats)
                    // and unequips the item
                    case "ArmourSlot":
                        Player.inventory.currentArmour.unequip();
                        Player.inventory.addItemToInventory(Player.inventory.currentArmour);
                        Player.inventory.currentArmour = null;
                        break;
                    case "BootsSlot":
                        Player.inventory.currentBoots.unequip();
                        Player.inventory.addItemToInventory(Player.inventory.currentBoots);
                        Player.inventory.currentBoots = null;
                        break;
                    case "HelmetSlot":
                        Player.inventory.currentHelmet.unequip();
                        Player.inventory.addItemToInventory(Player.inventory.currentHelmet);
                        Player.inventory.currentHelmet = null;
                        break;
                    case "PassiveSlot":
                        PassiveSlot slot = (PassiveSlot)this;
                        Player.inventory.currentPassives[slot.slotno - 1].unequip();
                        Player.inventory.addItemToInventory(Player.inventory.currentPassives[slot.slotno - 1]);
                        Player.inventory.currentPassives[slot.slotno - 1] = null;
                        break;
                    case "ShieldSlot":
                        Player.inventory.currentShield.unequip();
                        Player.inventory.addItemToInventory(Player.inventory.currentShield);
                        Player.inventory.currentShield = null;
                        break;
                    case "WeaponSlot":
                        Player.inventory.currentWeapon.unequip();
                        Player.inventory.addItemToInventory(Player.inventory.currentWeapon);
                        Player.inventory.currentWeapon = null;
                        break;
                }
            }
            catch(NullReferenceException nre)
            {

            }
        }
        if (BoundingBox.Contains(inputHelper.MousePosition))
        {
            hover = true;
        }
        else
        {
            hover = false;
        }
    }
}
