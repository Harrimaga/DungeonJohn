using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

class Consumables : SpriteGameObject
{
    string type;
    Texture2D consumablesprite;
    public static InventoryManager inventory;

    public Consumables(Vector2 startPosition, string consumablename, int layer = 0, string id = "consumable")
    : base("Sprites/Drops/Coin", layer, id)
    {
        position = startPosition + new Vector2(30,30);
        type = consumablename;
        if (type == "heart")
            consumablesprite = GameEnvironment.assetManager.GetSprite("Sprites/Drops/Heart");
        else if (type == "ammo")
            consumablesprite = GameEnvironment.assetManager.GetSprite("Sprites/Drops/Ammo");
        else if (type == "gold")
            consumablesprite = GameEnvironment.assetManager.GetSprite("Sprites/Drops/Coin");
        else if (type == "toaster")
            consumablesprite = GameEnvironment.assetManager.GetSprite("Sprites/Drops/Toaster");
        GameEnvironment.soundManager.loadSoundEffect("Pickup");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player)) 
        {
            switch (type)
            {
                case "heart":
                    if (PlayingState.player.health < PlayingState.player.maxhealth)
                    {
                        if (PlayingState.player.health <= PlayingState.player.maxhealth - 20)
                            PlayingState.player.health += 20;
                        else
                            PlayingState.player.health += PlayingState.player.maxhealth - PlayingState.player.health;
                        GameEnvironment.soundManager.playSoundEffect("Pickup");
                        GameObjectList.RemovedObjects.Add(this);
                    }
                    break;
                case "ammo":
                    IWeapon weapon = (IWeapon)Player.inventory.currentWeapon;
                    if (PlayingState.player.ammo >= 0 && weapon!= null && PlayingState.player.ammo < weapon.MaxAmmo)
                    {
                        PlayingState.player.ammo += 25;
                        if (PlayingState.player.ammo > weapon.MaxAmmo)
                            PlayingState.player.ammo = weapon.MaxAmmo;
                        GameEnvironment.soundManager.playSoundEffect("Pickup");
                        GameObjectList.RemovedObjects.Add(this);
                    }
                    break;
                case "gold":
                    PlayingState.player.gold += 5;
                    GameEnvironment.soundManager.playSoundEffect("Pickup");
                    GameObjectList.RemovedObjects.Add(this);
                    break;
                case "toaster":
                    GameEnvironment.gameStateManager.SwitchTo("Victory");
                    GameObjectList.RemovedObjects.Add(this);
                    break;
            }
        }
    }

    public override string ToString()
    {
        return type;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(consumablesprite, position, Color.White);
    }
}

