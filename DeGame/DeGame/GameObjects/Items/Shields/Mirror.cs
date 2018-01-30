﻿class Mirror : Item, IShield
{
    float addeddefence, defencemultiplier;
    public Mirror()
    {
        Cost = 12;
        addeddefence = 1;
        defencemultiplier = 1;
        itemName = "Mirror";
        itemDescription = "Sometimes reduces bullet damage and reflects the projectile";
        Type = "shield";
    }

    public override void equip()
    {
        PlayingState.player.Mirror = true;
    }

    public override void unequip()
    {
        PlayingState.player.Mirror = false;
        base.unequip();
    }

    public float AddedDefence
    {
        get
        {
            return addeddefence;
        }
    }

    public float DefenceMultiplier
    {
        get
        {
            return defencemultiplier;
        }
    }
}

