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

    public void Equip()
    {
        PlayingState.player.extraspeed += 2;
    }
    public void Unequip()
    {
        PlayingState.player.extraspeed -= 2;
        base.unequip();
    }
}