using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class VictoryState : IGameObject
{
    Button MainMenu;
    Vector2 BasisPosition;
    public VictoryState()
    {
        MainMenu = new Button(new Vector2(655, 775), "ReturnToMainMenu", "ReturnToMainMenu", "ReturnToMainMenuPressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        MainMenu.HandleInput(inputHelper, gameTime);
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/VictoryScreen"), BasisPosition);
        MainMenu.Draw(gameTime, spriteBatch);
    }

    public virtual void Update(GameTime gameTime)
    {
        MainMenu.Update(gameTime);
        if (MainMenu.Pressed)
            GameEnvironment.gameStateManager.SwitchTo("Credits");
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    }
    public virtual void Reset()
    {
    }
}

