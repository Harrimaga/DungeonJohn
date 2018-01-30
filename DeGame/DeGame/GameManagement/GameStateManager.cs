using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager : IGameObject
{
    public IGameObject currentGameState;
    Dictionary<string, IGameObject> gameStates;
    public string LastState;

    public GameStateManager()
    {
        gameStates = new Dictionary<string, IGameObject>();
        currentGameState = null;
    }

    public void SwitchTo(string name)
    {
        if (gameStates.ContainsKey(name))
        {
            currentGameState = gameStates[name];
        }
        else
        {
            throw new KeyNotFoundException("Could not find game state: " + name);
        }
    }

    public void AddGameState(string name, IGameObject state)
    {
        gameStates[name] = state;
    }

    public void RemoveGameState(string name)
    {
        gameStates.Remove(name);
    }

    public IGameObject GetGameState(string name)
    {
        return gameStates[name];
    }

    public void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if (currentGameState != null)
        {
            currentGameState.HandleInput(inputHelper, gameTime);
        }
    }

    public void Update(GameTime gameTime)
    {
        
        if (currentGameState != null)
        {
            currentGameState.Update(gameTime);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (currentGameState != null)
        {
            currentGameState.Draw(gameTime, spriteBatch);
        }
    }

    public void Reset()
    {
        if (currentGameState != null)
        {
            currentGameState.Reset();
        }
    }

}

