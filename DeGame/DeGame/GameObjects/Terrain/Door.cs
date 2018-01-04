using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

class Door : Solid
{
    bool isdoor, onup = false, ondown = false, onleft = false, onright = false;
    SpriteEffects Effect = SpriteEffects.None;
    Texture2D doorsprite, wallsprite;
    GameObjectList solid;
    int direction, counter = 0, doortimer = 0;
    int CellWidth = PlayingState.currentFloor.currentRoom.CellWidth;
    int CellHeight = PlayingState.currentFloor.currentRoom.CellHeight;
    int roomwidth = PlayingState.currentFloor.currentRoom.roomwidth;
    int roomheight = PlayingState.currentFloor.currentRoom.roomheight;

    public Door(bool Isdoor, Vector2 Startposition, int Direction, int layer = 0, string id = "door")
    : base(Startposition, layer, id)
    {
        solid = new GameObjectList();
        position = Startposition;
        direction = Direction;
        isdoor = Isdoor;
    }

    void ChooseSprite()
    {
        if (direction == 1 || direction == 2)
        {
            if (direction == 2)
            {
                Effect = SpriteEffects.FlipVertically;
                wallsprite = GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Down2");
            }
            else
                wallsprite = GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Up2");
            if (PlayingState.currentFloor.currentRoom.enemycounter > 0)
                doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/doorupclosed");
            else
                doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/doorup");
        }
        else
        {
            if (direction == 4)
            {
                Effect = SpriteEffects.FlipHorizontally;
                wallsprite = GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Right2");
            }
            else
                wallsprite = GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Left2");
            if (PlayingState.currentFloor.currentRoom.enemycounter > 0)
                doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/doorleftclosed");
            else
                doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/doorleft");
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (PlayingState.currentFloor.currentRoom.enemycounter > 0 || !isdoor)
        {
            base.Update(gameTime);
        }
        //else
        ControlCamera();
    }

    void ControlCamera()
    {
        Vector2 Cam = Camera.Position;
        if (PlayingState.currentFloor.currentRoom.enemycounter == 0 && CollidesWith(PlayingState.player) && doortimer == 0)         
            switch (direction)
            {
                case (1):
                    PlayingState.player.position -= new Vector2(0, 5 * CellHeight);
                    onup = true;
                    doortimer = 200;
                    break;
                case (2):
                    PlayingState.player.position += new Vector2(0, 5 * CellHeight);
                    ondown = true;
                    doortimer = 200;
                    break;
                case (3):
                    PlayingState.player.position -= new Vector2(5 * CellHeight, 0);
                    onleft = true;
                    doortimer = 200;
                    break;
                case (4):
                    PlayingState.player.position += new Vector2(5 * CellHeight, 0);
                    onright = true;
                    doortimer = 200;
                    break;
                default:
                    break;
            }
        if (doortimer > 0)
            doortimer--;

        Camera.Position = PlayingState.player.position;

        //Vector2 CameraVelocity = new Vector2(0, 0);

        //if (direction == 1 && Camera.Position.Y > Cam.Y - roomheight && onup == true && counter < 30)
        //{
        //    CameraVelocity = new Vector2(0, -roomheight / 30);
        //    PlayingState.currentFloor.currentRoom.CameraMoving = true;
        //}
        //else if (direction == 2 && Camera.Position.Y < Cam.Y + roomheight && ondown == true && counter < 30)
        //{
        //    CameraVelocity = new Vector2(0, roomheight / 30);
        //    PlayingState.currentFloor.currentRoom.CameraMoving = true;
        //}
        //else if (direction == 3 && Camera.Position.X > Cam.X - roomwidth && onleft == true && counter < 30)
        //{
        //    CameraVelocity = new Vector2(-roomwidth / 30, 0);
        //    PlayingState.currentFloor.currentRoom.CameraMoving = true;
        //}
        //else if (direction == 4 && Camera.Position.Y < Cam.X + roomwidth && onright == true && counter < 30)
        //{
        //    CameraVelocity = new Vector2(roomwidth / 30, 0);
        //    PlayingState.currentFloor.currentRoom.CameraMoving = true;
        //}

        //if  ((onup || ondown || onleft || onright) && counter < 30)
        //{
        //    Camera.Position += CameraVelocity;
        //    counter++;
        //}

        //if (counter >= 30)
        //{
        //    onup = false;
        //    ondown = false;
        //    onleft = false;
        //    onright = false;
        //    counter = 0;
        //    PlayingState.currentFloor.currentRoom.CameraMoving = false;
        //}
    }
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //ad
        ChooseSprite();
        spriteBatch.Draw(wallsprite, position, Color.Gray);
        if (isdoor)
            spriteBatch.Draw(doorsprite, position, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
    }
}
