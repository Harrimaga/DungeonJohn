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
    Floor floor;
    
    public PlayingState()
    {
        player = new Player();
        floor = new Floor();
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        player.Draw(gameTime, spriteBatch);
        floor.Draw(gameTime, spriteBatch);  
    }
    public virtual void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        floor.Update(gameTime);
    }
    public virtual void Reset()
    {
    }
}

