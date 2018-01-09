using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Crafting : IGameObject
{
    
    public Crafting()
    {
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        if(inputHelper.IsKeyDown(Keys.Space))
        {
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        GameEnvironment.gameStateManager.GetGameState("Playing").Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
    }
    public virtual void Reset()
    {
    }

}
