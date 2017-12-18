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
    protected IGameObject playingState;
    public GameOverState()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.currentKeyboardState.IsKeyDown(Keys.Z) && inputHelper.previousKeyboardState.IsKeyUp(Keys.Z))
            MainMenuState.reset = true;
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/GameOver"), Vector2.Zero);
    }
    public virtual void Update(GameTime gameTime)
    {
    }
    public virtual void Reset()
    {
    }

}

