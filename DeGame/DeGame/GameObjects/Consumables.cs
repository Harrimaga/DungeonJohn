using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Consumables : SpriteGameObject
{
    Vector2 dropposition;
    string type;
    Texture2D consumablesprite;
    public Consumables(Vector2 startPosition, string consumablename, int layer = 0, string id = "consumable")
    : base("Sprites/Coin", layer, id)
    {
        dropposition = startPosition + new Vector2(30,30);
        type = consumablename;
        if (type == "heart")
            consumablesprite = GameEnvironment.assetManager.GetSprite("Sprites/Heart");
        if (type == "gold")
            consumablesprite = GameEnvironment.assetManager.GetSprite("Sprites/Coin");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player)) //werkt niet
        {
            switch (type)
            {
                case "heart":
                    PlayingState.player.health += 20; PlayingState.player.health += 20;
                    break;
                case "gold":
                    PlayingState.player.gold += 5;
                    break;
            }
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(consumablesprite, dropposition, Color.White);
    }

    public override string ToString()
    {
        return type;
    }
}

