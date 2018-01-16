using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Consumables : SpriteGameObject
{
    string type;
    Texture2D consumablesprite;
    public Consumables(Vector2 startPosition, string consumablename, int layer = 0, string id = "consumable")
    : base("Sprites/Drops/Coin", layer, id)
    {
        position = startPosition + new Vector2(30,30);
        type = consumablename;
        if (type == "heart")
            consumablesprite = GameEnvironment.assetManager.GetSprite("Sprites/Drops/Heart");
        if (type == "gold")
            consumablesprite = GameEnvironment.assetManager.GetSprite("Sprites/Drops/Coin");
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
                        GameObjectList.RemovedObjects.Add(this);
                    }
                        break;
                case "gold":
                    PlayingState.player.gold += 5;
                    GameObjectList.RemovedObjects.Add(this);
                    break;
            }

        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(consumablesprite, position, Color.White);
    }

    public override string ToString()
    {
        return type;
    }
}

