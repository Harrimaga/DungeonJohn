using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Room
    {
    public void LoadTiles(string path)
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        int width = line.Length;
        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }
        TileField tiles = new TileField(textLines.Count - 2, width, 1, "tiles");

        Add(tiles);
        tiles.CellWidth = 72;
        tiles.CellHeight = 55;
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < textLines.Count - 2; ++y)
            {
                Tile t = LoadTile(textLines[y][x], x, y);
                tiles.Add(t, x, y);
            }
        }
    }


    //public void LoadTiles(string path)
    //{
    //    List<string> textLines = new List<string>();
    //    StreamReader fileReader = new StreamReader(path);
    //    string line = fileReader.ReadLine();
    //    int width = line.Length;
    //    while (line != null)
    //    {
    //        textLines.Add(line);
    //        line = fileReader.ReadLine();
    //    }
    //    TileField tiles = new TileField(textLines.Count - 2, width, 1, "tiles");

    //    Add(tiles);
    //    tiles.CellWidth = 72;
    //    tiles.CellHeight = 55;
    //    for (int x = 0; x < width; ++x)
    //    {
    //        for (int y = 0; y < textLines.Count - 2; ++y)
    //        {
    //            Tile t = LoadTile(textLines[y][x], x, y);
    //            tiles.Add(t, x, y);
    //        }
    //    }
    //}

    //private Tile LoadTile(char tileType, int x, int y)
    //{
    //    switch (tileType)
    //    {
    //        case '.':
    //            return new Tile();
    //        case '-':
    //            return LoadBasicTile("spr_platform", TileType.Platform);
    //        case '+':
    //            return LoadBasicTile("spr_platform_hot", TileType.Platform, true, false);
    //        case '@':
    //            return LoadBasicTile("spr_platform_ice", TileType.Platform, false, true);
    //        case 'X':
    //            return LoadEndTile(x, y);
    //        case 'W':
    //            return LoadWaterTile(x, y);
    //        case '1':
    //            return LoadStartTile(x, y);
    //        case '#':
    //            return LoadBasicTile("spr_wall", TileType.Normal);
    //        case '^':
    //            return LoadBasicTile("spr_wall_hot", TileType.Normal, true, false);
    //        case '*':
    //            return LoadBasicTile("spr_wall_ice", TileType.Normal, false, true);
    //        case 'T':
    //            return LoadTurtleTile(x, y);
    //        case 'R':
    //            return LoadRocketTile(x, y, true);
    //        case 'r':
    //            return LoadRocketTile(x, y, false);
    //        case 'S':
    //            return LoadSparkyTile(x, y);
    //        case 'A':
    //        case 'B':
    //        case 'C':
    //            return LoadFlameTile(x, y, tileType);
    //        default:
    //            return new Tile("");
    //    }
    //}
    //private Tile LoadTile(char tileType, int x, int y)
    //{
    //    switch (tileType)
    //    {
    //        case '.':
    //            return new Tile();
    //        case '-':
    //            return LoadBasicTile("spr_platform", TileType.Platform);
    //        case '+':
    //            return LoadBasicTile("spr_platform_hot", TileType.Platform, true, false);
    //        case '@':
    //            return LoadBasicTile("spr_platform_ice", TileType.Platform, false, true);
    //        case 'X':
    //            return LoadEndTile(x, y);
    //        case 'W':
    //            return LoadWaterTile(x, y);
    //        case '1':
    //            return LoadStartTile(x, y);
    //        case '#':
    //            return LoadBasicTile("spr_wall", TileType.Normal);
    //        case '^':
    //            return LoadBasicTile("spr_wall_hot", TileType.Normal, true, false);
    //        case '*':
    //            return LoadBasicTile("spr_wall_ice", TileType.Normal, false, true);
    //        case 'T':
    //            return LoadTurtleTile(x, y);
    //        case 'R':
    //            return LoadRocketTile(x, y, true);
    //        case 'r':
    //            return LoadRocketTile(x, y, false);
    //        case 'S':
    //            return LoadSparkyTile(x, y);
    //        case 'A':
    //        case 'B':
    //        case 'C':
    //            return LoadFlameTile(x, y, tileType);
    //        default:
    //            return new Tile("");
    //    }
    //}

    //private tile loadbasictile(string name, tiletype tiletype, bool hot = false, bool ice = false)
    //{
    //    tile t = new tile("tiles/" + name, tiletype);
    //    t.hot = hot;
    //    t.ice = ice;
    //    return t;
    //}

    //private tile loadstarttile(int x, int y)
    //{
    //    tilefield tiles = find("tiles") as tilefield;
    //    vector2 startposition = new vector2(((float)x + 0.5f) * tiles.cellwidth, (y + 1) * tiles.cellheight);
    //    player player = new player(startposition);
    //    add(player);
    //    return new tile("", tiletype.background);
    //}

    //private tile loadflametile(int x, int y, char enemytype)
    //{
    //    gameobjectlist enemies = find("enemies") as gameobjectlist;
    //    tilefield tiles = find("tiles") as tilefield;
    //    gameobject enemy = null;
    //    switch (enemytype)
    //    {
    //        case 'a': enemy = new unpredictableenemy(); break;
    //        case 'b': enemy = new playerfollowingenemy(); break;
    //        case 'c':
    //        default: enemy = new patrollingenemy(); break;
    //    }
    //    enemy.position = new vector2(((float)x + 0.5f) * tiles.cellwidth, (y + 1) * tiles.cellheight);
    //    enemies.add(enemy);
    //    return new tile();
    //}

    //private tile loadturtletile(int x, int y)
    //{
    //    gameobjectlist enemies = find("enemies") as gameobjectlist;
    //    tilefield tiles = find("tiles") as tilefield;
    //    turtle enemy = new turtle();
    //    enemy.position = new vector2(((float)x + 0.5f) * tiles.cellwidth, (y + 1) * tiles.cellheight + 25.0f);
    //    enemies.add(enemy);
    //    return new tile();
    //}


    //private tile loadsparkytile(int x, int y)
    //{
    //    gameobjectlist enemies = find("enemies") as gameobjectlist;
    //    tilefield tiles = find("tiles") as tilefield;
    //    sparky enemy = new sparky((y + 1) * tiles.cellheight - 100f);
    //    enemy.position = new vector2(((float)x + 0.5f) * tiles.cellwidth, (y + 1) * tiles.cellheight - 100f);
    //    enemies.add(enemy);
    //    return new tile();
    //}

    //private tile loadrockettile(int x, int y, bool movetoleft)
    //{
    //    gameobjectlist enemies = find("enemies") as gameobjectlist;
    //    tilefield tiles = find("tiles") as tilefield;
    //    vector2 startposition = new vector2(((float)x + 0.5f) * tiles.cellwidth, (y + 1) * tiles.cellheight);
    //    rocket enemy = new rocket(movetoleft, startposition);
    //    enemies.add(enemy);
    //    return new tile();
    //}

    //private tile loadendtile(int x, int y)
    //{
    //    tilefield tiles = find("tiles") as tilefield;
    //    spritegameobject exitobj = new spritegameobject("sprites/spr_goal", 1, "exit");
    //    exitobj.position = new vector2(x * tiles.cellwidth, (y + 1) * tiles.cellheight);
    //    exitobj.origin = new vector2(0, exitobj.height);
    //    add(exitobj);
    //    return new tile();
    //}

    //private tile loadwatertile(int x, int y)
    //{
    //    gameobjectlist waterdrops = find("waterdrops") as gameobjectlist;
    //    tilefield tiles = find("tiles") as tilefield;
    //    waterdrop w = new risingdrop();
    //    w.origin = w.center;
    //    w.position = new vector2(x * tiles.cellwidth, y * tiles.cellheight - 10);
    //    w.position += new vector2(tiles.cellwidth, tiles.cellheight) / 2;
    //    waterdrops.add(w);
    //    return new tile();
    //}
}

