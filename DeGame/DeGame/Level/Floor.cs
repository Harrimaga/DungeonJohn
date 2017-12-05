using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Floor
{
    string[,,] FloorLayout = new string[10, 10, 2];
    string[,] Mergearray = new string[100, 100];
    Room room = new Room();

    public void MergeArrays(string [,]roomarray)
    {
        int a = 0, b = 0;
        // TODO algoritme dat kamers indeeld en offset waarde aan a en b geeft
        for (int x = 0; x == 10; x++)
            for (int y = 0; y == 10; y++)
                Mergearray[x, y] = roomarray[x + 10 * a, y + 10 * b];
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int x = 0; x < 100; x++)
            for (int y = 0; y < 100; y++)
            {
                switch (Mergearray[x, y])
                {
                    case "Unknown":
                        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), new Vector2(x * 50, y * 50), Color.Black);
                        break;
                    case "Background":
                        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), new Vector2(x * 50, y * 50), Color.LightGray);
                        break;
                    case "Rock":
                        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), new Vector2(x * 50, y * 50), Color.DarkOrange); 
                        break;
                    case "Wall":
                        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), new Vector2(x * 50, y * 50), Color.Brown);
                        break;
                    case "Exit":
                        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), new Vector2(x * 50, y * 50), Color.Red);
                        break;
                    case "Start":
                        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), new Vector2(x * 50, y * 50), Color.Blue);
                        break;
                    default:
                        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), new Vector2(x * 50, y * 50), Color.Gray);
                        break;
                }
            }
    }
}

