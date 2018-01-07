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
        itemDescription = "This jacket was worn by a old mage making take move faster";
    }
    public void Equip()
    {
        PlayingState.player.speed += 2;
    }
    public void Unequip()
    {

        PlayingState.player.speed -= 2;
    }
}