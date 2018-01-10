using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SlimyBoots : Item, IBoots
{
    public SlimyBoots()
    {
        itemName = "SlimyBoots";
        itemDescription = "Spiderwebs stand no chance against these boots.";
        Type = "boots";
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