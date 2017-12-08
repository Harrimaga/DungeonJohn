//using DeGame.GameManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeGame
{
    public class Game1 : GameEnvironment
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }

        public Game1()
        {
            //graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            gameStateManager.AddGameState("MainMenu", new MainMenuState());
            gameStateManager.AddGameState("Playing", new PlayingState());
            gameStateManager.AddGameState("GameOver", new GameOverState());
            gameStateManager.AddGameState("PauseMenu", new PauseMenuState());
            gameStateManager.SwitchTo("MainMenu");
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
