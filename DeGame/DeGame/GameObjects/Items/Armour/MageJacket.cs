class MageJacket : Item, IArmour
{
    public MageJacket()
    {
        Cost = 7;
        itemName = "MageJacket";
        itemDescription = "This jacket was worn by an old mage making you move much faster";
        Type = "armour";
    }

    public override void equip()
    {
        PlayingState.player.extraspeed *= 1.15f;
    }
    public override void unequip()
    {
        PlayingState.player.extraspeed /= 1.15f;
        base.unequip();
    }
}