using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
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
    Vector2 exitposition, MiddleofPlayer;
    int pitroomnumber;
    int x = (int)PlayingState.currentFloor.currentRoom.position.X;
    int y = (int)PlayingState.currentFloor.currentRoom.position.Y;

    public PitState()
    {
        exitposition = new Vector2(10 * PlayingState.currentFloor.currentRoom.CellWidth + x * PlayingState.currentFloor.currentRoom.roomwidth, 2 * PlayingState.currentFloor.currentRoom.CellHeight + y * PlayingState.currentFloor.currentRoom.roomheight - 120);

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
        player2.position = new Vector2(10 * PlayingState.currentFloor.currentRoom.CellWidth + x * PlayingState.currentFloor.currentRoom.roomwidth, 7 * PlayingState.currentFloor.currentRoom.CellHeight + y * PlayingState.currentFloor.currentRoom.roomheight);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        PlayingState.currentFloor.floor[x, y].HandleInput(inputHelper);
        player2.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        PlayingState.currentFloor.floor[x, y].Update(gameTime);
        player2.Update(gameTime);
        PitExit(gameTime);
    }

    public void PitExit(GameTime gameTime)
    {
        MiddleofPlayer = new Vector2(player2.position.X + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2, player2.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2);

        if (PlayingState.currentFloor.floor[x, y].enemycounter == 0)
        {
            if (MiddleofPlayer.X >= exitposition.X && MiddleofPlayer.X <= exitposition.X + PlayingState.currentFloor.currentRoom.CellWidth
                && MiddleofPlayer.Y >= exitposition.Y + 120 && MiddleofPlayer.Y <= exitposition.Y + PlayingState.currentFloor.currentRoom.CellHeight + 120)
            {
                PlayingState.currentFloor.floor[x, y] = actualroom;
                PlayingState.player = player2;
                PlayingState.player.position = actualroom.LastEntryPoint;
                GameEnvironment.gameStateManager.SwitchTo("Playing");
            }
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        PlayingState.currentFloor.Draw(gameTime, spriteBatch);
        if (PlayingState.currentFloor.floor[x, y].enemycounter == 0)
        {
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/StartTile")), exitposition);
        }
        player2.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
    }
}