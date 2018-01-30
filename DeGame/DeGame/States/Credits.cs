using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Credits : IGameObject
{
    Vector2 velocity = new Vector2(0, -0.1f);
    Vector2 position = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    public Credits()
    {
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (inputHelper.KeyPressed(Keys.Space))
            GameEnvironment.gameStateManager.SwitchTo("Victory");
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/Credits"), position);
    }
    public virtual void Update(GameTime gameTime)
    {
        position += velocity * gameTime.ElapsedGameTime.Milliseconds;
        //Counter++;
        //if (Counter >= 1100)
        //{
        //    Reset();
        //    GameEnvironment.gameStateManager.SwitchTo("MainMenu");
        //}
        
    }
    public virtual void Reset()
    {
    }
}