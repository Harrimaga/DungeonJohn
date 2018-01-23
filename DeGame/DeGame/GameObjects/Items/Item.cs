using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Superclass for items
/// Subclasses with documentation are: StandardBow.cs
/// </summary>
public class Item
{
    public string itemName;
    public string itemDescription;
    public Texture2D up, down, left, right;
    public bool tried = false;

    public Item()
    { 
        itemName = "null";
        itemDescription = "null";
        Cost = 100;
        ItemRoomSpawnChance = 0.1;
        ShopSpawnChance = 0.1;
    }

    /// <summary>
    /// Gets or sets the type of an item
    /// (Armour, Helmet, Weapon, Shield, Passive, Boots)
    /// </summary>
    public string Type
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the cost for an item
    /// Cost is used in the shops
    /// </summary>
    public int Cost
    {
        get; set;
    }

    /// <summary>
    /// The chance for this item to spawn in an itemroom
    /// Currently unused
    /// </summary>
    public double ItemRoomSpawnChance
    {
        get; set;
    }

    /// <summary>
    /// The chance for this item to spawn in a shop
    /// Currently unused
    /// </summary>
    public double ShopSpawnChance
    {
        get; set;
    }

    /// <summary>
    /// Is called when the item is equipped
    /// Handles changing the player's stats
    /// </summary>
    public virtual void equip()
    {
        
    }

    /// <summary>
    /// Is called when the item is unequipped
    /// Reverts the stat change this item gave
    /// </summary>
    public virtual void unequip()
    {
        
    }

    /// <summary>
    /// Returns the name of the item
    /// </summary>
    /// <returns>The item's name</returns>
    public override string ToString()
    {
        return itemName;
    }

    /// <summary>
    /// Draws the item onto the player sprite
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public void DrawOnPlayer(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Sets the 4 different sprites
        if (!tried)
        {
            try
            {
                up = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + itemName + "Up");
                down = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + itemName + "Down");
                right = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + itemName + "Right");
                left = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + itemName + "Left");
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error, are you missing a sprite? Error code: 321");
            }
            tried = true;
        }

        // Draws the correct sprite on the player, based on the direction the player is facing
        // (or rather, the direction they were last moving in)
        switch (PlayingState.player.lastUsedspeed)
        {
            case "Up":
                if (up != null)
                    spriteBatch.Draw(up, PlayingState.player.position, Color.White);
                break;
            case "Down":
                if (down != null)
                    spriteBatch.Draw(down, PlayingState.player.position, Color.White);
                break;
            case "Left":
                if (left != null)
                    spriteBatch.Draw(left, PlayingState.player.position, Color.White);
                break;
            case "Right":
                if (right != null)
                    spriteBatch.Draw(right, PlayingState.player.position, Color.White);
                break;
        }
    }
}