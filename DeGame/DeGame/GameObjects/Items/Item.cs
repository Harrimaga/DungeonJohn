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
    }

    public string Type
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