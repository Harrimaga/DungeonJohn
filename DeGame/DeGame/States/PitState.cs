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
    Vector2 exitposition, MiddleofPlayer;
    protected IGameObject playingState;
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
            pitroomnumber = 222;
        else
            pitroomnumber = 333;

        actualroom = PlayingState.currentFloor.floor[x, y];
        PlayingState.currentFloor.floor[x, y] = new Room(pitroomnumber, x, y);
        PlayingState.currentFloor.floor[x, y].LoadTiles();
        player2 = PlayingState.player;
        player2.position = new Vector2(10 * PlayingState.currentFloor.currentRoom.CellWidth + x * PlayingState.currentFloor.currentRoom.roomwidth, 7 * PlayingState.currentFloor.currentRoom.CellHeight + y * PlayingState.currentFloor.currentRoom.roomheight);
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        PlayingState.currentFloor.floor[x, y].HandleInput(inputHelper);
        player2.HandleInput(inputHelper);
    }

    public virtual void Update(GameTime gameTime)
    {
        PlayingState.currentFloor.floor[x, y].Update(gameTime);
        player2.Update(gameTime);
        MiddleofPlayer = new Vector2(player2.position.X + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2, player2.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2);

        if (PlayingState.currentFloor.floor[x, y].enemycounter == 0)
        {
            if(MiddleofPlayer.X >= exitposition.X && MiddleofPlayer.X <= exitposition.X + PlayingState.currentFloor.currentRoom.CellWidth
                && MiddleofPlayer.Y >= exitposition.Y + 120 && MiddleofPlayer.Y <= exitposition.Y + PlayingState.currentFloor.currentRoom.CellHeight + 120)
                {
                    GameEnvironment.gameStateManager.SwitchTo("Playing");
                    PlayingState.currentFloor.floor[x, y] = new Room(actualroom.RoomListIndex, x, y);
                    PlayingState.currentFloor.DoorCheck();

                    for (int a = 0; a < 9; a++)
                        for (int b = 0; b < 9; b++)
                            if (PlayingState.currentFloor.floor[a, b] != null)
                                PlayingState.currentFloor.floor[a, b].LoadTiles();

                    PlayingState.player = player2;
                    PlayingState.player.position = new Vector2(10 * PlayingState.currentFloor.currentRoom.CellWidth + x * PlayingState.currentFloor.currentRoom.roomwidth, 7 * PlayingState.currentFloor.currentRoom.CellHeight + y * PlayingState.currentFloor.currentRoom.roomheight);
                }
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        PlayingState.currentFloor.floor[x, y].Draw(gameTime, spriteBatch);

        if (PlayingState.currentFloor.floor[x, y].enemycounter == 0)
        {
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/StartTile")), exitposition);
        }

        player2.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
    }
}