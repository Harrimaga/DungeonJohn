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

    }

    public override string ToString()
    {
        return itemName;
    }
}