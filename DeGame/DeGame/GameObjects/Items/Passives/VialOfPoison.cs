public class VialOfPoison : Item, IPassive
{
    public VialOfPoison()
    {
        Cost = 10;
        itemName = "VialOfPoison";
        itemDescription = "As if bullets weren't deadly enough on their own";
        Type = "passive";
    }

    public override void equip()
    {
        PlayingState.player.poisonchance += 0.08;
    }
    public override void unequip()
    {
        PlayingState.player.poisonchance -= 0.08;
        base.unequip();
    }
}