//using DeGame.GameManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeGame
{
    public class Game1 : GameEnvironment
    {
        //private SpriteSheet spriteSheet;

        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }

        public Game1()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            //spriteSheet = new SpriteSheet("Sprites/PlayerPaperAnimated", 60, 86, 6, 11, true);
            base.Initialize();


        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.Window.Position = new Point(150, 30);
            WindowSize = new Point(1600, 900);
            FullScreen = false;

            gameStateManager.AddGameState("Crafting", new Crafting());
            gameStateManager.AddGameState("MainMenu", new MainMenuState());
            gameStateManager.AddGameState("Playing", new PlayingState());
            gameStateManager.AddGameState("GameOver", new GameOverState());
            gameStateManager.AddGameState("PauseMenu", new PauseMenuState());
            gameStateManager.AddGameState("Leveling", new Leveling());
            gameStateManager.AddGameState("Victory", new VictoryState());
            gameStateManager.AddGameState("Option", new Option());
            gameStateManager.AddGameState("Intro", new IntroState());
            gameStateManager.SwitchTo("Intro");

            //spriteSheet.LoadContent(Content, GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //if(gameTime.TotalGameTime.TotalSeconds > 5)
            //{
            //    spriteSheet.Update(gameTime);
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //spriteBatch.Begin();
            //spriteBatch.Draw(spriteSheet.CurrentFrame, Vector2.Zero, Color.White);
            //spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
