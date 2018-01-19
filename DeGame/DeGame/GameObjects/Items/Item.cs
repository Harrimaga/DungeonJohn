using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Item
{
    public string itemName;
    public string itemDescription;
    public Texture2D up, down, left, right;

    public Item()
    { 
        itemName = "null";
        itemDescription = "null";
        Cost = 100;
        ItemRoomSpawnChance = 0.1;
        ShopSpawnChance = 0.1;

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
    }

    public string Type
    {
        get; set;
    }

    public int Cost
    {
        get; set;
    }

    //Graag tussen de 0 en de 1 ty!!
    public double ItemRoomSpawnChance
    {
        get; set;
    }

    //Zelfde hier!
    public double ShopSpawnChance
    {
        get; set;
    }

    public virtual void equip()
    {
        
    }

    public virtual void unequip()
    {
        
    }

    public override string ToString()
    {
        return itemName;
    }
    public void DrawOnPlayer(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (up != null && down != null && right != null && left != null)
        {
            switch (PlayingState.player.lastUsedspeed)
            {
                case "up":
                    spriteBatch.Draw(up, PlayingState.player.position, Color.White);
                    break;
                case "down":
                    spriteBatch.Draw(down, PlayingState.player.position, Color.White);
                    break;
                case "right":
                    spriteBatch.Draw(right, PlayingState.player.position, Color.White);
                    break;
                case "left":
                    spriteBatch.Draw(left, PlayingState.player.position, Color.White);
                    break;
            }
        }
    }
}