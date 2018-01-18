using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class EndRoom : Room
{
    static public bool cleared = false, trigger = false, placed = false;
    int currentlevel;

    public EndRoom(int roomListIndex, int a, int b, int layer = 0, string id = "") : base(roomListIndex, a, b, layer)
    {
        Type = "bossroom";
        RoomListIndex = 2;
        position = new Vector2(a, b);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        currentlevel = PlayingState.currentFloor.CurrentLevel;
        //TODO check player volgende floor mag nextFloor true maken
        CheckExit();
        if (!placed)
        {
            PlaceBoss(ChooseBoss(currentlevel));
            placed = true;
        }

    }

    public string ChooseBoss(int currentlevel)
    {
        if (currentlevel == 1)
            return "CreamBatBoss";
        else
            return "MinionBoss";
    }

    public void PlaceBoss(string boss)
    {
        switch (boss)
        {
            case ("HomingBoss"):
                Boss1 Homingboss = new Boss1(new Vector2(8 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight), new Vector2(a, b));
                bosses.Add(Homingboss);
                break;
            case ("MinionBoss"):
                MinionBoss MinionBoss = new MinionBoss(new Vector2(8 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight), new Vector2(a,b));
                bosses.Add(MinionBoss);
                MinionSpawner factory1 = new MinionSpawner(new Vector2(4 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight), new Vector2(a, b));
                MinionSpawner factory2 = new MinionSpawner(new Vector2(12 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight), new Vector2(a, b));
                enemies.Add(factory1);
                enemies.Add(factory2);
                break;
            case ("CreamBatBoss"):
                CreamBatBoss CreamBatBoss = new CreamBatBoss(new Vector2(8 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight), new Vector2(a, b));
                bosses.Add(CreamBatBoss);
                break;
            default:
                SpamEnemy spam = new SpamEnemy(new Vector2(8 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight), new Vector2(a, b));
                enemies.Add(spam);
                break;
        }
    }

    public override void CheckExit()
    {
        //base.CheckExit();
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2);
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

