using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Room : GameObjectList
{

    public bool updoor = false, downdoor = false, leftdoor = false, rightdoor = false, start = true, Visited = false, CameraIsMoving = false;
    int CellWidth, CellHeight, roomwidth, roomheight, roomarraywidth, roomarrayheight, counter;
    bool onup = false, ondown = false, onleft = false, onright = false;
    Vector2 MiddelofPlayer, Up, Down, Left, Right, Exit;
    public static GameObjectList enemies, solid, door;
    public int RoomListIndex, a, b;
    public string[,] roomarray;

    public Room(int roomListIndex, int A, int B, int layer = 0, string id = "") : base(layer)
    {
        enemies = new GameObjectList();
        solid = new GameObjectList();
        door = new GameObjectList();
        RoomListIndex = roomListIndex;
        a = A;
        b = B;
    }

    public void LoadTiles()
    {
        StreamReader fileReader = new StreamReader("Content/Levels/" + RoomListIndex + ".txt");
        string line = fileReader.ReadLine();
        List<string> textLines = new List<string>();

        CellWidth = GameEnvironment.assetManager.GetSprite("Sprites/Standardtile").Width;
        CellHeight = GameEnvironment.assetManager.GetSprite("Sprites/Standardtile").Height;

        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }
        line = textLines[0];
        roomarraywidth = line.Length;
        roomarrayheight = textLines.Count;
        roomwidth = line.Length * CellWidth;
        roomheight = textLines.Count * CellHeight;
        roomarray = new string[roomarraywidth, roomarrayheight];
        for (int x = 0; x < roomarraywidth; ++x)
            for (int y = 0; y < roomarrayheight; ++y)
                AssignType(textLines[y][x], x, y);
    }

    private void AssignType(char textlines, int x, int y)
    {
        switch (textlines)
        {
            case '.':
                roomarray[x, y] = "Background";
                break;
            case '!':
                roomarray[x, y] = "Rock";
                CreateObject(x, y, "!");
                break;
            case '+':
                roomarray[x, y] = "Wall";
                CreateObject(x, y, "+");
                break;
            case '-':
                roomarray[x, y] = "UpDoor";
                Up = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                CreateObject(x, y, "-");
                break;
            case '=':
                roomarray[x, y] = "DownDoor";
                Down = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                CreateObject(x, y, "=");
                break;
            case '<':
                roomarray[x, y] = "LeftDoor";
                Left = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                CreateObject(x, y, "<");
                break;
            case '>':
                roomarray[x, y] = "RightDoor";
                Right = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                CreateObject(x, y, ">");
                break;
            case 'C':
                roomarray[x, y] = "ChasingEnemy";
                CreateObject(x, y, "C");
                break;
            case 'R':
                roomarray[x, y] = "RangedEnemy";
                CreateObject(x, y, "R");
                break;
            case 'O':
                roomarray[x, y] = "Pit";
                break;
            case 'I':
                roomarray[x, y] = "Item";
                break;
            case 'E':
                roomarray[x, y] = "Exit";
                Exit = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                break;
            case 'S':
                roomarray[x, y] = "Start";
                break;
            default:
                roomarray[x, y] = "N/A";
                break;
        }
    }

    public void Update(GameTime gameTime, Room CurrentRoom)
    {
        if (CurrentRoom.position == new Vector2(a, b))        
            Visited = true;        
        //~Yoran~ Uitgecommentaard want geeft exeption. En is nu onnodig, dubbele enemy update
        if (start) { OnLoad(); }
        enemies.Update(gameTime);
        solid.Update(gameTime);
        door.Update(gameTime);
        ControlCamera();
        CheckExit();
    }

    void OnLoad()
    {
        //CreateEnemy();
        start = false;
    }

    public void CreateObject(int x, int y, string Type)
    {
        //Enemy enemyChase;
        //Enemy enemyRanged;
        //Enemy enemy;
        switch (Type)
        {
            case ("C"):
                Enemy enemyChase = new ChasingEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "ChasingEnemy");
                enemies.Add(enemyChase);
                break;

            case ("R"):
                Enemy enemyRanged = new RangedEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "RangedEnemy");
                enemies.Add(enemyRanged);
                break;

            case ("!"):
                Solid rock = new Rock(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "Rock");
                solid.Add(rock);
                break;

            case ("+"):
                Solid wall = new Wall(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "Wall");
                solid.Add(wall);
                break;

            case ("-"):
                    Door up = new Door(updoor, Up, 1);
                    door.Add(up);                
                break;

            case ("="):                
                    Door down = new Door(downdoor, Down, 2);
                    door.Add(down);                
                break;

            case ("<"):
                    Door left = new Door(leftdoor, Left, 3);
                    door.Add(left);                
                break;
            case (">"):
                    Door right = new Door(rightdoor, Right, 4);
                    door.Add(right);                
                break;
        }
    }

    void ControlCamera()
    {
        Vector2 Cam = Camera.Position;
        MiddelofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2);

        if (enemies.Count == 0)
        {
            if (updoor && MiddelofPlayer.X >= Up.X && MiddelofPlayer.X <= Up.X + CellWidth)
                if (MiddelofPlayer.Y >= Up.Y && MiddelofPlayer.Y <= Up.Y + CellHeight)
                {
                    onup = true;
                    PlayingState.player.position -= new Vector2(0, 2 * CellHeight);
                }

            if (downdoor && MiddelofPlayer.X >= Down.X && MiddelofPlayer.X <= Down.X + CellWidth)
                if (MiddelofPlayer.Y >= Down.Y && MiddelofPlayer.Y <= Down.Y + CellHeight)
                {
                    ondown = true;
                    PlayingState.player.position += new Vector2(0, 2 * CellHeight);
                }

            if (leftdoor && MiddelofPlayer.X >= Left.X && MiddelofPlayer.X <= Left.X + CellWidth)
                if (MiddelofPlayer.Y >= Left.Y && MiddelofPlayer.Y <= Left.Y + CellHeight)
                {
                    onleft = true;
                    PlayingState.player.position -= new Vector2(2 * CellHeight, 0);
                }

            if (rightdoor && MiddelofPlayer.X >= Right.X && MiddelofPlayer.X <= Right.X + CellWidth)
                if (MiddelofPlayer.Y >= Right.Y && MiddelofPlayer.Y <= Right.Y + CellHeight)
                {
                    onright = true;
                    PlayingState.player.position += new Vector2(2 * CellHeight, 0);
                }

            Vector2 CameraVelocity = new Vector2(0, 0);

            if (Camera.Position.Y > Cam.Y - roomheight && onup == true && counter < 30)
                CameraVelocity = new Vector2(0, -roomheight / 30);
            if (Camera.Position.Y < Cam.Y + roomheight && ondown == true && counter < 30)
                CameraVelocity = new Vector2(0, roomheight / 30);
            if (Camera.Position.X > Cam.X - roomwidth && onleft == true && counter < 30)
                CameraVelocity = new Vector2(-roomwidth / 30, 0);
            if (Camera.Position.Y < Cam.X + roomwidth && onright == true && counter < 30)
                CameraVelocity = new Vector2(roomwidth / 30, 0);

            if ((onup || ondown || onleft || onright) && counter < 30)
            {
                Camera.Position += CameraVelocity;
                counter++;
                CameraIsMoving = true;
            }
            if (counter >= 30)
            {
                onup = false;
                ondown = false;
                onleft = false;
                onright = false;
                counter = 0;
                CameraIsMoving = false;
            }
        }
    }

    public void CheckExit()
    {
        MiddelofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2);
        if (MiddelofPlayer.X >= Exit.X && MiddelofPlayer.X <= Exit.X + CellWidth)
            if (MiddelofPlayer.Y >= Exit.Y && MiddelofPlayer.Y <= Exit.Y + CellHeight)
                PlayingState.currentFloor.NextFloor();
    }            

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int x = 0; x < roomarray.GetLength(0); x++)
            for (int y = 0; y < roomarray.GetLength(1); y++)
                if (roomarray != null)
                    switch (roomarray[x, y])
                    {
                        case "Background":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;
                        case "Rock":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;
                        case "Wall":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;
                        case "Pit":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/PitTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.White);
                            break;
                        case "Item":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/ItemTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;
                        case "Exit":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/EndTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.White);
                            break;
                        case "Start":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/StartTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight - 120), Color.Gray);
                            //System.Console.WriteLine(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight).ToString());
                            PlayingState.currentFloor.startPlayerPosition = new Vector2(x * CellWidth + a * roomwidth + CellWidth / 2, y * CellHeight + b * roomheight + CellHeight / 2);
                            //Camera.Position = new Vector2(x * CellWidth + a * roomwidth + CellWidth / 2, y * CellHeight + b * roomheight + CellHeight / 2);
                            break;
                        case "RangedEnemy":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;
                        case "ChasingEnemy":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;
                        default:
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Red);
                            break;
                    }

        foreach (Enemy enemy in enemies.Children)        
            enemy.Draw(gameTime, spriteBatch); 
        foreach (Solid solid in solid.Children)        
            solid.Draw(gameTime, spriteBatch);       
        foreach (Door door in door.Children)
            door.Draw(gameTime, spriteBatch);
    }    
}