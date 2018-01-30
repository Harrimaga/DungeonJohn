using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Wall : Solid
{
    public Wall(Vector2 startPosition, int layer = 0, string id = "Wall")
    : base(startPosition, layer, id)
    {
        position = startPosition;
        Rectangle newBoundingBox = new Rectangle((int)startPosition.X, (int)startPosition.Y, GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite").Width, GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite").Height);
    }

    public override void Update(GameTime gameTime)
    {
        /// All walls are solid
        base.Update(gameTime);
        SolidCollision();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
}
