using Microsoft.Xna.Framework.Graphics;
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

    float AddedDamage
    {
        get;
    }

    int Ammo
    {
        get;
    }

    float Projectile_Velocity
    {
        get;
    }

    float AttackSpeed
    {
        get;
    }

    float Range
    {
        get;
    }

    Texture2D BulletSpriteLeft
    {
        get;
    }

    Texture2D BulletSpriteUp
    {
        get;
    }

    string Type
    {
        get;
    }

    void Attack(int direction);
}

