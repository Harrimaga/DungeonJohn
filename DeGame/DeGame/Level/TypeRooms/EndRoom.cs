using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class EndRoom : Room
{
    static public bool cleared = false, trigger = false;
    int currentlevel;

    public EndRoom(int roomListIndex, int a, int b, int layer = 0, string id = "") : base(a, b, layer)
    {
        currentlevel = PlayingState.currentFloor.CurrentLevel;
        PlaceBoss(ChooseBoss(currentlevel));
        Type = "bossroom";
        RoomListIndex = 2;
        position = new Vector2(a, b);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //TODO check player volgende floor mag nextFloor true maken
        CheckExit();
    }

    public string ChooseBoss(int currentlevel)
    {
        // if (currentlevel == 1)
        return "MinionBoss";
    }

    public void PlaceBoss(string boss)
    {
        switch (boss)
        {
            case ("MinionBoss"):
                Boss MinionBoss = new Boss1(new Vector2(11 * CellWidth + a * roomwidth, 11 * CellHeight + b * roomheight), new Vector2(a,b));
                bosses.Add(MinionBoss);
                MinionSpawner factory1 = new MinionSpawner(new Vector2(4 * CellWidth + a * roomwidth, 11 * CellHeight + b * roomheight), new Vector2(a, b));
                MinionSpawner factory2 = new MinionSpawner(new Vector2(17 * CellWidth + a * roomwidth, 11 * CellHeight + b * roomheight), new Vector2(a, b));
                enemies.Add(factory1);
                enemies.Add(factory2);
                break;
            default:
                Boss Boss1 = new Boss1(new Vector2(11 * CellWidth + a * roomwidth, 3 * CellHeight + b * roomheight), new Vector2(a, b));
                bosses.Add(Boss1);
                break;
        }
    }

    public override void CheckExit()
    {
        base.CheckExit();
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2);
        if (cleared)
        {
            if (MiddleofPlayer.X >= Exit.X && MiddleofPlayer.X <= Exit.X + CellWidth)
                if (MiddleofPlayer.Y >= Exit.Y && MiddleofPlayer.Y <= Exit.Y + CellHeight)
                    PlayingState.currentFloor.NextShop();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
}

