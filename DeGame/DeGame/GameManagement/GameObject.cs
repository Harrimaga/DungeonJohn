using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject : IGameObject
    {

    protected GameObject parent;
    protected Vector2 position, velocity;
    protected int layer;
    protected string id;
    protected bool visible;
    public GameObject(int layer = 0, string id = "")
    {
        this.layer = layer;
        this.id = id;
        position = Vector2.Zero;
        velocity = Vector2.Zero;
        visible = true;
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public virtual int Layer
    {
        get { return layer; }
        set { layer = value; }
    }

    public string Id
    {
        get { return id; }
    }

    public virtual Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public virtual Vector2 GlobalPosition
    {
        get
        {
            if (parent != null)
            {
                return parent.GlobalPosition + Position;
            }
            else
            {
                return Position;
            }
        }
    }

    public virtual Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
        }
    }

    public virtual void Reset()
    {
    }
}
