using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class GameEnvironment : Game
{
    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;
    public InputHelper inputHelper;
    public static GameStateManager gameStateManager;
    protected static Random random;

    public Matrix spriteScale;

    public static Point WindowSize;

    public static AssetManager assetManager;
    public static SoundManager soundManager;
    protected static Camera cam;

    public static Point Dimensions;

    public Texture2D CursorSprite;

    bool startup;

    public GameEnvironment()
    {
        graphics = new GraphicsDeviceManager(this);
        inputHelper = new InputHelper();
        gameStateManager = new GameStateManager();
        random = new Random();
        assetManager = new AssetManager(Content);
        soundManager = new SoundManager(Content);
        Dimensions = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        IsMouseVisible = true;
        startup = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        System.Console.WriteLine(graphics.PreferredBackBufferHeight);
        System.Console.WriteLine(graphics.PreferredBackBufferWidth);
        System.Console.WriteLine(Dimensions.ToString());
        System.Console.WriteLine(GraphicsDevice.Viewport.ToString());
    }

    protected void HandleInput(GameTime gameTime)
    {
        inputHelper.Update();
        gameStateManager.HandleInput(inputHelper, gameTime);
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
            graphics.PreferredBackBufferWidth = WindowSize.X;
            graphics.PreferredBackBufferHeight = WindowSize.Y;
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

        float targetAspectRatio = (float)WindowSize.X / (float)WindowSize.Y;
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

        inputHelper.Scale = new Vector2((float)GraphicsDevice.Viewport.Width / Dimensions.X,
                                        (float)GraphicsDevice.Viewport.Height / Dimensions.Y);
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
        HandleInput(gameTime);
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
