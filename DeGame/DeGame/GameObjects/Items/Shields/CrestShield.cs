using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class CrestShield : Item, IShield
{
    float addeddefence, defencemultiplier;

    public CrestShield()
    {
        Cost = 7;
        addeddefence = 1;
        defencemultiplier = 1;
        itemName = "CrestShield";
        itemDescription = "Ocassionally blocks a bullet. Alse gives you the urge to roll more often...";
        Type = "shield";
    }

    public override void equip()
    {
        PlayingState.player.CrestShield = true;
    }

    public override void unequip()
    {
        PlayingState.player.CrestShield = false;
        base.unequip();
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

