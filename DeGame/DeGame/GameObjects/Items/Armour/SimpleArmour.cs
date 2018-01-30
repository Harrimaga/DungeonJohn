class SimpleArmour : Item, IArmour
{
    public SimpleArmour()
    {
        Cost = 9;
        itemName = "SimpleArmour";
        itemDescription = "This is simply Armour";
        Type = "armour";
    }

    public override void equip()
    {
        PlayingState.player.changeMaxHealth(90);
    }
    public override void unequip()
    {
        PlayingState.player.changeMaxHealth(-90);
        base.unequip();
    }
}