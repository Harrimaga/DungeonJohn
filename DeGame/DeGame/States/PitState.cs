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
    Player player2;
    Room actualroom;
    protected IGameObject playingState;
    int pitroomnumber;
    int x = (int)PlayingState.currentFloor.currentRoom.position.X;
    int y = (int)PlayingState.currentFloor.currentRoom.position.Y;

    public PitState()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");

        r = new Random();
        int random = r.Next(100);
        if (random < 20)
            pitroomnumber = 111;
        else if (random < 50)
            pitroomnumber = 111;
        else
            pitroomnumber = 111;

        actualroom = PlayingState.currentFloor.floor[x, y];
        PlayingState.currentFloor.floor[x, y] = new Room(pitroomnumber, x, y);
        PlayingState.currentFloor.floor[x, y].LoadTiles();
        player2 = PlayingState.player;
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        if(inputHelper.KeyPressed(Keys.H))
        {
            GameEnvironment.gameStateManager.SwitchTo("Playing");
            PlayingState.currentFloor.floor[x, y] = new Room(actualroom.RoomListIndex, x, y);
            PlayingState.currentFloor.DoorCheck();

            for (int a = 0; a < 9; a++)
                for (int b = 0; b < 9; b++)
                    if(PlayingState.currentFloor.floor[a, b] != null)
                        PlayingState.currentFloor.floor[a, b].LoadTiles(); 

            PlayingState.player = player2;
        }
        PlayingState.currentFloor.floor[x, y].HandleInput(inputHelper);
        player2.HandleInput(inputHelper);
    }

    public virtual void Update(GameTime gameTime)
    {
        PlayingState.currentFloor.floor[x, y].Update(gameTime);
        player2.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        PlayingState.currentFloor.floor[x, y].Draw(gameTime, spriteBatch);
        player2.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
    }
}