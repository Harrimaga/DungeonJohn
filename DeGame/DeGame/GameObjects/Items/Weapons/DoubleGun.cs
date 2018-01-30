using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DoubleGun : Item, IWeapon
{
    bool melee, twoHanded;
    float addedDamage, projectile_velocity, attackspeed, range;
    int ammo, maxammo;
    Texture2D bulletleft, bulletup;

    public DoubleGun()
    {
        Cost = 10;
        melee = false;
        twoHanded = false;
        addedDamage = 40;
        projectile_velocity = 0.7f;
        attackspeed = 3.0f;
        range = 550;
        maxammo = 200;
        ammo = maxammo;
        Type = "weapon";
        itemName = "DoubleGun";
        itemDescription = "What is better than 1 gun? 2 guns!!";
        bulletleft = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/Mac10_BulletLeft");
        bulletup = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/Mac10_BulletUp");
    }

    public override void unequip()
    {
        ammo = PlayingState.player.ammo;
        base.unequip();
    }

    public void Attack(int direction)
    {
        int forwarddirection = direction;
        if (direction == 2)
            forwarddirection = 1;
        else if (direction == 4)
            forwarddirection = 3;
        Bullet bullet = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 4), forwarddirection);
        if (direction == 1 || direction == 2)
        {
            Bullet bulletBack = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 4), 2);
            PlayingState.player.bullets.Add(bulletBack);
        }

        else if (direction == 3 || direction == 4)
        {
            Bullet bulletBack = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 4), 4);
            PlayingState.player.bullets.Add(bulletBack);
        }
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

