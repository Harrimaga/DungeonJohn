using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputHelper
    {
    public MouseState currentMouseState, previousMouseState;
    public KeyboardState currentKeyboardState, previousKeyboardState;
    public GamePadState currentGamePadState, previousGamePadState;
    PlayerIndex playerIndex;
    protected Vector2 scale, offset;

    public InputHelper()
    {
        scale = Vector2.One;
        offset = Vector2.Zero;
        if (setConnectedController() != 0)
        {
            playerIndex = setConnectedController();
        }
    }

    public void Update()
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        previousGamePadState = currentGamePadState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
        currentGamePadState = GamePad.GetState(playerIndex);
        offset = new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    }

    public PlayerIndex setConnectedController()
    {
        for (int i = 0; i < 4; i++)
        {
            switch(i)
            {
                case 0:
                    if(GamePad.GetState(PlayerIndex.One).IsConnected)
                    {
                        return PlayerIndex.One;
                    }
                    break;
                case 1:
                    if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                    {
                        return PlayerIndex.Two;
                    }
                    break;
                case 2:
                    if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                    {
                        return PlayerIndex.Three;
                    }
                    break;
                case 3:
                    if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                    {
                        return PlayerIndex.Four;
                    }
                    break;
            }
        }
        return 0;
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
        get { return (new Vector2(currentMouseState.X, currentMouseState.Y) + offset); }
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

    public bool ButtonPressed(Buttons b)
    {
        return currentGamePadState.IsButtonDown(b) && previousGamePadState.IsButtonUp(b);
    }
    
    public bool IsButtonDown(Buttons b)
    {
        return currentGamePadState.IsButtonDown(b);
    }

    public string getThumpDirection(string stick)
    {
        Vector2 thumbStick = new Vector2(0,0);

        if (stick == "right")
        {
            thumbStick = currentGamePadState.ThumbSticks.Right;
        }
        else
        {
            thumbStick = currentGamePadState.ThumbSticks.Left;
        }

        if (thumbStick.X > -0.5f && thumbStick.X < 0.5f && thumbStick.Y > 0)
        {
            return "up";
        }
        else if (thumbStick.X > -0.5 && thumbStick.X < 0.5f && thumbStick.Y < 0)
        {
            return "down";
        }
        else if (thumbStick.X < 0 && thumbStick.Y < 0.5f && thumbStick.Y > -0.5f)
        {
            return "left";
        }
        else if (thumbStick.X > 0 && thumbStick.Y < 0.5f && thumbStick.Y > -0.5f)
        {
            return "right";
        }
        else
        {
            return null;
        }
    }

    public bool AnyKeyPressed
    {
        get { return currentKeyboardState.GetPressedKeys().Length > 0 && previousKeyboardState.GetPressedKeys().Length == 0; }
    }
}

