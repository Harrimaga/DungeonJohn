﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BigMac : Item, IWeapon
{
    bool melee, twoHanded;
    float addedDamage, damageMultiplier, projectile_velocity, attackspeed, range;
    int ammo;
    Texture2D bulletleft, bulletup;

    public BigMac()
    {
        melee = false;
        twoHanded = true;
        addedDamage = 40;
        damageMultiplier = 1;
        projectile_velocity = 15;
        attackspeed = 40;
        range = 700;
        ammo = 300;
        Type = "weapon";
        itemName = "BigMac";
        itemDescription = "What is better than 1 gun? 2 guns!";
        bulletleft = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BigMac_BulletLeft");
        bulletup = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BigMac_BulletUp");
    }

    public override void unequip()
    {
        ammo = PlayingState.player.ammo;
    }

    public void Attack(int direction)
    {
        Bullet bullet = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 4), direction);
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
