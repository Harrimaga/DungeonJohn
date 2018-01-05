using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StandardBow : Item, IWeapon
{
    bool melee, twoHanded;
    int addedDamage, damageMultiplier, ammo;

    public StandardBow()
    {
        melee = false;
        twoHanded = true;
        addedDamage = 30;
        damageMultiplier = 1;
        ammo = -1;
        Type = "weapon";
        itemName = "StandardBow";
        itemDescription = "Basic weapon with infinte ammo and suck damage.";
    }

    public override void unequip()
    {
        ammo = PlayingState.player.ammo;
    }

    public void Attack(int direction)
    {
        Bullet bullet = new Bullet(PlayingState.player.position, direction);
        PlayingState.player.bullets.Add(bullet);
    }

    public int AddedDamage
    {
        get
        {
            return addedDamage;
        }
    }

    public int Ammo
    {
        get
        {
            return ammo;
        }
    }

    public int DamageMultiplier
    {
        get
        {
            return damageMultiplier;
        }
    }

    public bool Melee
    {
        get
        {
            return melee;
        }
    }

    public bool TwoHanded
    {
        get
        {
            return twoHanded;
        }
    }
}

