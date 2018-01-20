using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GameOverState : IGameObject
{
    Button MainMenu;
    Vector2 BasisPosition;
    protected IGameObject playingState;
    public GameOverState()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
        MainMenu = new Button(new Vector2(620, 760), "ReturnToMainMenu", "ReturnToMainMenu", "ReturnToMainMenuPressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        MainMenu.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Z))
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/GameOver"), BasisPosition);
        MainMenu.Draw(gameTime, spriteBatch);
    }

    public virtual void Update(GameTime gameTime)
    {
        MainMenu.Update(gameTime);
        if (MainMenu.Pressed)
            GameEnvironment.gameStateManager.SwitchTo("MainMenu"); 
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2) - 60);
    }
    public virtual void Reset()
    {
    }

}

