using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HardHelmet : Item, IHelmet
{
    public HardHelmet()
    {
        itemName = "HardHelmet";
        itemDescription = "Bullets are not very effective, but it is so heavy that you are a lot slower";
        Type = "helmet";
    }
    public void Equip()
    {
        PlayingState.player.damagereduction *= 0.8;
        PlayingState.player.extraspeed-= 10;
    }
    public void unequip()
    {
        PlayingState.player.damagereduction *= 1.25;
        PlayingState.player.extraspeed++;
        base.unequip();
    }
}