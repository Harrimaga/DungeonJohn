using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class HUD
{
    public int screenheight, screenwidth;
    HealthBar healthbar;
    public WornItems wornItems;

    public HUD()
    {
        screenwidth = GameEnvironment.WindowSize.X;
        screenheight = GameEnvironment.WindowSize.Y;
        healthbar = new HealthBar(PlayingState.player.health, PlayingState.player.maxhealth, PlayingState.player.position, true);
        wornItems = new WornItems(new Vector2(0, 0));
    }

    public void Update(GameTime gameTime)
    {
        healthbar.Update(gameTime, PlayingState.player.health, PlayingState.player.maxhealth, PlayingState.player.position);
        wornItems.Update(gameTime);
    }

    void DrawMinimap(SpriteBatch spriteBatch)
    {
        int FloorCellWidth = 15;
        int FloorCellHeight = 15;
        int currentroomx = (int)PlayingState.player.position.X / 1260;
        int currentroomy = (int)PlayingState.player.position.Y / 900;
        if (PlayingState.currentFloor.floor[currentroomx, currentroomy] != null)
            PlayingState.currentFloor.currentRoom = PlayingState.currentFloor.floor[currentroomx, currentroomy];
        else
            PlayingState.currentFloor.currentRoom.position = new Vector2(currentroomx, currentroomy);

        for (int x = 0; x < PlayingState.currentFloor.floorWidth; x++)
            for (int y = 0; y < PlayingState.currentFloor.floorHeight; y++)
            {
                Vector2 MinimapTilePosition = new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2));
                if (PlayingState.currentFloor.floor[x, y] != null)
                {
                    if (PlayingState.currentFloor.floor[x, y].Visited)
                        switch (PlayingState.currentFloor.floor[x, y].RoomListIndex)
                        {
                            case (1):
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUD/MinimapStartTile")), MinimapTilePosition, Color.White);
                                break;
                            case (2):
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUD/MinimapBossTile")), MinimapTilePosition, Color.White);
                                break;
                            case (3):
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUD/MinimapItemTile")), MinimapTilePosition, Color.White);
                                break;
                            default:
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUD/MinimapTile")), MinimapTilePosition, Color.White);
                                break;
                        }
                    else if ((PlayingState.currentFloor.CheckNeighbours(x, y, true) && PlayingState.currentFloor.floor[x, y - 1].Visited) || (PlayingState.currentFloor.CheckNeighbours(x, y, false, true) && PlayingState.currentFloor.floor[x, y + 1].Visited)
                        || (PlayingState.currentFloor.CheckNeighbours(x, y, false, false, true) && PlayingState.currentFloor.floor[x - 1, y].Visited) || (PlayingState.currentFloor.CheckNeighbours(x, y, false, false, false, true) && PlayingState.currentFloor.floor[x + 1, y].Visited))
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUD/MinimapUnknownTile")), MinimapTilePosition, Color.White);
                }
                if (PlayingState.currentFloor.currentRoom.position == new Vector2(x, y))
                {
                    spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUD/CurrentMinimapTile")), MinimapTilePosition, Color.White);
                }
            }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    { 
        string Gold = "Gold: " + PlayingState.player.gold;
        string tokens;
        if (PlayingState.player.leveltokens == 1)
            tokens = "You have " + PlayingState.player.leveltokens + " unspent token,";
        else
            tokens = "You have " + PlayingState.player.leveltokens + " unspent tokens,";
        string tokens2 = "press 'O' to level up";
        string enemycount = "Count: " + PlayingState.currentFloor.currentRoom.enemycounter;

        if (PlayingState.currentFloor.FloorGenerated)
        {
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUD/HUDbackground")), new Vector2(screenwidth - 340 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2)));
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), PlayingState.currentFloor.Level, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 50), Color.White);
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), Gold, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 250), Color.Yellow);
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Player Level: " + Convert.ToString(PlayingState.player.level), new Vector2(PlayingState.hud.screenwidth - 275 + (Camera.Position.X - PlayingState.hud.screenwidth / 2), 200 + (Camera.Position.Y - PlayingState.hud.screenheight / 2)), Color.White);
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Damage: " + Convert.ToString((int)PlayingState.player.attack), new Vector2(PlayingState.hud.screenwidth - 275 + (Camera.Position.X - PlayingState.hud.screenwidth / 2), 225 + (Camera.Position.Y - PlayingState.hud.screenheight / 2)), Color.Red);
            if (PlayingState.player.leveltokens > 0)
            {
                spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), tokens, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 400), Color.White);
                spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), tokens2, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 425), Color.White);
            }
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), enemycount, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 450), Color.White);

            healthbar.Draw(spriteBatch);
            DrawMinimap(spriteBatch);

            wornItems.Position = new Vector2(screenwidth - 300 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 510);
            wornItems.Draw(gameTime, spriteBatch);
        }
    }
}
