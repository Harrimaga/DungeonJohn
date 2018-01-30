using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class PlayingState : IGameObject
{
    /// <summary>
    /// Hier worden de floor, de player en de hud aangemaakt
    /// </summary>
    public static Player player;
    public static Floor currentFloor;
    public static HUD hud;
    Floor floor;
    SoundEffect Loss;

    public PlayingState()
    {
        player = new Player();
        floor = new Floor();
        hud = new HUD();
        currentFloor = floor;
        GameEnvironment.gameStateManager.LastState = "playing";
        Player.inventory.currentWeapon = new BigMac();
        Loss = GameEnvironment.assetManager.GetSound("SoundEffects/Loss");
    }

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        player.HandleInput(inputHelper, gameTime);
        if (inputHelper.KeyPressed(Keys.P) || inputHelper.ButtonPressed(Buttons.Start))
        {
            GameEnvironment.gameStateManager.SwitchTo("PauseMenu");
        }
        if (inputHelper.KeyPressed(Keys.M) || inputHelper.ButtonPressed(Buttons.Start))
        {
            GameEnvironment.gameStateManager.SwitchTo("Credits");
        }
        floor.HandleInput(inputHelper, gameTime); 
    }

    public virtual void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        floor.Update(gameTime);
        hud.Update(gameTime);
        if (player.health <= 0)
        {
            Loss.Play();
            GameEnvironment.gameStateManager.SwitchTo("GameOver");
        }
        if (currentFloor.CurrentLevel >= 10)
        {
            GameEnvironment.gameStateManager.SwitchTo("Credits");
        }
    }

    public virtual void Reset()
    {
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        floor.Draw(gameTime, spriteBatch);
        hud.Draw(gameTime, spriteBatch);
        player.Draw(gameTime, spriteBatch);
    }
}


