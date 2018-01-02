using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

class Door : Solid
{
    bool isdoor, onup = false, ondown = false, onleft = false, onright = false;
    Vector2 doorposition, positionOld = PlayingState.player.position;
    SpriteEffects Effect = SpriteEffects.None;
    Texture2D doorsprite, wallsprite;
    GameObjectList solid;
    int direction, counter = 0;
    int CellWidth = PlayingState.currentFloor.currentRoom.CellWidth;
    int CellHeight = PlayingState.currentFloor.currentRoom.CellHeight;
    int roomwidth = PlayingState.currentFloor.currentRoom.roomwidth;
    int roomheight = PlayingState.currentFloor.currentRoom.roomheight;

    public Door(bool Isdoor, Vector2 Startposition, int Direction, int layer = 0, string id = "door")
    : base(Startposition, layer, id)
    {
        solid = new GameObjectList();
        doorposition = Startposition;
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
                wallsprite = wallsprite = GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Right2");
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
        if (PlayingState.currentFloor.currentRoom.enemycounter > 0)
        {
            base.Update(gameTime);
            if (CollidesWith(PlayingState.player))
            {
                PlayingState.player.position.X -= 10;
            }
            List<GameObject> RemoveBullets = new List<GameObject>();

            foreach (Bullet bullet in PlayingState.player.bullets.Children)
                if (CollidesWith(bullet))
                    RemoveBullets.Add(bullet);
        }
        if (!isdoor)
            solid.Update(gameTime);
        ControlCamera();
    }

    void ControlCamera()
    {
        Vector2 Cam = Camera.Position;
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2);

        if (PlayingState.currentFloor.currentRoom.enemycounter == 0)
        {
            if (isdoor && MiddleofPlayer.X >= doorposition.X && MiddleofPlayer.X <= doorposition.X + CellWidth)
                if (MiddleofPlayer.Y >= doorposition.Y && MiddleofPlayer.Y <= doorposition.Y + CellHeight)                
                    switch (direction)
                    {
                        case (1):
                            PlayingState.player.position -= new Vector2(0, 2 * CellHeight + 30);
                            onup = true;
                            break;
                        case (2):
                            PlayingState.player.position += new Vector2(0, 2 * CellHeight + 30);
                            ondown = true;
                            break;
                        case (3):
                            PlayingState.player.position -= new Vector2(2 * CellHeight + 30, 0);
                            onleft = true;
                            break;
                        case (4):
                            PlayingState.player.position += new Vector2(2 * CellHeight + 30, 0);
                            onright = true;
                            break;
                        default:
                            break;
                    }                
        }

            Vector2 CameraVelocity = new Vector2(0, 0);

            if (direction == 1 && Camera.Position.Y > Cam.Y - roomheight && onup == true && counter < 30)
                CameraVelocity = new Vector2(0, -roomheight / 30);
            if (direction == 2 && Camera.Position.Y < Cam.Y + roomheight && ondown == true && counter < 30)
                CameraVelocity = new Vector2(0, roomheight / 30);
            if (direction == 3 && Camera.Position.X > Cam.X - roomwidth && onleft == true && counter < 30)
                CameraVelocity = new Vector2(-roomwidth / 30, 0);
            if (direction == 4 && Camera.Position.Y < Cam.X + roomwidth && onright == true && counter < 30)
                CameraVelocity = new Vector2(roomwidth / 30, 0);

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
            }
        }
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //ad
        ChooseSprite();
        spriteBatch.Draw(wallsprite, doorposition, Color.Gray);
        if (isdoor)
            spriteBatch.Draw(doorsprite, doorposition, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
    }
}
