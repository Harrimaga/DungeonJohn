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

    public Item()
    { 
        itemName = "null";
        itemDescription = "null";
        Cost = 100;
        ItemRoomSpawnChance = 0.1;
        ShopSpawnChance = 0.1;
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
        Player.inventory.addItemToInventory(this);
    }

    public override string ToString()
    {
        return itemName;
    }
    public void DrawOnPlayer(GameTime gameTime, SpriteBatch spriteBatch)
    {

        //switch (PlayingState.player.lastUsedspeed)
        //{
        //case "up":
        //    spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/" + itemName + "up"), PlayingState.player.position, Color.White);
        //    break;
        //case "down":
        //    spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/" + itemName + "down"), PlayingState.player.position, Color.White);
        //    break;
        //case "right":
        //    spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/" + itemName + "right"), PlayingState.player.position, Color.White);
        //    break;
        //case "left":
        //    spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/" + itemName + "left"), PlayingState.player.position, Color.White);
        //    break;
        //}
    }
}