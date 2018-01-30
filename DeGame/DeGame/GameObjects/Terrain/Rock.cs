using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Rock : Solid
{
    bool IceRock;

    public Rock(Vector2 startPosition, int layer = 0, string id = "Rock", bool icerock = false)
    : base(startPosition, layer, id)
    {
        position = startPosition;
        IceRock = icerock;
    }

    public override void Update(GameTime gameTime)
    {
        /// When the player is not wearing the helicopterhat, all the rocks will be solid
        base.Update(gameTime);
        if(!PlayingState.player.HelicopterHat)
        {
            SolidCollision();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        /// When a rock is a icerock, a icerock will be drawn instead
        if (!IceRock)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Rock Sprite"), position);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Ice Rock Sprite"), position);
    }
}
