using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class CraftingSlots : GameObjectList
{
    CraftingSlot itemSlot1, itemSlot2;
    new Vector2 position;


    public CraftingSlots(Vector2 position) : base()
    {
        this.position = position;
        itemSlot1 = new CraftingSlot(position, null);
        itemSlot2 = new CraftingSlot(position + new Vector2(212,0),null);

        Add(itemSlot1);
        Add(itemSlot2);
    }

    public override Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            itemSlot1.position = position;
            itemSlot2.position = position + new Vector2(212, 0);
        }
    }
}
