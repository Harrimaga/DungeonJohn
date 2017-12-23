using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Consumables : SpriteGameObject
{
    Vector2 dropposition;
    string type;

    public Consumables(Vector2 startPosition, string consumablename, int layer = 0, string id = "consumable")
    : base("Sprites/Coin", layer, id)
    {
        dropposition = position;
        type = consumablename;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
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
            PlayingState.player.gold += 5;
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Coin")), dropposition, Color.White);
    }
}

