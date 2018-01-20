using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MageJacket : Item, IArmour
{
    public MageJacket()
    {
        itemName = "MageJacket";
        itemDescription = "This jacket was worn by a old mage making you move really faster";
        Type = "armour";
    }

    public override void equip()
    {
        PlayingState.player.extraspeed += 2;
    }
    public override void unequip()
    {
        PlayingState.player.extraspeed -= 2;
        base.unequip();
    }
}