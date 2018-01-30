using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameOverState : IGameObject
{
    Button MainMenu;
    Vector2 BasisPosition;
    protected IGameObject playingState;
    public GameOverState()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
        //button
        MainMenu = new Button(new Vector2(600, 60), "MainMenu", "MainMenu2", "MainMenu2Pressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        MainMenu.HandleInput(inputHelper, gameTime);
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //button draw and gameover picture
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/GameOver"), BasisPosition);
        MainMenu.Draw(gameTime, spriteBatch);
    }

    public virtual void Update(GameTime gameTime)
    {
        //update button
        MainMenu.Update(gameTime);
        if (MainMenu.Pressed)
            GameEnvironment.gameStateManager.SwitchTo("MainMenu");
        GameEnvironment.soundManager.PlaySong("Menu");
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    }
    public virtual void Reset()
    {
    }
}

