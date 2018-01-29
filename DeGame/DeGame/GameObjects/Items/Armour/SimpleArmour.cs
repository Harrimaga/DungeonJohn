class SimpleArmour : Item, IArmour
{
    public SimpleArmour()
    {
        Cost = 7;
        itemName = "SimpleArmour";
        itemDescription = "This is simply Armour";
        Type = "armour";
    }

    public override void equip()
    {
        PlayingState.player.changeMaxHealth(120);
    }
    public override void unequip()
    {
        PlayingState.player.changeMaxHealth(-120);
        base.unequip();
    }
}