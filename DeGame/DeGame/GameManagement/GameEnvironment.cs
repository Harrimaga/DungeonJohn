using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



public class GameEnvironment : Game
    {
    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;
    public InputHelper inputHelper;
    public static GameStateManager gameStateManager;
    protected static Random random;

    public Matrix spriteScale;

    protected Point windowSize = new Point(800, 480);

    public static AssetManager assetManager;
    protected static Camera cam;

    public static Point Dimensions;

    bool startup;


    public GameEnvironment()
    {
        graphics = new GraphicsDeviceManager(this);
        inputHelper = new InputHelper();
        gameStateManager = new GameStateManager();
        random = new Random();
        assetManager = new AssetManager(Content);

        Dimensions = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

        startup = true;

        //ApplyResolutionSettings();
    }
    
    protected override void LoadContent()
    {
        //DrawingHelper.Initialize(this.GraphicsDevice);
        spriteBatch = new SpriteBatch(GraphicsDevice);

        System.Console.WriteLine(graphics.PreferredBackBufferHeight);
        System.Console.WriteLine(graphics.PreferredBackBufferWidth);
        System.Console.WriteLine(Dimensions.ToString());
        System.Console.WriteLine(GraphicsDevice.Viewport.ToString());
    }
    protected void HandleInput()
    {
        inputHelper.Update();
        /*if (inputHelper.KeyPressed(Keys.Escape))
        {
            Exit();
        }
        
        /*
        if (inputHelper.KeyPressed(Keys.F5))
        {
            FullScreen = !FullScreen;
        }
        */

        if (inputHelper.IsKeyDown(Keys.Right))
        {
            Camera.Position = new Vector2(Camera.Position.X + 10, Camera.Position.Y);
        }

        gameStateManager.HandleInput(inputHelper);
    }

    public bool FullScreen
    {
        get { return graphics.IsFullScreen; }
        set
        {
            ApplyResolutionSettings(value);
        }
    }

    public void ApplyResolutionSettings(bool fullscreen = false)
    {
        if (!fullscreen)
        {
            graphics.PreferredBackBufferWidth = windowSize.X;
            graphics.PreferredBackBufferHeight = windowSize.Y;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }
        else
        {
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        float targetAspectRatio = (float) Dimensions.X / (float) Dimensions.Y;
        int width = graphics.PreferredBackBufferWidth;
        int height = (int)(width / targetAspectRatio);
        if (height > graphics.PreferredBackBufferHeight)
        {
            height = graphics.PreferredBackBufferHeight;
            width = (int)(height * targetAspectRatio);
        }

        Viewport viewport = new Viewport();
        viewport.X = (graphics.PreferredBackBufferWidth / 2) - (width / 2);
        viewport.Y = (graphics.PreferredBackBufferHeight / 2) - (height / 2);
        viewport.Width = width;
        viewport.Height = height;
        GraphicsDevice.Viewport = viewport;

        inputHelper.Scale = new Vector2((float) GraphicsDevice.Viewport.Width / Dimensions.X,
                                        (float) GraphicsDevice.Viewport.Height / Dimensions.Y);
        inputHelper.Offset = new Vector2(viewport.X, viewport.Y);
        spriteScale = Matrix.CreateScale(inputHelper.Scale.X, inputHelper.Scale.Y, 1);

        cam = new Camera(GraphicsDevice.Viewport);
        cam.Zoom = 1f;
        cam.Rotation = 0;

        if (startup)
        {
            Camera.Position = new Vector2(Dimensions.X / 2, Dimensions.Y / 2);
            startup = false;

            System.Console.WriteLine(width + " * " + height);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        HandleInput();
        gameStateManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.TransformMatrix);
        gameStateManager.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public static Camera Camera
    {
        get { return cam; }
        set { cam = value; }
    }
}
