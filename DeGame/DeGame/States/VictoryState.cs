using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class VictoryState : IGameObject
{
    Button Credits;
    Vector2 BasisPosition;
    public VictoryState()
    {
        //button
        Credits = new Button(new Vector2(605, 775), "Credits", "Credits", "CreditsPressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        Credits.HandleInput(inputHelper, gameTime);
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //draws victory picture and button
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/VictoryScreen"), BasisPosition);
        Credits.Draw(gameTime, spriteBatch);
    }

    public virtual void Update(GameTime gameTime)
    {
        Credits.Update(gameTime);
        //button switch ing state
        if (Credits.Pressed)
            GameEnvironment.gameStateManager.SwitchTo("Credits");
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    }
    public virtual void Reset()
    {
    }
}

