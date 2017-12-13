using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SpriteGameObject : GameObject
{
    protected Vector2 origin;
    public bool PerPixelCollisionDetection = true;
    //public float UsesCameraX = 1;
    //public float UsesCameraY = 1;
    protected Texture2D sprite;

    public SpriteGameObject(string assetName, int layer = 0, string id = "")
        : base(layer, id)
    {
        this.sprite = GameEnvironment.assetManager.GetSprite(assetName);
    }
    public override void Update(GameTime gameTime)
    {
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(spriteBatch, GlobalPosition * Camera.CameraPosition, origin);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
    }


    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sprite.Width;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }

    public bool Mirror
    {
        get { return this.Mirror; }
        set { this.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public bool CollidesWith(SpriteGameObject obj)
    {
        return true; //placeholder
    }
}

