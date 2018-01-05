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
    float addedDamage, damageMultiplier, projectile_velocity, attackspeed, range;
    int ammo;

    public StandardBow()
    {
        melee = false;
        twoHanded = true;
        addedDamage = 50;
        damageMultiplier = 1;
        projectile_velocity = 10;
        attackspeed = 50;
        range = 800;
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

    public float AddedDamage
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

    public float DamageMultiplier
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

    public float Projectile_Velocity
    {
        get
        {
            return projectile_velocity;
        }
    }

    public float AttackSpeed
    {
        get
        {
            return attackspeed;
        }
    }

    public float Range
    {
        get
        {
            return range;
        }
    }
}

