﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager : IGameObject
{
    IGameObject currentGameState;
    Dictionary<string, IGameObject> gameStates;

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

    public IGameObject GetGameState(string name)
    {
        return gameStates[name];
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (currentGameState != null)
        {
            currentGameState.HandleInput(inputHelper);
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

