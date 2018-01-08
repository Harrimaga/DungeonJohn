using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PitState : IGameObject 
{
    Random r;
    Room pitroom;
    Player player2;
    int pitroomnumber;

    public PitState()
    {
        r = new Random();
        int random = r.Next(100);
        if (random < 20)
            pitroomnumber = 111;
        else if (random < 40)
            pitroomnumber = 222;
        else
            pitroomnumber = 333;

        pitroom = new Room(pitroomnumber, (int)PlayingState.currentFloor.currentRoom.position.X, (int)PlayingState.currentFloor.currentRoom.position.Y);
        player2 = PlayingState.player;
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        if(inputHelper.KeyPressed(Keys.H))
        {
            GameEnvironment.gameStateManager.SwitchTo("Playing");
            PlayingState.player = player2;
        }
        player2.HandleInput(inputHelper);
    }

    public virtual void Update(GameTime gameTime)
    {
        pitroom.Update(gameTime);
        player2.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        pitroom.LoadTiles();
        pitroom.Draw(gameTime, spriteBatch);
        player2.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
    }
}