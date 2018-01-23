using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Pit : Solid
{
    int roomx, roomy, x, y;
    Vector2 TilePosition;
    Texture2D pitsprite1, pitsprite2, pitsprite3, pitsprite4, pitsprite5, pitsprite6, pitsprite7, pitsprite8, pitsprite9;
    bool check = false;

    public Pit(Vector2 startPosition, int layer = 0, string id = "Pit")
        : base(startPosition, layer, id)
    {
        position = startPosition;
        hittable = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!PlayingState.player.HelicopterHat)
        {
            SolidCollision();
        }
        if (roomx == 0)
        {
            roomx = (int)position.X / 1260;
            roomy = (int)position.Y / 900;
            x = ((int)position.X - roomx * 1260) / BoundingBox.Width;
            y = ((int)position.Y - roomy * 900) / BoundingBox.Height;
        }
        else if (!check)
        {
            TilePosition = new Vector2(x * BoundingBox.Width + roomx * 1260, y * BoundingBox.Height + roomy * 900);
            PitShader();
            check = true;
        }
    }


    void PitShader()
    {
        pitsprite1 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitTile");
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitFull");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Pit3Up");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Pit3Down");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Pit3Left");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Pit3Right");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitLU");
            if (!PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && !PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y + 1, 3))
                pitsprite3 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitCornerRD");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitLD");
            if (!PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && !PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y - 1, 3))
                pitsprite3 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitCornerRU");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitRU");
            if (!PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && !PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y + 1, 3))
                pitsprite3 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitCornerLD");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3))
        {
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitRD");
            if (!PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && !PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y - 1, 3))
                pitsprite3 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitCornerLU");
            return;
        }
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3))
            pitsprite2 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitUp");
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3))
            pitsprite3 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitDown");
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3))
            pitsprite4 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitLeft");
        if (PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3))
            pitsprite5 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitRight");
        if (!PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && !PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y - 1, 3))
            pitsprite6 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitCornerLU");
        if (!PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y, 3) && !PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x - 1, y + 1, 3))
            pitsprite7 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitCornerLD");
        if (!PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && !PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y - 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y - 1, 3))
            pitsprite8 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitCornerRU");
        if (!PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y, 3) && !PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x, y + 1, 3) && PlayingState.currentFloor.floor[roomx, roomy].CheckRoomarray(x + 1, y + 1, 3))
            pitsprite9 = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitCornerRD");
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (check)
            spriteBatch.Draw(pitsprite1, TilePosition);
        if (pitsprite2 != null)
            spriteBatch.Draw(pitsprite2, TilePosition);
        if (pitsprite3 != null)
            spriteBatch.Draw(pitsprite3, TilePosition);
        if (pitsprite4 != null)
            spriteBatch.Draw(pitsprite4, TilePosition);
        if (pitsprite5 != null)
            spriteBatch.Draw(pitsprite5, TilePosition);
        if (pitsprite6 != null)
            spriteBatch.Draw(pitsprite6, TilePosition);
        if (pitsprite7 != null)
            spriteBatch.Draw(pitsprite7, TilePosition);
        if (pitsprite8 != null)
            spriteBatch.Draw(pitsprite8, TilePosition);
        if (pitsprite9 != null)
            spriteBatch.Draw(pitsprite9, TilePosition);
    }
}
