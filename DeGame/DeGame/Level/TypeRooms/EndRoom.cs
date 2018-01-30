using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class EndRoom : Room
{
    static public bool cleared = false, trigger = false, placed = false, finalboss = false;
    int currentlevel;
    Random random = new Random();

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

        if (!placed)
        {
            PlaceBoss(ChooseBoss(currentlevel));
            if (finalboss)
                for (int x = 0; x < roomarraywidth; ++x)
                    for (int y = 0; y < roomarrayheight; ++y)
                        if (roomarray[x, y] == "Exit")
                            roomarray[x, y] = "Background";
            placed = true;
        }
        CheckExit();
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
        }
        int randomboss = random.Next(5) + 1;
        return ChooseBoss(randomboss);
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
            default:
                BossPosition = new Vector2(8 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight);
                SpawnPosition1 = BossPosition;
                SpawnPosition2 = BossPosition;
                break;
        }
        switch (boss)
        {
            case ("HomingBoss"):
                HomingBoss Homingboss = new HomingBoss(new Vector2(8 * CellWidth + a * roomwidth - 3, 6 * CellHeight + b * roomheight - 50), new Vector2(a, b));
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
                HomingBoss Homingboss2 = new HomingBoss(new Vector2(8 * CellWidth + a * roomwidth - 3, 6 * CellHeight + b * roomheight - 50), new Vector2(a, b), 10);
                bosses.Add(Homingboss2);
                break;
            case ("MinionBoss2"):
                MinionBoss MinionBoss2 = new MinionBoss(BossPosition, new Vector2(a, b), 10);
                bosses.Add(MinionBoss2);
                MinionSpawner factory11 = new MinionSpawner(SpawnPosition1, new Vector2(a, b), currentlevel);
                MinionSpawner factory22 = new MinionSpawner(SpawnPosition2, new Vector2(a, b), currentlevel);
                enemies.Add(factory11);
                enemies.Add(factory22);
                break;
            case ("CreamBatBoss"):
                CreamBatBoss CreamBatBoss = new CreamBatBoss(new Vector2(8 * CellWidth + a * roomwidth, 6 * CellHeight + b * roomheight), new Vector2(a, b));
                bosses.Add(CreamBatBoss);
                break;
        }
        enemycounter++;
    }

    protected override void CheckExit()
    {
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 2);
        if (cleared && !finalboss)
        {
            if (MiddleofPlayer.X >= Exit.X && MiddleofPlayer.X <= Exit.X + CellWidth)
                if (MiddleofPlayer.Y >= Exit.Y && MiddleofPlayer.Y <= Exit.Y + CellHeight)
                {
                    PlayingState.currentFloor.NextShop();
                    cleared = false;
                }
        }
        for (int x = 0; x < roomarraywidth; ++x)
            for (int y = 0; y < roomarrayheight; ++y)
                if (roomarray[x, y] == "Exit" && cleared)
                    roomarray[x, y] = "ExitShop";
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
}

