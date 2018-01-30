using Microsoft.Xna.Framework.Graphics;

public class BigMac : Item, IWeapon
{
    bool melee, twoHanded;
    float addedDamage, projectile_velocity, attackspeed, range;
    int ammo, maxammo;
    Texture2D bulletleft, bulletup;

    public BigMac()
    {
        Cost = 18;
        melee = false;
        twoHanded = true;
        addedDamage = 45;
        projectile_velocity = 0.7f;
        attackspeed = 1.5f;
        range = 250;
        maxammo = 300;
        ammo = maxammo;
        Type = "weapon";
        itemName = "BigMac";
        itemDescription = "I'm lovin' it";
        bulletleft = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BigMac_BulletLeft");
        bulletup = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BigMac_BulletUp");
    }

    public override void unequip()
    {
        ammo = PlayingState.player.ammo;
        base.unequip();
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

    public Texture2D BulletSpriteLeft
    {
        get
        {
            return bulletleft;
        }
    }

    public Texture2D BulletSpriteUp
    {
        get
        {
            return bulletup;
        }
    }

    public int MaxAmmo
    {
        get
        {
            return maxammo;
        }
    }
}

