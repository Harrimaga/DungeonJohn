using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

public class Floor
{
    Room[,] floor;
    bool[,] Checked;
    int[,] AdjacentRooms;
    int[,] possiblechoice;
    int[,] backupchoice;
    int maxRooms = 5, minRooms = 5, floorWidth = 9, floorHeight = 9, CurrentLevel = 1, CurrentRooms, b = 0, q;
    Random random = new Random();
    public Vector2 startPlayerPosition;
    public Room currentRoom;
    bool FloorGenerated = false;

    public Floor()
    {
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        possiblechoice = new int[floorWidth * floorHeight / 2, 2];
        backupchoice = new int[floorWidth * floorHeight / 2, 2];
        FloorGenerator();
    }

    void FloorGenerator()
    {
        ClearFloor();
        int RoomAmount = random.Next(maxRooms - minRooms + 1) + minRooms;
        int x = random.Next(floorWidth - 2) + 2;
        int y = random.Next(floorHeight - 2) + 2;
        floor[x, y] = new Room(1);
        currentRoom = floor[x, y];
        //System.Console.WriteLine(currentRoom.position.ToString());
        FloorGeneratorRecursive(x, y, RoomAmount);
        ChooseSpecialRoom(2);
        ChooseSpecialRoom(3);
        //FloorGenerated = true;
    }

    void FloorGeneratorRecursive(int x, int y, int RoomAmount)
    {
        if (y + 1 < floorHeight)
        {
            if (CurrentRooms < RoomAmount && floor[x, y + 1] == null && random.Next(100) <= CheckAdjacent(x, y + 1))
            {
                CurrentRooms++;
                floor[x, y + 1] = new Room(4);
            }
            else
                Checked[x, y + 1] = true;
        }

        if (y - 1 >= 0)
        {
            if (CurrentRooms < RoomAmount && floor[x, y - 1] == null && random.Next(100) <= CheckAdjacent(x, y - 1))
            {
                CurrentRooms++;
                floor[x, y - 1] = new Room(4);
            }
            else
                Checked[x, y - 1] = true;
        }

        if (x + 1 < floorWidth)
        {
            if (CurrentRooms < RoomAmount && floor[x + 1, y] == null && random.Next(100) <= CheckAdjacent(x + 1, y))
            {
                CurrentRooms++;
                floor[x + 1, y] = new Room(4);
            }
            else
                Checked[x + 1, y] = true;
        }

        if (x - 1 >= 0)
        {
            if (CurrentRooms < RoomAmount && floor[x - 1, y] == null && random.Next(100) <= CheckAdjacent(x - 1, y))
            {
                CurrentRooms++;
                floor[x - 1, y] = new Room(4);
            }
            else
                Checked[x - 1, y] = true;
        }

        Checked[x, y] = true;
        int counter = 0;
        if (CurrentRooms < RoomAmount)
        {
            for (int m = 0; m < floorWidth; m++)
                for (int n = 0; n < floorHeight; n++)
                    if (floor[m, n] != null && Checked[m, n] == false)
                    {
                        FloorGeneratorRecursive(m, n, RoomAmount);
                        counter++;
                    }
            if (counter == 0)
            {
                ClearFloor();
                RoomAmount = random.Next(maxRooms - minRooms + 1) + minRooms;
                x = random.Next(floorWidth - 2) + 2;
                y = random.Next(floorHeight - 2) + 2;
                floor[x, y] = new Room(1);
                FloorGeneratorRecursive(x, y, RoomAmount);
            }                       
        }
    }

    void ChooseSpecialRoom(int Index)
    {
        if (b == 0)
        {
            for (int x = 0; x < floorWidth; x++)
                for (int y = 0; y < floorHeight; y++)
                {
                    CheckAdjacent(x, y);
                    if (AdjacentRooms[x, y] == 1 && CanSpawnSpecialRoom(x, y) == true)                    
                        if (floor[x, y] == null)
                        {
                            backupchoice[b, 0] = x;
                            backupchoice[b, 1] = y;
                            b++;
                        }                    
                }
        }  
            q = random.Next(b - 1);
            floor[backupchoice[q, 0], backupchoice[q, 1]] = new Room(Index);
            if (q != 0)
            {
                backupchoice[q, 0] = backupchoice[q - 1, 0];
                backupchoice[q, 1] = backupchoice[q - 1, 1];
            }
            else
            {
                backupchoice[q, 0] = backupchoice[q + 1, 0];
                backupchoice[q, 1] = backupchoice[q + 1, 1];
            }        
    }

    bool CanSpawnSpecialRoom(int x, int y)
    {
        int counter = 0;
        if (x + 1 >= floorWidth)
            counter++;
        else if (floor[x + 1, y] == null || floor[x + 1, y].RoomListIndex >= 4)
            counter++;
        if (x - 1 < 0)
            counter++;
        else if (floor[x - 1, y] == null || floor[x - 1, y].RoomListIndex >= 4)
            counter++;
        if (y + 1 > -floorHeight)
            counter++;
        else if (floor[x, y + 1] == null || floor[x, y + 1].RoomListIndex >= 4)
            counter++;
        if (y - 1 < 0)
            counter++;
        else if (floor[x, y - 1] == null || floor[x, y - 1].RoomListIndex >= 4)
            counter++;
        if (counter == 4)
            return true;
        return false;
    }

