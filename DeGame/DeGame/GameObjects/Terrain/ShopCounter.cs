using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ShopCounter : Solid
{
    public ShopCounter(Vector2 startPosition, int layer = 0, string id = "ShopCounter")
    : base(startPosition, layer, id)
    {
        position = startPosition;
        Rectangle newBoundingBox = new Rectangle((int)startPosition.X, (int)startPosition.Y, GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite").Width, GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite").Height);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        SolidCollision();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
}