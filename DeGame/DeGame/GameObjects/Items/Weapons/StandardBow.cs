using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Base weapon class
/// </summary>
public class StandardBow : Item, IWeapon
{
    bool melee, twoHanded;
    float addedDamage, projectile_velocity, attackspeed, range;
    int ammo, maxammo;
    Texture2D bulletleft, bulletup;

    public StandardBow()
    {
        melee = false;
        twoHanded = true;
        addedDamage = 40;
        projectile_velocity = 0.5f;
        attackspeed = 4;
        range = 400;
        maxammo = -1;
        ammo = maxammo;
        Type = "weapon";
        itemName = "StandardBow";
        itemDescription = "Basic weapon with infinte ammo";
        bulletleft = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/StandardBow_BulletLeft");
        bulletup = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/StandardBow_BulletUp");
    }

    /// <summary>
    /// Sets this weapon's ammo to the player's ammo, so that it is saved
    /// </summary>
    public override void unequip()
    {
        ammo = PlayingState.player.ammo;
        base.unequip();
    }

    /// <summary>
    /// Shoots a projectile in a given direction
    /// </summary>
    /// <param name="direction">Direction int (1 - 4)</param>
    public void Attack(int direction)
    {
        // Creates a new bullet
        Bullet bullet = new Bullet(PlayingState.player.position, direction);

        // Add this bullet to the bullet GameObjectList
        PlayingState.player.bullets.Add(bullet);
    }

    /// <summary>
    /// Gets the damage bonus that this weapon has
    /// </summary>
    public float AddedDamage
    {
        get
        {
            return addedDamage;
        }
    }

    /// <summary>
    /// Gets the weapon's ammo
    /// </summary>
    public int Ammo
    {
        get
        {
            return ammo;
        }
    }

    /// <summary>
    /// Gets whether the weapon is melee or not
    /// Melee is currently unused
    /// </summary>
    public bool Melee
    {
        get
        {
            return melee;
        }
    }

    /// <summary>
    /// Gets whether the weapon is worn in two hands or not
    /// Currently unused
    /// </summary>
    public bool TwoHanded
    {
        get
        {
            return twoHanded;
        }
    }

    /// <summary>
    /// Gets the velocity of the projectiles
    /// Each weapon has different projectile velocities
    /// </summary>
    public float Projectile_Velocity
    {
        get
        {
            return projectile_velocity;
        }
    }

    /// <summary>
    /// Gets the attack speed of the weapon
    /// AttackSpeed determines the delay between being able to shoot another projectile
    /// </summary>
    public float AttackSpeed
    {
        get
        {
            return attackspeed;
        }
    }

    /// <summary>
    /// Gets the range of the weapon
    /// Range determines how long a projectile can travel before it despawns
    /// </summary>
    public float Range
    {
        get
        {
            return range;
        }
    }

    /// <summary>
    /// Gets the sprite for the projectile traveling left
    /// </summary>
    public Texture2D BulletSpriteLeft
    {
        get
        {
            return bulletleft;
        }
    }

    /// <summary>
    /// Gets the sprite for the projectile traveling up
    /// </summary>
    public Texture2D BulletSpriteUp
    {
        get
        {
            return bulletup;
        }
    }

    /// <summary>
    /// Gets the maximum amount of ammo the player may carry for this weapon
    /// </summary>
    public int MaxAmmo
    {
        get
        {
            return maxammo;
        }
    }
}