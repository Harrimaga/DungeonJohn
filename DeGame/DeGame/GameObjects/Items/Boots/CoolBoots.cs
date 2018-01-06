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
    }
    public void Equip()
    {
        PlayingState.player.CoolBoots = true;
        PlayingState.player.extraspeed += 2;
    }
    public void Unequip()
    {
        PlayingState.player.extraspeed -= 2;
    }
}