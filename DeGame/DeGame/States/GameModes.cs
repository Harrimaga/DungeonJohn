using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameModes : IGameObject
{
    Button normalB, endlessB;
    public GameModes()
    {
        normalB = new Button(new Vector2(600, 340), "NormalMode", "NormalMode", "NormalModePressed", true, 1);
        endlessB = new Button(new Vector2(600, 440), "EndlessMode", "EndlessMode", "EndlessModePressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        normalB.HandleInput(inputHelper, gameTime);
        endlessB.HandleInput(inputHelper, gameTime);
        if (normalB.Pressed)
        {
            Boss.endless = false;
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }
        if (endlessB.Pressed)
        {
            Boss.endless = true;
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/PauseMenu/GameModes"), new Vector2(-400, -380));
        endlessB.Draw(gameTime, spriteBatch);
        normalB.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
        normalB.Update(gameTime);
        endlessB.Update(gameTime);
    }
    public virtual void Reset()
    {
    }
}