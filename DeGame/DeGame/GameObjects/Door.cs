using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Door : SpriteGameObject
{
    Vector2 doorposition, positionOld = PlayingState.player.position;
    SpriteEffects Effect = SpriteEffects.None;
    Texture2D doorsprite, wallsprite;
    GameObjectList solid;
    int direction;
    bool isdoor;


    public Door(bool Isdoor, Vector2 Startposition, int Direction, int layer = 0, string id = "door")
    : base("Sprites/doorup", layer, id)
    {
        solid = new GameObjectList();
        doorposition = Startposition;
        direction = Direction;
        isdoor = Isdoor;
        if (!isdoor)
        {
            Solid wall = new Wall(doorposition, 0, "Wall");
            solid.Add(wall);
        }
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
            if (Room.enemies.Count > 0)
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
            if (Room.enemies.Count > 0)
                doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/doorleftclosed");
            else
                doorsprite = GameEnvironment.assetManager.GetSprite("Sprites/doorleft");
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Room.enemies.Count > 0)
        {
            if (CollidesWith(PlayingState.player))
            {
                PlayingState.player.position.X -= 10;
            }
        }
        if(!isdoor)
            solid.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        ChooseSprite();
        spriteBatch.Draw(wallsprite, doorposition, Color.Gray);
        if (isdoor)
            spriteBatch.Draw(doorsprite, doorposition, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
    }    
}

