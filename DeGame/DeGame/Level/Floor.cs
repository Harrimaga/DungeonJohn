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
    int[,] possiblespecial;
    int maxRooms = 5, minRooms = 5, floorWidth = 9, floorHeight = 9, CurrentLevel = 1, CurrentRooms, b = 0, q;
    Random random = new Random();
    bool FloorGenerated = false;

    public Floor()
    {
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        possiblechoice = new int[floorWidth * floorHeight / 2, 2];
        possiblespecial = new int[floorWidth * floorHeight / 2, 2];
        FloorGenerator();
    }

    void FloorGenerator()
    {
        ClearFloor();
        int RoomAmount = random.Next(maxRooms - minRooms + 1) + minRooms;
        int x = random.Next(floorWidth - 2) + 2;
        int y = random.Next(floorHeight - 2) + 2;
        floor[x, y] = new Room(1, x, y);
        FloorGeneratorRecursive(x, y, RoomAmount);
        ChooseSpecialRoom(2);
        ChooseSpecialRoom(3);
        if (CurrentLevel >= 7)
            ChooseSpecialRoom(3);
        FloorGenerated = true;
    }

    void FloorGeneratorRecursive(int x, int y, int RoomAmount)
    {
        if (y + 1 < floorHeight)
        {
            if (CurrentRooms < RoomAmount && floor[x, y + 1] == null && random.Next(100) <= CheckAdjacent(x, y + 1))
            {
                CurrentRooms++;
                floor[x, y + 1] = new Room(4, x, y + 1);
            }
            else
                Checked[x, y + 1] = true;
        }

        if (y - 1 >= 0)
        {
            if (CurrentRooms < RoomAmount && floor[x, y - 1] == null && random.Next(100) <= CheckAdjacent(x, y - 1))
            {
                CurrentRooms++;
                floor[x, y - 1] = new Room(4, x, y - 1);
            }
            else
                Checked[x, y - 1] = true;
        }

        if (x + 1 < floorWidth)
        {
            if (CurrentRooms < RoomAmount && floor[x + 1, y] == null && random.Next(100) <= CheckAdjacent(x + 1, y))
            {
                CurrentRooms++;
                floor[x + 1, y] = new Room(4, x + 1, y);
            }
            else
                Checked[x + 1, y] = true;
        }

        if (x - 1 >= 0)
        {
            if (CurrentRooms < RoomAmount && floor[x - 1, y] == null && random.Next(100) <= CheckAdjacent(x - 1, y))
            {
                CurrentRooms++;
                floor[x - 1, y] = new Room(4, x - 1,y);
            }
            else
                Checked[x - 1, y] = true;
        }

        Checked[x, y] = true;
        int counter = 0;
        if (CurrentRooms < RoomAmount)
        {
            for (int m = floorWidth - 1; m >= 0; m--)
                for (int n = floorHeight - 1; n >= 0; n--)
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
                floor[x, y] = new Room(1, x, y);
                FloorGeneratorRecursive(x, y, RoomAmount);
            }                       
        }
    }

    void ChooseSpecialRoom(int Index)
    {
        bool secondtime = false;
        if (b == 0)
        {
            for (int x = 0; x < floorWidth; x++)
                for (int y = 0; y < floorHeight; y++)
                {
                    CheckAdjacent(x, y);
                    if (AdjacentRooms[x, y] == 1 && CanSpawnSpecialRoom(x, y) == true)                    
                        if (floor[x, y] == null)
                        {
                            possiblespecial[b, 0] = x;
                            possiblespecial[b, 1] = y;
                            b++;
                        }                    
                }
        }  
        else
            secondtime = true;
        q = random.Next(b - 1);
        if (secondtime == true)
        {
            CheckAdjacent(possiblespecial[q, 0], possiblespecial[q, 1]);
            while (AdjacentRooms[possiblespecial[q, 0], possiblespecial[q, 1]] != 1)
            {
                q = random.Next(b - 1);
                CheckAdjacent(possiblespecial[q, 0], possiblespecial[q, 1]);
            }
        }
            floor[possiblespecial[q, 0], possiblespecial[q, 1]] = new Room(Index, possiblespecial[q, 0], possiblespecial[q, 1]);
            if (q != 0)
            {
                possiblespecial[q, 0] = possiblespecial[q - 1, 0];
                possiblespecial[q, 1] = possiblespecial[q - 1, 1];
            }
            else
            {
                possiblespecial[q, 0] = possiblespecial[q + 1, 0];
                possiblespecial[q, 1] = possiblespecial[q + 1, 1];
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
        int RoomSpawnChance = 70;
        int SpawnChanceReduction = 20;
        int neighbours = 0;
        if (y + 1 < floorHeight && floor[x, y + 1] != null)
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (y - 1 >= 0 && floor[x, y - 1] != null)
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (x + 1 < floorWidth && floor[x + 1, y] != null)
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (x - 1 >= 0 && floor[x - 1, y] != null)
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        AdjacentRooms[x, y] = neighbours;
        return RoomSpawnChance * SpawnChanceReduction / 10;
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
        if (CurrentLevel <= 10)
        {
            maxRooms += 3;
            minRooms += 3;
        }
        FloorGenerator();
        CurrentLevel++;
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
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.R))
            NextFloor();
    }
    
    void DrawMinimap(SpriteBatch spriteBatch)
    {
        int FloorCellWidth = 15;
        int FloorCellHeight = 15;
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
                if (floor[x, y] != null)
                {
                    if (floor[x, y].RoomListIndex == 1)
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(600 + x * (FloorCellWidth + 2), y * (FloorCellHeight + 2)), Color.Lime);
                    }
                    else if (floor[x, y].RoomListIndex == 2)
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(600 + x * (FloorCellWidth + 2), y * (FloorCellHeight + 2)), Color.Blue);
                    }
                    else if (floor[x, y].RoomListIndex == 3)
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(600 + x * (FloorCellWidth + 2), y * (FloorCellHeight + 2)), Color.Yellow);
                    }
                    else
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(600 + x * (FloorCellWidth + 2), y * (FloorCellHeight + 2)), Color.Red);
                    }
                }
        //TODO alleen kamer tekenen op minimap als de speler er is geweest
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        string Level = "5";
        spriteBatch.DrawString(Content.Load<SpriteFont>("SpelFont"), Level, new Vector2(500,600), Color.Black);
        for (int a = 0; a < floorWidth; a++)
            for (int b = 0; b < floorHeight; b++)
                if (floor[a, b] != null)
                {
                    if (FloorGenerated == true)
                    {
                        floor[a, b].LoadTiles();
                    }
                    floor[a, b].Draw(gameTime, spriteBatch, a, b);
                }
        FloorGenerated = false;
        DrawMinimap(spriteBatch);
    }
}

