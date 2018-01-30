using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class PlayingState : IGameObject
{
    // Hier worden de player, de floor en de hud aangemaakt
    public static Player player;
    public static Floor currentFloor;
    public static HUD hud;
    Floor floor;

    public PlayingState()
    {

        player = new Player();
        floor = new Floor();
        hud = new HUD();
        currentFloor = floor;
        GameEnvironment.gameStateManager.LastState = "playing";
        GameEnvironment.soundManager.loadSoundEffect("Loss");
    }

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        player.HandleInput(inputHelper, gameTime);
        if (inputHelper.KeyPressed(Keys.P))
        {
            GameEnvironment.gameStateManager.SwitchTo("PauseMenu");
        }
        floor.HandleInput(inputHelper, gameTime); 
    }

    public virtual void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        floor.Update(gameTime);
        hud.Update(gameTime);
        //switching state if you are dead
        if (player.health <= 0)
        {
            GameEnvironment.soundManager.playSoundEffect("Loss");
            GameEnvironment.gameStateManager.SwitchTo("GameOver");
        }
        //switching state if you win
        if (currentFloor.CurrentLevel >= 10 && !Boss.endless)
        {
            GameEnvironment.gameStateManager.SwitchTo("Victory");
        }
    }

    public virtual void Reset()
    {
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //draws floor and player and hud
        floor.Draw(gameTime, spriteBatch);
        hud.Draw(gameTime, spriteBatch);
        player.Draw(gameTime, spriteBatch);
    }
}
