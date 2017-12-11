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
    Enemy enemy;
    Floor floor;


    public PlayingState()
    {
        player = new Player();
        enemy = new ChasingEnemy();
        floor = new Floor();

    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        player.HandleInput(inputHelper);
        if (inputHelper.currentKeyboardState.IsKeyDown(Keys.P) && inputHelper.previousKeyboardState.IsKeyUp(Keys.P))
        {
            GameEnvironment.gameStateManager.SwitchTo("PauseMenu");
        }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        floor.Draw(gameTime, spriteBatch);
        player.Draw(gameTime, spriteBatch);
        enemy.Draw(gameTime, spriteBatch); 

    }
    public virtual void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        floor.Update(gameTime);
        enemy.Update(gameTime);
    }
    public virtual void Reset()
    {
    }
}

