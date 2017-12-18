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
    //public static Enemy enemy;
    public static Floor currentFloor;
    Floor floor;
    Button button;


    public PlayingState()
    {
        player = new Player();
        floor = new Floor();
        currentFloor = floor;
        //button = new Button(new Vector2(100, 100), "Klik me!", "Sprites/Button Sprite", 0, "button");
        //enemy = new RangedEnemy(Vector2.Zero, 0, "Enemy");
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        player.HandleInput(inputHelper);
        if (inputHelper.currentKeyboardState.IsKeyDown(Keys.P) && inputHelper.previousKeyboardState.IsKeyUp(Keys.P))
        {
            GameEnvironment.gameStateManager.SwitchTo("PauseMenu");
        }
        floor.HandleInput(inputHelper);
        //button.HandleInput(inputHelper);
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        floor.Draw(gameTime, spriteBatch);
        player.Draw(gameTime, spriteBatch);
        //button.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        Console.WriteLine(player.position.ToString());
        floor.Update(gameTime);
        //button.Update(gameTime);
        //enemy.Update(gameTime);
    }
    public virtual void Reset()
    {
    }
}

