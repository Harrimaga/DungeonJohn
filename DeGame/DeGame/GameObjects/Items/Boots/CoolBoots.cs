class CoolBoots : Item, IBoots
{
    public CoolBoots()
    {
        Cost = 5;
        itemName = "CoolBoots";
        itemDescription = "Boots that can withstand lava much better than the average foot";
        Type = "boots";
    }

    public override void equip()
    {
        PlayingState.player.CoolBoots = true;
        PlayingState.player.extraspeed *= 1.20f;
    }

    public override void unequip()
    {
        PlayingState.player.CoolBoots = false;
        PlayingState.player.extraspeed /= 1.20f;
        base.unequip();
    }
}