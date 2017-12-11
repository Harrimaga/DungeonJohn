using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
    }
    public virtual void Reset()
    {
    }

}