    int CheckAdjacent(int x, int y)
    {
        int RoomSpawnChance = 30;
        int neighbours = 0;
        if (y + 1 < floorHeight && floor[x, y + 1] != null)
        {
            RoomSpawnChance = RoomSpawnChance / 12 * 10;
            neighbours++;
        }
        if (y - 1 >= 0 && floor[x, y - 1] != null)
        {
            RoomSpawnChance = RoomSpawnChance / 12 * 10;
            neighbours++;
        }
        if (x + 1 < floorWidth && floor[x + 1, y] != null)
        {
            RoomSpawnChance = RoomSpawnChance / 12 * 10;
            neighbours++;
        }
        if (x - 1 >= 0 && floor[x - 1, y] != null)
        {
            RoomSpawnChance = RoomSpawnChance / 12 * 10;
            neighbours++;
        }
        AdjacentRooms[x, y] = neighbours;
        return RoomSpawnChance * 12 / 10;
    }
    
    void ClearFloor()
    {
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
            {
                floor[x, y] = null;
                Checked[x, y] = false;
                AdjacentRooms[x, y] = 0;
            }
        b = 0;
        CurrentRooms = 1;
    }

    void NextFloor()
    {
        ClearFloor();
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        //floorWidth = floor.GetLength(0);
        //floorHeight = floor.GetLength(1);
        maxRooms += 3;
        minRooms += 3;
        FloorGenerator();
        CurrentLevel++;
        FloorGenerated = false;
        //Camera.Position = currentRoom.position /* + new Vector2(25, 25)*/;
    }

    //void DoorCheck()
    //{
    //    if (FloorGenerated == true)
    //    {
    //        for (int x = 0; x < 9; x++)
    //            for (int y = 0; y < 9; y++)
    //                if (floor[x, y] != null)
    //                {
    //                    if (x + 1 < 9 && floor[x + 1, y] != null)
    //                        floor[x, y].right = true;
    //                    if (x - 1 >= 0 && floor[x - 1, y] != null)
    //                        floor[x, y].left = true;
    //                    if (y + 1 < 9 && floor[x, y + 1] != null)
    //                        floor[x, y].down = true;
    //                    if (y - 1 >= 0 && floor[x, y - 1] != null)
    //                        floor[x, y].up = true;
    //                    FloorGenerated = false;
    //                }
    //    }
    //}

    public virtual void Update(GameTime gameTime)
    {
        //foreach (Room room in floor)
        //{
        //    if (room != null)
        //    {
        //        room.Update(gameTime);
        //    }
        //}
        ////TODO als nextFloor true is voer dan NextFloor() uit


        //Camera.Position = new Vector2(currentRoom.Position.X, currentRoom.position.Y);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.R))
            NextFloor();

        //if(inputHelper.IsKeyDown(Keys.Right))
        //{
        //    Camera.Position = new Vector2(Camera.Position.X + 10, Camera.Position.Y);
        //}
        //if (inputHelper.IsKeyDown(Keys.Left))
        //{
        //    Camera.Position = new Vector2(Camera.Position.X - 10, Camera.Position.Y);
        //}
        //if (inputHelper.IsKeyDown(Keys.Up))
        //{
        //    Camera.Position = new Vector2(Camera.Position.X, Camera.Position.Y - 10);
        //}
        //if (inputHelper.IsKeyDown(Keys.Down))
        //{
        //    Camera.Position = new Vector2(Camera.Position.X, Camera.Position.Y + 10);
        //}
    }
    
    void DrawMinimap(SpriteBatch spriteBatch)
    {

        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), Vector2.Zero, Color.White);

        int FloorCellWidth = 15;
        int FloorCellHeight = 15;
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
                if (floor[x, y] != null)
                {
                    if (floor[x, y].RoomListIndex == 1)
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(GameEnvironment.WindowSize.X - 200 + x * (FloorCellWidth + 2) + (Camera.Position.X - GameEnvironment.WindowSize.X / 2), y * (FloorCellHeight + 2) + (Camera.Position.Y - GameEnvironment.WindowSize.Y / 2)), Color.Lime);
                        //Console.WriteLine(new Vector2(600 + x * (FloorCellWidth + 2) + Camera.Position.X, y * (FloorCellHeight + 2) + Camera.Position.Y).ToString());
                    }
                    else if (floor[x, y].RoomListIndex == 2)
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(GameEnvironment.WindowSize.X - 200 + x * (FloorCellWidth + 2) + (Camera.Position.X - GameEnvironment.WindowSize.X / 2), y * (FloorCellHeight + 2) + (Camera.Position.Y - GameEnvironment.WindowSize.Y / 2)), Color.Blue);
                    }
                    else if (floor[x, y].RoomListIndex == 3)
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(GameEnvironment.WindowSize.X - 200 + x * (FloorCellWidth + 2) + (Camera.Position.X - GameEnvironment.WindowSize.X / 2), y * (FloorCellHeight + 2) + (Camera.Position.Y - GameEnvironment.WindowSize.Y / 2)), Color.Yellow);
                    }
                    else
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(GameEnvironment.WindowSize.X - 200 + x * (FloorCellWidth + 2) + (Camera.Position.X - GameEnvironment.WindowSize.X / 2), y * (FloorCellHeight + 2) + (Camera.Position.Y - GameEnvironment.WindowSize.Y / 2)), Color.Red);
                    }
                }
        //TODO alleen kamer tekenen op minimap als de speler er is geweest
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
            for (int a = 0; a < floorWidth; a++)
                for (int b = 0; b < floorHeight; b++)
                    if (floor[a, b] != null)
                    {
                        floor[a, b].LoadTiles();
                        floor[a, b].Draw(gameTime, spriteBatch, a, b);
                    }
        //FloorGenerated = false;

        if (FloorGenerated == false)
        {
            PlayingState.player.position = startPlayerPosition - new Vector2(23, 22);
            Camera.Position = startPlayerPosition;
            FloorGenerated = true;
        }

            DrawMinimap(spriteBatch);
    }
}

