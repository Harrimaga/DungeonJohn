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
    
    public PlayingState()
    {
        player = new Player();
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Begin();
        player.Draw(gameTime, spriteBatch);
        //spriteBatch.End();
    }
    public virtual void Update(GameTime gameTime)
    {
    }
    public virtual void Reset()
    {
    }
}

