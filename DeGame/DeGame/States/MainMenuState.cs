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
    Button start, option;

    public MainMenuState()
    {
       start = new Button(new Vector2(620, 725), "Start", "Start","StartPressed",true, 1);
       option = new Button(new Vector2(620, 800), "Option", "Option", "OptionPressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        start.HandleInput(inputHelper, gameTime);
        option.HandleInput(inputHelper, gameTime);
        if (inputHelper.KeyPressed(Keys.Space) || start.Pressed)
        {
            GameEnvironment.gameStateManager.SwitchTo("Playing");
            Reset();
        }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/NewBTeam"), new Vector2(-300, -350));
        start.Draw(gameTime, spriteBatch);
        option.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
        start.Update(gameTime);
        option.Update(gameTime);
        Camera.Position = new Vector2(0,0);
    }
    public virtual void Reset()
    {
        PlayingState.player.health = 100;
        PlayingState.currentFloor.ResetFloor();
        PlayingState.player.position = PlayingState.currentFloor.startPlayerPosition;
    }

}

