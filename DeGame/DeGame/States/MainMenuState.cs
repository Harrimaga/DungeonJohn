using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MainMenuState : IGameObject
{
    //public static bool reset = false;
    public virtual void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Space))
        {
            GameEnvironment.gameStateManager.SwitchTo("Playing");
            Reset();
        }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/bteam"), new Vector2(200,70));
    }
    public virtual void Update(GameTime gameTime)
    {
    }
    public virtual void Reset()
    {
        PlayingState.player.health = 100;
        PlayingState.currentFloor.ResetFloor();
        PlayingState.player.position = PlayingState.currentFloor.startPlayerPosition;
    }

}

