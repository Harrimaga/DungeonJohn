using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Ice : Tiles
{
    public bool OnThisTile = false;

    public Ice(Vector2 startPosition, int layer = 0, string id = "Ice")
    : base(startPosition, layer, id)
    {
        position = startPosition;
    }

    /// <summary>
    /// Check if the player is on the icetiles
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        if (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
        {
            OnThisTile = true;
        }
        else
        {
            OnThisTile = false;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite"), position,Color.Aqua);
    }
}