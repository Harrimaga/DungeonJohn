using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayingState : IGameObject
{
    public static Player player;
    public static Floor currentFloor;
    Floor floor;

    public PlayingState()
    {
        player = new Player();
        floor = new Floor();
        currentFloor = floor;
        GameEnvironment.gameStateManager.LastState = "playing";
        Player.inventory.currentWeapon = new BigMac();
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        player.HandleInput(inputHelper, gameTime);
        if (inputHelper.KeyPressed(Keys.P) || inputHelper.ButtonPressed(Buttons.Start))
        {
            GameEnvironment.gameStateManager.SwitchTo("PauseMenu");
        }
        floor.HandleInput(inputHelper, gameTime);        
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        floor.Draw(gameTime, spriteBatch);
        player.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        floor.Update(gameTime);
        if (player.health <= 0)
        {
            GameEnvironment.gameStateManager.SwitchTo("GameOver");
        }
        if (currentFloor.CurrentLevel >= 10)
        {
            GameEnvironment.gameStateManager.SwitchTo("Victory");
        }
        currentFloor.wornItems.Update(gameTime);
    }
    public virtual void Reset()
    {
    }
}

