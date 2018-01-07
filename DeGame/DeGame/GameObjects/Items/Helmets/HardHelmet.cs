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
    }
    public void Equip()
    {
        PlayingState.player.HardHelmet = true;
        PlayingState.player.speed--;
    }
    public void Unequip()
    {
        PlayingState.player.HardHelmet = false;
        PlayingState.player.speed++;
    }
}