using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IWeapon
{
    bool Melee
    {
        get;
    }

    bool TwoHanded
    {
        get;
    }

    int AddedDamage
    {
        get;
    }

    int DamageMultiplier
    {
        get;
    }

    int Ammo
    {
        get;
    }
}

