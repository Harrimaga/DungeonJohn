using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class WornItems : GameObjectList
{
    WeaponSlot weaponSlot;
    HelmetSlot helmetSlot;
    ArmourSlot armourSlot;
    BootsSlot bootsSlot;
    ShieldSlot shieldShot;
    PassiveSlot passiveSlot1, passiveSlot2;
    new Vector2 position;


    public WornItems(Vector2 position) : base()
    {
        this.position = position;
        weaponSlot = new WeaponSlot(position + new Vector2(0, 74));
        helmetSlot = new HelmetSlot(position + new Vector2(74, 0));
        armourSlot = new ArmourSlot(position + new Vector2(74, 74));
        bootsSlot = new BootsSlot(position + new Vector2(74, 212));
        shieldShot = new ShieldSlot(position + new Vector2(212, 74));
        passiveSlot1 = new PassiveSlot(position);
        passiveSlot2 = new PassiveSlot(position + new Vector2(212, 0));

        Add(weaponSlot);
        Add(helmetSlot);
        Add(armourSlot);
        Add(bootsSlot);
        Add(shieldShot);
        Add(passiveSlot1);
        Add(passiveSlot2);
    }

    public override Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            weaponSlot.position = position + new Vector2(0, 74);
            helmetSlot.position = position + new Vector2(74, 0);
            armourSlot.position = position + new Vector2(74, 74);
            bootsSlot.position = position + new Vector2(74, 212);
            shieldShot.position = position + new Vector2(212, 74);
            passiveSlot1.position = position;
            passiveSlot2.position = position + new Vector2(212, 0);
        }
    }
}

