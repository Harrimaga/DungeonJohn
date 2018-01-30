using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

class Leveling : IGameObject
{
    Vector2 BasisPosition;
    Button attackB, healthB, speedB, attackSpeedB;
    bool picked = false;
    protected IGameObject playingState;
    int counter = 0;
    SoundEffect levelingsound;

    public Leveling()
    {
        playingState = GameEnvironment.gameStateManager.GetGameState("Playing");
        levelingsound = GameEnvironment.assetManager.GetSound("SoundEffects/LevelUp");
        attackB = new Button(new Vector2(300,500), "Attack","AttackUp", "AttackUpPressed",true,1);
        healthB = new Button(new Vector2(680, 500), "Health", "HealthUp","HealthUpPressed",true, 1);
        speedB = new Button(new Vector2(300, 600), "Speed", "Speed", "SpeedPressed", true, 1);
        attackSpeedB = new Button(new Vector2(680, 600), "AttackSpeed", "AttackSpeed", "AttackSpeedPressed", true, 1);
    }

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        attackB.HandleInput(inputHelper, gameTime);
        healthB.HandleInput(inputHelper, gameTime);
        speedB.HandleInput(inputHelper, gameTime);
        attackSpeedB.HandleInput(inputHelper, gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        BasisPosition = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
        playingState.Draw(gameTime, spriteBatch);
        attackB.Draw(gameTime, spriteBatch);
        healthB.Draw(gameTime, spriteBatch);
        speedB.Draw(gameTime, spriteBatch);
        attackSpeedB.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/States/LevelUp"), new Vector2(350,100) + BasisPosition);
    }

    public virtual void Update(GameTime gameTime)
    {
        attackB.Update(gameTime);
        healthB.Update(gameTime);
        speedB.Update(gameTime);
        attackSpeedB.Update(gameTime);
        if (attackB.Pressed && !picked)
        {
            PlayingState.player.StatIncrease(1);
            picked = true;
            levelingsound.Play();
        }
        if (healthB.Pressed && !picked)
        {
            PlayingState.player.StatIncrease(2);
            picked = true;
            levelingsound.Play();
        }
        if (speedB.Pressed && !picked)
        {
            PlayingState.player.StatIncrease(3);
            picked = true;
        }
        if (attackSpeedB.Pressed && !picked)
        {
            PlayingState.player.StatIncrease(4);
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
