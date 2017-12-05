//using DeGame.GameManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayingState : IGameObject
{
    Player player;
    Enemy enemy;
    
    public PlayingState()
    {
        player = new Player();
        enemy = new Enemy();
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        player.HandleInput(inputHelper);
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Begin();
        player.Draw(gameTime, spriteBatch);
        enemy.Draw(gameTime, spriteBatch);
        //spriteBatch.End();
    }
    public virtual void Update(GameTime gameTime)
    {
    }
    public virtual void Reset()
    {
    }
}

