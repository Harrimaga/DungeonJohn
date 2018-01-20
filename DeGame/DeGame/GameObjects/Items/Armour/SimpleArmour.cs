using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SimpleArmour : Item, IArmour
{
    public SimpleArmour()
    {
        itemName = "SimpleArmour";
        itemDescription = "This is simply Armour";
        Type = "armour";
    }

    public override void equip()
    {
        PlayingState.player.changeMaxHealth(120);
    }
    public override void unequip()
    {
        PlayingState.player.changeMaxHealth(-120);
        base.unequip();
    }
}