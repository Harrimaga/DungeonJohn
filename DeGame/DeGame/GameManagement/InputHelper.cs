﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InputHelper
    {
    public MouseState currentMouseState, previousMouseState;
    public KeyboardState currentKeyboardState, previousKeyboardState;
    protected Vector2 scale, offset;

    public InputHelper()
    {
        scale = Vector2.One;
        offset = Vector2.Zero;
    }

    public void Update()
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
    }

    public Vector2 Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public Vector2 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    public Vector2 MousePosition
    {
        get { return (new Vector2(currentMouseState.X, currentMouseState.Y) - offset) / scale; }
    }

    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }

    public bool MouseLeftButtonDown()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }

    public bool IsKeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }

    public bool AnyKeyPressed
    {
        get { return currentKeyboardState.GetPressedKeys().Length > 0 && previousKeyboardState.GetPressedKeys().Length == 0; }
    }

}

