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
    int counter = 0;

    public Leveling()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
        attackB = new Button(new Vector2(240,600), "Attack","AttackUp", "AttackUpPressed",true,1);
        healthB = new Button(new Vector2(620, 600), "Health", "HealthUp","HealthUpPressed",true, 1);
    }

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        attackB.HandleInput(inputHelper, gameTime);
        healthB.HandleInput(inputHelper, gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
        playingState.Draw(gameTime, spriteBatch);
        attackB.Draw(gameTime, spriteBatch);
        healthB.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/LevelUp"), new Vector2(350,100) + BasisPosition);
        //spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Level-Up",BasisPosition + new Vector2(600,240), Color.Black);
        //spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/pauze"), BasisPosition);
    }

    public virtual void Update(GameTime gameTime)
    {
        attackB.Update(gameTime);
        healthB.Update(gameTime);
        if (attackB.Pressed && !picked)
        {
            PlayingState.player.StatIncrease(1);
            picked = true;
        }
        if (healthB.Pressed && !picked)
        {
            PlayingState.player.StatIncrease(2);
            picked = true;
        }
        if (picked && counter < 10)
        {
            counter++;
        }
        if (counter >= 10)
        {
            counter = 0;
            picked = false;
            GameEnvironment.gameStateManager.SwitchTo("Playing");
        }
    }

    public virtual void Reset()
    {
    }
}
