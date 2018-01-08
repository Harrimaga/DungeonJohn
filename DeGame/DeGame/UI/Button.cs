using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Button : SpriteGameObject
{
    protected bool pressed,hover;
    Player player;
    string buttontext;
    Vector2 buttonposition;
    string textureNormal, texturePressed;
    int spriteWidth, spriteHeight;
    bool withouttext;

    public Button(Vector2 startposition, string text, string imageAsset, string imageAssetPressed,bool withoutText, int layer = 0, string id = "")
        : base("Sprites/Button Sprite", 0, "button")
    {
        pressed = false;
        withouttext = withoutText;
        buttonposition = startposition;
        buttontext = text;
        textureNormal = imageAsset;
        texturePressed  = imageAssetPressed;
        spriteWidth = GameEnvironment.assetManager.GetSprite("Sprites/" + imageAsset).Width;
        spriteHeight = GameEnvironment.assetManager.GetSprite("Sprites/" + imageAsset).Height;
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(position.X - origin.X);
            int top = (int)(position.Y - origin.Y);
            return new Rectangle(left, top, spriteWidth, spriteHeight);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonDown() && BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
        hover = BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Update(GameTime gameTime)
    {
        position = buttonposition + new Vector2(Camera.Position.X - (GameEnvironment.WindowSize.X / 2), Camera.Position.Y - (GameEnvironment.WindowSize.Y / 2));
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
            return;

        if(hover)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/" + texturePressed), position, Color.White);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/" + textureNormal), position, Color.White);

        if(!withouttext)
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