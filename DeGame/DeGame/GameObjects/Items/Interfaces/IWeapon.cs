using Microsoft.Xna.Framework.Graphics;

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

    int MaxAmmo
    {
        get;
    }

    void Attack(int direction);
}

