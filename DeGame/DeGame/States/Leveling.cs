using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Leveling : IGameObject
{
    Vector2 BasisPosition;
    Button attackB, healthB;
    bool picked = false;
    protected IGameObject playingState;
    public Leveling()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
        attackB = new Button(new Vector2(240,600), "Attack","n",1);
        healthB = new Button(new Vector2(420, 600), "Health", "n", 1);
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
        attackB.HandleInput(inputHelper);
        healthB.HandleInput(inputHelper);
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        attackB.Draw(gameTime, spriteBatch);
        healthB.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/LevelUp"), new Vector2(350,100) + BasisPosition);
        //spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Level-Up",BasisPosition + new Vector2(600,240), Color.Black);
        //spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/pauze"), BasisPosition);
    }
    public virtual void Update(GameTime gameTime)
    {
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
        attackB.Update(gameTime);
        healthB.Update(gameTime);
        if (picked)
        {
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }
        if (PlayingState.player.state && attackB.Pressed)
        {
            PlayingState.player.StateIncrease(1);
            PlayingState.player.state = false;
            //attackB.Pressed = false;
            picked = true;
        }
        if (PlayingState.player.state && healthB.Pressed)
        {
            PlayingState.player.StateIncrease(2);
            PlayingState.player.state = false;
            //healthB.Pressed = false;
            picked = true;
        }
    }
    public virtual void Reset()
    {
    }
}
