using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Cool_Boots : Item, IBoots
{
    public Cool_Boots()
    {
        itemName = "Cool Boots";
        Equip();
        itemDescription = "Boots so cool they can withstand the hottest of lava!";
    }
    public void Equip()
    {
        PlayingState.player.Cool_Boots = true;
        PlayingState.player.speed++;
    }
    public void Unequip()
    {
        PlayingState.player.speed--;
    }
}