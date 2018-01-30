class MageJacket : Item, IArmour
{
    public MageJacket()
    {
        Cost = 13;
        itemName = "MageJacket";
        itemDescription = "Get the Cool boots effect and attack and move faster";
        Type = "armour";
    }

    public override void equip()
    {
        PlayingState.player.CoolBoots = true;
        PlayingState.player.extraspeed *= 1.15f;
        PlayingState.player.attackspeedreduction *= 0.90f;
    }
    public override void unequip()
    {
        PlayingState.player.SlimyBoots = false;
        PlayingState.player.extraspeed /= 1.15f;
        PlayingState.player.attackspeedreduction /= 0.90f;
        base.unequip();
    }
}