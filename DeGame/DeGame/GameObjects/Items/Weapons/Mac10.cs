using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Mac10 : Item, IWeapon
{
    bool melee, twoHanded;
    float addedDamage, projectile_velocity, attackspeed, range;
    int ammo;
    Texture2D bulletleft, bulletup;

    public Mac10()
    {
        Cost = 7;
        melee = false;
        twoHanded = false;
        addedDamage = 10;
        projectile_velocity = 0.8f;
        attackspeed = 0.3f;
        range = 250;
        ammo = 200;
        Type = "weapon";
        itemName = "Mac10";
        itemDescription = "You ought to know this weapon by now.";
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
        Bullet bullet = new Bullet(PlayingState.player.position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 4), direction);
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

}