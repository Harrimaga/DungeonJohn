using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class MainMenuState : IGameObject
{
    Button start, option;

    public MainMenuState()
    {
       //buttons
       start = new Button(new Vector2(620, 725), "Start", "Start","StartPressed",true, 1);
       option = new Button(new Vector2(620, 800), "Option", "Controls", "ControlsPressed", true, 1);
    }
    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        start.HandleInput(inputHelper, gameTime);
        option.HandleInput(inputHelper, gameTime);
        //button check switching state
        if (start.Pressed)
        {
            GameEnvironment.gameStateManager.SwitchTo("GameModes");
            Reset();
        }
        if (inputHelper.KeyPressed(Keys.Z) || option.Pressed)
        {
            GameEnvironment.gameStateManager.SwitchTo("Option");
            //Reset();
        }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/PauseMenu/DungeonJohn"), new Vector2(-550, -380));
        start.Draw(gameTime, spriteBatch);
        option.Draw(gameTime, spriteBatch);
    }
    public virtual void Update(GameTime gameTime)
    {
        start.Update(gameTime);
        option.Update(gameTime);
        Camera.Position = new Vector2(0,0);
    }
    public virtual void Reset()
    {
        PlayingState.player.health = 100;
        PlayingState.currentFloor.ResetFloor();
        PlayingState.player.position = PlayingState.currentFloor.startPlayerPosition;
    }
}

