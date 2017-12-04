//using DeGame.GameManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

//namespace DeGame.GameManagement

public class GameEnvironment : Game
    {
    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;
    protected InputHelper inputHelper;
 
    protected static GameStateManager gameStateManager;
    protected static Random random;
    protected static AssetManager assetManager;

    public GameEnvironment()
    {
        graphics = new GraphicsDeviceManager(this);

        inputHelper = new InputHelper();
        gameStateManager = new GameStateManager();
        random = new Random();
        assetManager = new AssetManager(Content);
    }

    protected override void LoadContent()
    {
        //DrawingHelper.Initialize(this.GraphicsDevice);
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }
    protected void HandleInput()
    {
        inputHelper.Update();
        if (inputHelper.KeyPressed(Keys.Escape))
        {
            Exit();
        }
        /*
        if (inputHelper.KeyPressed(Keys.F5))
        {
            FullScreen = !FullScreen;
        }
        */
        gameStateManager.HandleInput(inputHelper);
    }

    protected override void Update(GameTime gameTime)
    {
        HandleInput();
        gameStateManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Purple);
        spriteBatch.Begin();
        spriteBatch.Draw(assetManager.GetSprite("Sprites/Random"), Vector2.Zero);
        gameStateManager.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}
