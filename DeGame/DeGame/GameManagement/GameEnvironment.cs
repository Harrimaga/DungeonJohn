﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class GameEnvironment : Game
    {
    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;
    protected InputHelper inputHelper;
 
    protected static GameStateManager gameStateManager;
    protected static Random random;
<<<<<<< DeGame/DeGame/GameManagement/GameEnvironment.cs
    public static AssetManager assetManager;
    
=======
    public static AssetManager assetManager;
    protected static Camera cam;
>>>>>>> DeGame/DeGame/GameManagement/GameEnvironment.cs

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
        /*if (inputHelper.KeyPressed(Keys.F5))
        {
            FullScreen = !FullScreen;
        }*/
        gameStateManager.HandleInput(inputHelper);
    }

    protected override void Update(GameTime gameTime)
    {
        HandleInput();
        gameStateManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        spriteBatch.Begin();
        //spriteBatch.Draw(assetManager.GetSprite("Sprites/Random"), Vector2.Zero);
        gameStateManager.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public static Camera Camera
    {
        get { return cam; }
        set { cam = value; }
    }
}
