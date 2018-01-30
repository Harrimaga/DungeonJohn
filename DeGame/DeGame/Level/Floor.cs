using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

public class Floor
{
    public int maxRooms = 7, minRooms = 5, floorWidth = 9, floorHeight = 9, CurrentRooms, b = 0, q;
    public int CurrentLevel = 1, doortimer = 0, displayint = 1;
    int[,] possiblespecial, AdjacentRooms;
    public Room currentRoom;
    public Room[,] floor;
    public bool FloorGenerated = false;
    bool[,] Checked;
    public Vector2 startPlayerPosition;
    Random random = new Random();
    public string Level;

    public Floor()
    {
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        possiblespecial = new int[floorWidth * floorHeight / 2, 2];
    }
    /// <summary>
    /// Creates a new floor with boss-, start- and itemrooms
    /// </summary>
    void FloorGenerator()
    {
        ClearFloor();
        int RoomAmount = random.Next(maxRooms - minRooms + 1) + minRooms;
        int x = random.Next(floorWidth - 2) + 2;
        int y = random.Next(floorHeight - 2) + 2;
        floor[x, y] = new Room("", 1, x, y);
        currentRoom = floor[x, y];
        FloorGeneratorRecursive(x, y, RoomAmount);
        SpawnItemRoom();
        if (CurrentLevel >= 7)
            SpawnItemRoom();
        SpawnBossRoom(x, y);
        DoorCheck();
        FloorGenerated = false;
    }

    /// <summary>
    /// Return an int that decides which room will be used, filters out any special rooms
    /// </summary>
    int RandomRoom()
    {
        return random.Next(31) + 5;
    }

