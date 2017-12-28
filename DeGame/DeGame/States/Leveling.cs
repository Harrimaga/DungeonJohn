using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Leveling : IGameObject
{
    Vector2 BasisPosition;
    bool picked = false;
    protected IGameObject playingState;
    public Leveling()
    {
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/pauze"), BasisPosition);
    }
    public virtual void Update(GameTime gameTime)
    {
        if (picked)
        {
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }
    }
    public virtual void Reset()
    {
    }
}
