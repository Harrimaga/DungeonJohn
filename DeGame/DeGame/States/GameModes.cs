using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameModes : IGameObject
{
    Button normalB, endlessB, mainmenuB;
    public GameModes()
    {
        //buttons
        normalB = new Button(new Vector2(600, 340), "NormalMode", "NormalMode", "NormalModePressed", true, 1);
        endlessB = new Button(new Vector2(600, 440), "EndlessMode", "EndlessMode", "EndlessModePressed", true, 1);
        mainmenuB = new Button(new Vector2(600, 540), "MainMenu", "MainMenu2", "MainMenu2Pressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        normalB.HandleInput(inputHelper, gameTime);
        endlessB.HandleInput(inputHelper, gameTime);
        mainmenuB.HandleInput(inputHelper, gameTime);

        //which states buttons has to switch
        if (normalB.Pressed)
        {
            Boss.endless = false;
            GameEnvironment.gameStateManager.SwitchTo("Playing");
            GameEnvironment.soundManager.PlaySong("Base");
        }
        if (endlessB.Pressed)
        {
            Boss.endless = true;
            GameEnvironment.gameStateManager.SwitchTo("Playing");
            GameEnvironment.soundManager.PlaySong("Base");
        }

        if (mainmenuB.Pressed)
        {
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
        }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //draw tekst picture and draws buttons
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/PauseMenu/GameModes"), new Vector2(-400, -380));
        normalB.Draw(gameTime, spriteBatch);
        endlessB.Draw(gameTime, spriteBatch);
        mainmenuB.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
        //updates buttons
        normalB.Update(gameTime);
        endlessB.Update(gameTime);
        mainmenuB.Update(gameTime);
    }
    public virtual void Reset()
    {
    }
}