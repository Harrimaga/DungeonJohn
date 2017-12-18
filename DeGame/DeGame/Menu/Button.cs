using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Button : SpriteGameObject
{
    protected bool pressed;
    Player player;
    string buttontext;
    Vector2 buttonposition;

    public Button(Vector2 startposition, string text, string imageAsset, int layer = 0, string id = "")
        : base("Sprites/Button Sprite", 0, "button")
    {
        pressed = false;
        buttonposition = startposition;
        buttontext = text;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonDown() && BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Update(GameTime gameTime)
    {
        position = buttonposition + new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
            return;

        if(pressed)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Button2 Sprite"), position, Color.White);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Button Sprite"), position, Color.White);

        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), buttontext, position + new Vector2(12, 5), Color.White);
    }

    public override void Reset()
    {
        base.Reset();
        pressed = false;
    }

    public bool Pressed
    {
        get { return pressed; }
    }
}
