class HardHelmet : Item, IHelmet
{
    public HardHelmet()
    {
        itemName = "HardHelmet";
        itemDescription = "Bullets are not very effective, but it is so heavy that you are a lot slower";
        Type = "helmet";
    }
    public override void equip()
    {
        PlayingState.player.damagereduction *= 0.8;
        PlayingState.player.extraspeed*= 0.7f;
    }
    public override void unequip()
    {
        PlayingState.player.damagereduction /= 0.8;
        PlayingState.player.extraspeed/= 0.7f;
        base.unequip();
    }
}