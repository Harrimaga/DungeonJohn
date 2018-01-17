using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DoubleGun : Item, IWeapon
{
    bool melee, twoHanded;
    float addedDamage, damageMultiplier, projectile_velocity, attackspeed, range;
    int ammo;
    Texture2D bulletleft, bulletup;

    public DoubleGun()
    {
        melee = false;
        twoHanded = false;
        addedDamage = 30;
        damageMultiplier = 1;
        projectile_velocity = 12;
        attackspeed = 30;
        range = 800;
        ammo = 200;
        Type = "weapon";
        itemName = "DoubleGun";
        itemDescription = "What is better then 1 gun 2 guns!";
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
        Bullet bullet = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 4), direction);
        Bullet bulletBack = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 4), direction);
        if (direction == 1)
        {
            bulletBack = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 4), 2);
        }
        if (direction == 2)
        {
            bulletBack = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 4), 1);
        }
        if (direction == 3)
        {
            bulletBack = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 4), 4);
        }
        if (direction == 4)
        {
            bulletBack = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 4), 3);
        }
        PlayingState.player.bullets.Add(bullet);
        PlayingState.player.bullets.Add(bulletBack);
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

}

