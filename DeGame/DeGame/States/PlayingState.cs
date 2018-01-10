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

    public PlayingState()
    {
        player = new Player();
        floor = new Floor();
        currentFloor = floor;
        Player.inventory.items.Add(new StandardBow());
        Player.inventory.items.Add(new StandardBow());
        Player.inventory.items.Add(new CoolBoots());
        //Player.inventory.items.Add(new Mirror());
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
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        floor.Draw(gameTime, spriteBatch);
        player.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        Console.WriteLine(player.position.ToString());
        floor.Update(gameTime);
        //enemy.Update(gameTime);
        if (player.health <= 0)
        {
            GameEnvironment.gameStateManager.SwitchTo("GameOver");
        }
    }
    public virtual void Reset()
    {
    }
}

