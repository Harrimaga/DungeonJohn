using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class EndRoom : Room
{
    static public bool cleared = false, trigger = false, placed = false;
    int currentlevel;

    public EndRoom(string map, int roomListIndex, int a, int b, int layer = 0, string id = "") : base(map, roomListIndex, a, b, layer)
    {
        Type = "bossroom";
        RoomListIndex = 2;
        position = new Vector2(a, b);
        placed = false;
        cleared = false;
        trigger = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        currentlevel = PlayingState.currentFloor.displayint;
        //TODO check player volgende floor mag nextFloor true maken
        CheckExit();
        if (!placed)
        {
            PlaceBoss(ChooseBoss(currentlevel));
            placed = true;
        }
        if (cleared && enemycounter > 0)
            enemycounter--;

    }

    public string ChooseBoss(int currentlevel)
    {
        switch (currentlevel)
        {
            case 1:
                return "HomingBoss";
            case 2:
                return "MinionBoss";
            case 3:
                return "HomingBoss2";
            case 4:
                return "MinionBoss2";
            case 5:
                return "CreamBatBoss";
            default:
                return "HomingBoss";
        }
    }

    public void PlaceBoss(string boss)
    {
        Vector2 BossPosition, SpawnPosition1, SpawnPosition2;
        switch (Lastentrypoint)
        {
            case 1:
                BossPosition = new Vector2(8 * CellWidth + a * roomwidth, 9 * CellHeight + b * roomheight);
                SpawnPosition1 = new Vector2(BossPosition.X + 6 * CellWidth, BossPosition.Y);
                SpawnPosition2 = new Vector2(BossPosition.X - 3 * CellWidth, BossPosition.Y);
                break;
            case 2:
                BossPosition = new Vector2(8 * CellWidth + a * roomwidth, 2 * CellHeight + b * roomheight);
                SpawnPosition1 = new Vector2(BossPosition.X + 6 * CellWidth, BossPosition.Y);
                SpawnPosition2 = new Vector2(BossPosition.X - 3 * CellWidth, BossPosition.Y);
                break;
            case 3:
                BossPosition = new Vector2(15 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight);
                SpawnPosition1 = new Vector2(BossPosition.X, BossPosition.Y + 5 * CellHeight);
                SpawnPosition2 = new Vector2(BossPosition.X, BossPosition.Y - 2 * CellHeight);
                break;
            case 4:
                BossPosition = new Vector2(2 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight);
                SpawnPosition1 = new Vector2(BossPosition.X, BossPosition.Y + 5 * CellHeight);
                SpawnPosition2 = new Vector2(BossPosition.X, BossPosition.Y - 2 * CellHeight);
                break;
            case 5:
                BossPosition = new Vector2(8 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight);
                SpawnPosition1 = new Vector2(BossPosition.X + 6 * CellWidth, BossPosition.Y);
                SpawnPosition2 = new Vector2(BossPosition.X - 3 * CellWidth, BossPosition.Y);
                break;
            default:
                BossPosition = new Vector2(8 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight);
                SpawnPosition1 = BossPosition;
                SpawnPosition2 = BossPosition;
                break;
        }
        switch (boss)
        {
            case ("HomingBoss"):
                Boss1 Homingboss = new Boss1(BossPosition, new Vector2(a, b));
                bosses.Add(Homingboss);
                break;
            case ("MinionBoss"):
                MinionBoss MinionBoss = new MinionBoss(BossPosition, new Vector2(a,b));
                bosses.Add(MinionBoss);
                MinionSpawner factory1 = new MinionSpawner(SpawnPosition1, new Vector2(a, b), currentlevel);
                MinionSpawner factory2 = new MinionSpawner(SpawnPosition2, new Vector2(a, b), currentlevel);
                enemies.Add(factory1);
                enemies.Add(factory2);
                break;
            case ("HomingBoss2"):
                Boss1 Homingboss2 = new Boss1(BossPosition, new Vector2(a, b), 3);
                bosses.Add(Homingboss2);
                break;
            case ("MinionBoss2"):
                MinionBoss MinionBoss2 = new MinionBoss(BossPosition, new Vector2(a, b), 3);
                bosses.Add(MinionBoss2);
                MinionSpawner factory11 = new MinionSpawner(SpawnPosition1, new Vector2(a, b), currentlevel);
                MinionSpawner factory22 = new MinionSpawner(SpawnPosition2, new Vector2(a, b), currentlevel);
                enemies.Add(factory11);
                enemies.Add(factory22);
                break;
            case ("CreamBatBoss"):
                CreamBatBoss CreamBatBoss = new CreamBatBoss(BossPosition, new Vector2(a, b));
                bosses.Add(CreamBatBoss);
                break;
        }
        enemycounter++;
    }

    public override void CheckExit()
    {
        //base.CheckExit();
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 2);
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

