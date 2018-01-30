class PowerHelmet : Item, IHelmet
{
    public PowerHelmet()
    {
        Cost = 13;
        itemName = "PowerHelmet";
        itemDescription = "With a helmet like this you have the power to do anything";
        Type = "helmet";
    }
    public override void equip()
    {
        PlayingState.player.damagereduction *= 0.7;
        PlayingState.player.extraattack += 5;
        PlayingState.player.CalculateDamage();
    }
    public override void unequip()
    {
        PlayingState.player.damagereduction /= 0.7;
        PlayingState.player.extraattack -= 5;
        PlayingState.player.CalculateDamage();
        base.unequip();
    }
}