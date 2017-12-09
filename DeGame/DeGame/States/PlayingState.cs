using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        enemy = new Enemy();
        floor = new Floor();

    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        player.HandleInput(inputHelper);
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

