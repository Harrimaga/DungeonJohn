using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class IntroState : IGameObject
{
    Vector2 velocity = new Vector2(0,-0.1f);
    Vector2 position = new Vector2(Camera.Position.X - 300, Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2) + 1000);
    Vector2 TextPosition = new Vector2(Camera.Position.X - 300, Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2) + 1600);
    Vector2 SkipPosition = new Vector2(Camera.Position.X - 800, Camera.Position.X - 600);
    int Counter = 0;
    public IntroState()
    {
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (inputHelper.KeyPressed(Keys.Space))
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "[Press 'Space' to skip]", SkipPosition, Color.White);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/NewBTeam"), position);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Dungeon John is a man with a mission:", TextPosition, Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "To take revenge on the infamous CreamBat that kidnapped his beloved toaster!", TextPosition + new Vector2(0, 25), Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Join John on his adventure through perilous dungeon floors,", TextPosition + new Vector2(0,50), Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "As he gains powerful artifacts and fight even more powerful enemies in this totally original dungeon crawler!", TextPosition + new Vector2(0, 75), Color.White);
    }

    public virtual void Update(GameTime gameTime)
    {
        position += velocity * gameTime.ElapsedGameTime.Milliseconds;
        TextPosition += velocity * gameTime.ElapsedGameTime.Milliseconds;
        Counter++;
        if (Counter >= 1100)
        {
            Reset();
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
        }
    }
    public virtual void Reset()
    {
        Counter = 0;
        Vector2 position = new Vector2(Camera.Position.X - 300, Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2) + 1000);
        Vector2 TextPosition = new Vector2(Camera.Position.X - 300, Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2) + 1600);
    }

}
