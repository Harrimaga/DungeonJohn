using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Heart : SpriteGameObject
{
    Vector2 dropposition;
    public Heart(Vector2 startPosition, int layer = 0, string id = "Gold")
    : base("Sprites/Coin", layer, id)
    {
        dropposition = position;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            PlayingState.player.health += 20;
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Heart")), dropposition, Color.White);
    }
}

