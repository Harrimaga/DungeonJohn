class HelicopterHat : Item, IHelmet
{
    public HelicopterHat()
    {
        Cost = 20;
        itemName = "HelicopterHat";
        itemDescription = "Has anyone ever even worn one of these things? Grants the ability to fly";
        Type = "helmet";
    }
    public override void equip()
    {
        PlayingState.player.HelicopterHat = true;
    }
    public override void unequip()
    {
        PlayingState.player.HelicopterHat = false;
        base.unequip();
    }
}