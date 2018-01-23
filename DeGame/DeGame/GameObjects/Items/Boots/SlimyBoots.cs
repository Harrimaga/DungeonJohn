using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SlimyBoots : Item, IBoots
{
    public SlimyBoots()
    {
        Cost = 7;
        itemName = "SlimyBoots";
        itemDescription = "Ice stands no chance against these boots.";
        Type = "boots";
    }

    public override void equip()
    {
        PlayingState.player.SlimyBoots = true;
        PlayingState.player.extraspeed *= 1.20f;
    }

    public override void unequip()
    {
        PlayingState.player.SlimyBoots = false;
        PlayingState.player.extraspeed /= 1.20f;
        base.unequip();
    }
}