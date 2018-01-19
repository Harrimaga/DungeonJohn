using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class VialOfPoison : Item, IPassive
{
    float damageMultiplier;
    public VialOfPoison()
    {
        damageMultiplier = 1;
        itemName = "VialOfPoison";
        itemDescription = "As if bullets weren't deadly enough on their own";
        Type = "passive";
    }
    public float DamageMultiplier
    {
        get
        {
            return damageMultiplier;
        }
    }
    public override void equip()
    {
        PlayingState.player.VialOfPoison = true;
    }
    public override void unequip()
    {
        PlayingState.player.VialOfPoison = false;
        base.unequip();
    }
}