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
        Type = "boots";

    }

    public override void equip()
    {
        PlayingState.player.CoolBoots = true;
        PlayingState.player.extraspeed += 2;
    }

    public override void unequip()
    {
        PlayingState.player.CoolBoots = false;
        PlayingState.player.extraspeed -= 2;
    }
}