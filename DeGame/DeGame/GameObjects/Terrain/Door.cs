using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Door : Solid
{
    bool onup = false, ondown = false, onleft = false, onright = false, closed = false;
    SpriteEffects Effect = SpriteEffects.None;
    Texture2D doorsprite;
    GameObjectList solid;
    int DoorNumber;
    int direction, counter = 0;
    int CellWidth = PlayingState.currentFloor.currentRoom.CellWidth;
    int CellHeight = PlayingState.currentFloor.currentRoom.CellHeight;
    int roomwidth = PlayingState.currentFloor.currentRoom.roomwidth;
    int roomheight = PlayingState.currentFloor.currentRoom.roomheight;

    public Door(int doornumber, Vector2 Startposition, int Direction, int layer = 0, string id = "door")
    : base(Startposition, layer, id)
    {
        solid = new GameObjectList();
        position = Startposition;
        direction = Direction;
        DoorNumber = doornumber;
    }

    void ChooseSprite()
    {
        closed = false;
        if (direction == 1 || direction == 2)
        {
            if (direction == 2)
            {
                Effect = SpriteEffects.FlipVertically;
            }
            if (PlayingState.currentFloor.currentRoom.enemycounter > 0 || PlayingState.currentFloor.doortimer > 0)
            {
                switch(DoorNumber)
                {
                    case (1):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/doorupclosed");
                        break;
                    case (2):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Bossdoorupclosed");
                        break;
                    case (3):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Itemdoorupclosed");
                        break;
                }
                closed = true;
            }
            else
                switch (DoorNumber)
                {
                    case (1):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/doorup");
                        break;
                    case (2):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Bossdoorup");
                        break;
                    case (3):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Itemdoorup");
                        break;
                }
        }
        else
        {
            if (direction == 4)
            {
                Effect = SpriteEffects.FlipHorizontally;
            }
            if (PlayingState.currentFloor.currentRoom.enemycounter > 0 || PlayingState.currentFloor.doortimer > 0)
            {
                switch (DoorNumber)
                {
                    case (1):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/doorleftclosed");
                        break;
                    case (2):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Bossdoorleftclosed");
                        break;
                    case (3):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Itemdoorleftclosed");
                        break;
                }
                closed = true;
            }
            else
                switch (DoorNumber)
                {
                    case (1):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/doorleft");
                        break;
                    case (2):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Bossdoorleft");
                        break;
                    case (3):
                        doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Itemdoorleft");
                        break;
                }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (CellWidth == 0)
        {
            CellWidth = PlayingState.currentFloor.currentRoom.CellWidth;
            CellHeight = PlayingState.currentFloor.currentRoom.CellHeight;
            roomwidth = PlayingState.currentFloor.currentRoom.roomwidth;
            roomheight = PlayingState.currentFloor.currentRoom.roomheight;
        }
        if (PlayingState.currentFloor.currentRoom.enemycounter > 0 || DoorNumber == 0 || closed)
        {
            base.Update(gameTime);
        }
        if (DoorNumber > 0)
        {
            ControlCamera();
        }
    }

    void ControlCamera()
    {
        Vector2 Cam = Camera.Position;
        if (PlayingState.currentFloor.currentRoom.enemycounter == 0 && CollidesWith(PlayingState.player) && PlayingState.currentFloor.doortimer == 0)
            switch (direction)
            {
                case (1):
                    PlayingState.player.position -= new Vector2(0, 3 * CellHeight);
                    onup = true;
                    PlayingState.currentFloor.doortimer = 50;
                    break;
                case (2):
                    PlayingState.player.position += new Vector2(0, 3 * CellHeight + 40);
                    ondown = true;
                    PlayingState.currentFloor.doortimer = 50;
                    break;
                case (3):
                    PlayingState.player.position -= new Vector2(3 * CellHeight, 0);
                    onleft = true;
                    PlayingState.currentFloor.doortimer = 50;
                    break;
                case (4):
                    PlayingState.player.position += new Vector2(3 * CellHeight + 40, 0);
                    onright = true;
                    PlayingState.currentFloor.doortimer = 50;
                    break;
                default:
                    break;
            }

        Vector2 CameraVelocity = new Vector2(0, 0);

        if (direction == 1 && Camera.Position.Y > Cam.Y - roomheight && onup == true && counter < 30)
        {
            CameraVelocity = new Vector2(0, -roomheight / 30);
            PlayingState.currentFloor.currentRoom.CameraMoving = true;
        }
        else if (direction == 2 && Camera.Position.Y < Cam.Y + roomheight && ondown == true && counter < 30)
        {
            CameraVelocity = new Vector2(0, roomheight / 30);
            PlayingState.currentFloor.currentRoom.CameraMoving = true;
        }
        else if (direction == 3 && Camera.Position.X > Cam.X - roomwidth && onleft == true && counter < 30)
        {
            CameraVelocity = new Vector2(-roomwidth / 30, 0);
            PlayingState.currentFloor.currentRoom.CameraMoving = true;
        }
        else if (direction == 4 && Camera.Position.X < Cam.X + roomwidth && onright == true && counter < 30)
        {
            CameraVelocity = new Vector2(roomwidth / 30, 0);
            PlayingState.currentFloor.currentRoom.CameraMoving = true;
        }

        if ((onup || ondown || onleft || onright) && counter < 30)
        {
            Camera.Position += CameraVelocity;
            counter++;
        }

        if (counter >= 30)
        {
            onup = false;
            ondown = false;
            onleft = false;
            onright = false;
            counter = 0;
            PlayingState.currentFloor.currentRoom.CameraMoving = false;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (DoorNumber > 0)
        {
            ChooseSprite();
            spriteBatch.Draw(doorsprite, position, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
        }
    }
}
