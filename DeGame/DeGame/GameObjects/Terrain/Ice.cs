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

    public override void Update(GameTime gameTime)
    {
        if (CollidesWith(PlayingState.player))
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