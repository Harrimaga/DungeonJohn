using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class BloodRing : Item, IPassive
{
    float damageMultiplier;
    public BloodRing()
    {
        damageMultiplier = 1;
        itemName = "BloodRing";
        itemDescription = "Suck the life force out this ring to gain more health";
        Type = "passive";
    }
    public float DamageMultiplier
    {
        get
        {
            return damageMultiplier;
        }
    }
    public void Equip()
    {
        PlayingState.player.maxhealth += 100;
    }
    public void unequip()
    {
        PlayingState.player.extraspeed -= 100;
    }
}
