﻿class SlimyBoots : Item, IBoots
{
    public SlimyBoots()
    {
        Cost = 7;
        itemName = "SlimyBoots";
        itemDescription = "Ice stands no chance against these boots.";
        Type = "boots";
    }

    public override void equip()
    {
        PlayingState.player.SlimyBoots = true;
        PlayingState.player.extraspeed *= 0.90f;
    }

    public override void unequip()
    {
        PlayingState.player.SlimyBoots = false;
        PlayingState.player.extraspeed /= 0.90f;
        base.unequip();
    }
}