public class BloodRing : Item, IPassive
{
    public BloodRing()
    {
        Cost = 5;
        itemName = "BloodRing";
        itemDescription = "Suck the life force out this ring to gain more health";
        Type = "passive";
    }
    public override void equip()
    {
        PlayingState.player.changeMaxHealth(75);
    }
    public override void unequip()
    {
        PlayingState.player.changeMaxHealth(-75);
        base.unequip();
    }
}