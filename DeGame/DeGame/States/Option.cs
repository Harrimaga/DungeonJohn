using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Option : IGameObject
{
    Button MainMenu;
    public Option()
    {
        MainMenu = new Button(new Vector2(655, 840), "ReturnToMainMenu", "ReturnToMainMenu", "ReturnToMainMenuPressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        MainMenu.HandleInput(inputHelper, gameTime);
        if (inputHelper.KeyPressed(Keys.X) || MainMenu.Pressed)
        {
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
        }     
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Controls/KeyboardControls"), new Vector2(-550, -380));
        MainMenu.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
        MainMenu.Update(gameTime);
    }
    public virtual void Reset()
    {
    }

}

