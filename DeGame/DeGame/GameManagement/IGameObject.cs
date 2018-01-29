using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IGameObject
{
    void HandleInput(InputHelper inputHelper, GameTime gameTime);

    void Update(GameTime gameTime);

    void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    void Reset();
}

