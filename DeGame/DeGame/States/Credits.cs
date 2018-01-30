using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Credits : IGameObject
{
    Vector2 position, Baseposition;
    Vector2 velocity = new Vector2(0, -0.1f);
    int Counter = 0;
    bool w = false;
    public Credits()
    {
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (inputHelper.KeyPressed(Keys.Space))
            GameEnvironment.gameStateManager.SwitchTo("Victory");
    }

    public virtual void Update(GameTime gameTime)
    {
        if (w == false)
        {
            position = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
            w = true;
        }

        Baseposition = position += velocity * gameTime.ElapsedGameTime.Milliseconds;
        Counter += 1 * gameTime.ElapsedGameTime.Milliseconds;
        if (Counter >= 55000)
        {
            //Reset();
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/Credits"), Baseposition);
    }

    public virtual void Reset()
    {
    }
}