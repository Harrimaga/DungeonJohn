using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CoolBoots : Item, IBoots
{
    public CoolBoots()
    {
        itemName = "CoolBoots";
        itemDescription = "Boots that can withstand lava much better than the average foot";
        PlayingState.player.CoolBoots = true;
        PlayingState.player.extraspeed += 2;
    }

    public override void equip()
    {

    }

    public override void unequip()
    {
        PlayingState.player.CoolBoots = false;
        PlayingState.player.extraspeed -= 2;
    }
}