    /// <summary>
    /// Checks on each side of a given coordinate in the floor grid if a new room should be placed,
    /// then repeats the method on placed rooms until a desired amount of rooms is reached
    /// </summary>
    void FloorGeneratorRecursive(int x, int y, int RoomAmount)
    {
        if (CurrentRooms < RoomAmount)
        {
            if (y + 1 < floorHeight)
            {
                if (floor[x, y + 1] == null && random.Next(100) <= CheckAdjacent(x, y + 1))
                {
                    CurrentRooms++;
                    floor[x, y + 1] = new Room("", RandomRoom(), x, y + 1);
                }
                else
                    Checked[x, y + 1] = true;
            }

            if (y - 1 >= 0)
            {
                if (floor[x, y - 1] == null && random.Next(100) <= CheckAdjacent(x, y - 1))
                {
                    CurrentRooms++;
                    floor[x, y - 1] = new Room("", RandomRoom(), x, y - 1);
                }
                else
                    Checked[x, y - 1] = true;
            }

            if (x + 1 < floorWidth)
            {
                if (floor[x + 1, y] == null && random.Next(100) <= CheckAdjacent(x + 1, y))
                {
                    CurrentRooms++;
                    floor[x + 1, y] = new Room("", RandomRoom(), x + 1, y);
                }
                else
                    Checked[x + 1, y] = true;
            }

            if (x - 1 >= 0)
            {
                if (floor[x - 1, y] == null && random.Next(100) <= CheckAdjacent(x - 1, y))
                {
                    CurrentRooms++;
                    floor[x - 1, y] = new Room("", RandomRoom(), x - 1, y);
                }
                else
                    Checked[x - 1, y] = true;
            }
        }

        Checked[x, y] = true;
        int counter = 0;
        if (CurrentRooms < RoomAmount)
        {
            for (int m = floorWidth - 1; m >= 0; m--)
                for (int n = floorHeight - 1; n >= 0; n--)
                    if (floor[m, n] != null && !Checked[m, n])
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
                floor[x, y] = new Room("", 1, x, y);
                currentRoom = floor[x, y];
                FloorGeneratorRecursive(x, y, RoomAmount);
            }
        }
    }

    /// <summary>
    /// Composes a list of al possible places a itemroom could be placed at, then chooses one randomly
    /// </summary>
    void SpawnItemRoom()
    {
        if (b == 0)
        {
            for (int x = 0; x < floorWidth; x++)
                for (int y = 0; y < floorHeight; y++)
                    if (CanSpawnSpecialRoom(x, y))
                    {
                        possiblespecial[b, 0] = x;
                        possiblespecial[b, 1] = y;
                        b++;
                    }
            q = random.Next(b);
        }
        else
        {
            q = random.Next(b);
            CheckAdjacent(possiblespecial[q, 0], possiblespecial[q, 1]);
            while (AdjacentRooms[possiblespecial[q, 0], possiblespecial[q, 1]] != 1)
            {
                q = random.Next(b);
                CheckAdjacent(possiblespecial[q, 0], possiblespecial[q, 1]);
            }
        }
        floor[possiblespecial[q, 0], possiblespecial[q, 1]] = new Room("", 3, possiblespecial[q, 0], possiblespecial[q, 1]);
        possiblespecial[q, 0] = possiblespecial[b, 0];
        possiblespecial[q, 1] = possiblespecial[b, 1];
    }

    /// <summary>
    /// Uses a place from the beforementioned list that is the farthest away from the startroom
    /// </summary>
    void SpawnBossRoom(int x, int y)
    {
        int DistancefromStart = 0;
        int bossx = 4, bossy = 4;
        bool changed = false;
        for (int a = 0; a <= b; a++)
            if (CanSpawnSpecialRoom(possiblespecial[a, 0], possiblespecial[a, 1]) && Math.Abs(x - possiblespecial[a, 0]) + Math.Abs(y - possiblespecial[a, 1]) > DistancefromStart)
            {
                bossx = possiblespecial[a, 0];
                bossy = possiblespecial[a, 1];
                DistancefromStart = Math.Abs(x - possiblespecial[a, 0]) + Math.Abs(y - possiblespecial[a, 1]);
                changed = true;
            }
        if (changed)
            floor[bossx, bossy] = new EndRoom("", 2, bossx, bossy);
        else
        {
            ClearFloor();
            FloorGenerator();
        }
    }

    /// <summary>
    /// Checks if the given coordinate is adjacent to any other special room or has more than 1 neighbour
    /// </summary>
    bool CanSpawnSpecialRoom(int x, int y)
    {
        if (floor[x, y] == null)
        {
            CheckAdjacent(x, y);
            if (AdjacentRooms[x, y] == 1)
            {
                if (CheckNeighbours(x, y, true) && floor[x, y - 1].RoomListIndex <= 3)
                    return false;
                if (CheckNeighbours(x, y, false, true) && (floor[x, y + 1].RoomListIndex <= 3))
                    return false;
                if (CheckNeighbours(x, y, false, false, true) && floor[x - 1, y].RoomListIndex <= 3)
                    return false;
                if (CheckNeighbours(x, y, false, false, false, true) && floor[x + 1, y].RoomListIndex <= 3)
                    return false;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Hands info to each room about what kind of doors it should have
    /// </summary>
    void DoorCheck()
    {
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
                if (floor[x, y] != null)
                {
                    if (CheckNeighbours(x, y, true))
                        switch (floor[x, y - 1].RoomListIndex)
                        {
                            case (2):
                                floor[x, y].updoor = 2;
                                break;
                            case (3):
                                floor[x, y].updoor = 3;
                                break;
                            default:
                                floor[x, y].updoor = 1;
                                break;
                        }
                    if (CheckNeighbours(x, y, false, true))
                        switch (floor[x, y + 1].RoomListIndex)
                        {
                            case (2):
                                floor[x, y].downdoor = 2;
                                break;
                            case (3):
                                floor[x, y].downdoor = 3;
                                break;
                            default:
                                floor[x, y].downdoor = 1;
                                break;
                        }
                    if (CheckNeighbours(x, y, false, false, true))
                        switch (floor[x - 1, y].RoomListIndex)
                        {
                            case (2):
                                floor[x, y].leftdoor = 2;
                                break;
                            case (3):
                                floor[x, y].leftdoor = 3;
                                break;
                            default:
                                floor[x, y].leftdoor = 1;
                                break;
                        }
                    if (CheckNeighbours(x, y, false, false, false, true))
                        switch (floor[x + 1, y].RoomListIndex)
                        {
                            case (2):
                                floor[x, y].rightdoor = 2;
                                break;
                            case (3):
                                floor[x, y].rightdoor = 3;
                                break;
                            default:
                                floor[x, y].rightdoor = 1;
                                break;
                        }
                }
    }

    /// <summary>
    /// Checks the amount of neighbours a coordinate has and what the change should be that a new room would appear that location
    /// </summary>
    int CheckAdjacent(int x, int y)
    {
        int RoomSpawnChance = 80;
        int SpawnChanceReduction = 20;
        int neighbours = 0;
        if (CheckNeighbours(x, y, true))
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (CheckNeighbours(x, y, false, true))
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (CheckNeighbours(x, y, false, false, true))
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (CheckNeighbours(x, y, false, false, false, true))
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
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
    }

    /// <summary>
    /// Makes sure only rooms containing the player or rooms directly next to the player are updated
    /// </summary>
    public void Update(GameTime gameTime)
    {
        foreach (Room room in floor)
            if (room != null)
            {
                if (room.position == currentRoom.position)
                {
                    room.Update(gameTime);
                }
                else if (room.position == currentRoom.position + new Vector2(1, 0) || room.position == currentRoom.position - new Vector2(1, 0)
                || room.position == currentRoom.position + new Vector2(0, 1) || room.position == currentRoom.position - new Vector2(0, 1))
                {
                    room.enemybullets.Update(gameTime);
                    room.homingenemybullets.Update(gameTime);
                }
            }
        if (doortimer > 0)
        {
            doortimer--;
        }
    }

    public void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (inputHelper.KeyPressed(Keys.T))
            if (CurrentLevel % 2 == 0)
                NextFloor();
            else
                NextShop();
        if (inputHelper.KeyPressed(Keys.R))
            ResetFloor();
        foreach (Room r in floor)
        {
            if (r != null)
                r.HandleInput(inputHelper, gameTime);
        }
    }

    public void NextFloor()
    {
        ClearFloor();
        GameEnvironment.soundManager.PlaySong("Base");
        if (displayint <= 5)
        {
            maxRooms += 3;
            minRooms += 3;
        }
        FloorGenerator();
        CurrentLevel++;
        displayint++;
        Level = "Level: " + displayint;
    }

    public void NextShop()
    {
        ClearFloor();
        GameEnvironment.soundManager.PlaySong("Shop");
        floor[4, 4] = new Room("", 4, 4, 4);
        currentRoom = floor[4, 4];
        CurrentLevel++;
        Level = "Level: Shop after " + displayint;
        FloorGenerated = false;
    }

    /// <summary>
    /// Completely resets the game; puts you back at level 1
    /// </summary>
    public void ResetFloor()
    {
        ClearFloor();
        maxRooms = 7;
        minRooms = 5;
        CurrentLevel = 1;
        displayint = 1;
        Level = "Level: " + displayint;
        FloorGenerator();
        PlayingState.player.Reset();
    }

    /// <summary>
    /// Checks if a given coordinate has neighbours in specific places
    /// </summary>
    public bool CheckNeighbours(int x, int y, bool up = false, bool down = false, bool left = false, bool right = false)
    {
        int boolcounter = 0;
        int truecounter = 0;
        if (up)
        {
            boolcounter++;
            if (y - 1 >= 0 && floor[x, y - 1] != null)
                truecounter++;
        }
        if (down)
        {
            boolcounter++;
            if (y + 1 < floorHeight && floor[x, y + 1] != null)
                truecounter++;
        }
        if (left)
        {
            boolcounter++;
            if (x - 1 >= 0 && floor[x - 1, y] != null)
                truecounter++;
        }
        if (right)
        {
            boolcounter++;
            if (x + 1 < floorWidth && floor[x + 1, y] != null)
                truecounter++;
        }
        if (truecounter == boolcounter)
            return true;
        return false;
    }


    /// <summary>
    /// Makes sure only rooms around the player and the room with the player are drawn
    /// </summary>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (Room room in floor)
        {
            if (room != null)
            {
                if (!FloorGenerated)
                    room.LoadTiles();
                if (room != null && (room.position == currentRoom.position) || room.position == currentRoom.position + new Vector2(1, 0) || room.position == currentRoom.position - new Vector2(1, 0)
                || room.position == currentRoom.position + new Vector2(0, 1) || room.position == currentRoom.position - new Vector2(0, 1))
                    room.Draw(gameTime, spriteBatch);
            }
        }

        if (!FloorGenerated)
        {
            PlayingState.player.position = startPlayerPosition - new Vector2(23, 22);
            Camera.Position = startPlayerPosition + new Vector2(170, 0);
            FloorGenerated = true;
        }
    }
}

