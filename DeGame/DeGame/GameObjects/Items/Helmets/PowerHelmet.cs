using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PowerHelmet : Item, IHelmet
{
    public PowerHelmet()
    {
        itemName = "PowerHelmet";
        itemDescription = "With a helmet like this you have the power to do anything";
        Type = "helmet";
    }
    public override void equip()
    {
        PlayingState.player.extraattack += 10;
        PlayingState.player.CalculateDamage();
    }
    public override void unequip()
    {
        PlayingState.player.extraattack -= 10;
        PlayingState.player.CalculateDamage();
        base.unequip();
    }
}