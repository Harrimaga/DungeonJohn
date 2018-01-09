using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Mirror : Item, IShield
{
    float addeddefence, defencemultiplier;
    public Mirror()
    {
        addeddefence = 1;
        defencemultiplier = 1;
        itemName = "Mirror";
        itemDescription = "Sometimes reduces bullet damage and reflects the projectile";
    }

    public override void equip()
    {
        PlayingState.player.Mirror = true;
    }

    public override void unequip()
    {
        PlayingState.player.Mirror = false;
    }

    public float AddedDefence
    {
        get
        {
            return addeddefence;
        }
    }

    public float DefenceMultiplier
    {
        get
        {
            return defencemultiplier;
        }
    }
}

