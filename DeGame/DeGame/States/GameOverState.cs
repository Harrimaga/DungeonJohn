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
    Vector2 BasisPosition;
    protected IGameObject playingState;
    public GameOverState()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Z))
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
            Reset();
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/GameOver"), BasisPosition);
    }

    public virtual void Update(GameTime gameTime)
    {
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    }
    public virtual void Reset()
    {
        PlayingState.player.health = 100;
        PlayingState.currentFloor.ResetFloor();
        PlayingState.player.position = PlayingState.currentFloor.startPlayerPosition;
    }

}

