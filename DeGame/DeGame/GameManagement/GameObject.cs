using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject : IGameObject
    {
    protected GameObject parent;
    public Vector2 position, velocity;
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

    public virtual void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
    }

    /// <summary>
    /// Calls the update for the GameObject
    /// </summary>
    /// <param name="gameTime">Current GameTime</param>
    public virtual void Update(GameTime gameTime)
    {
       position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    /// <summary>
    /// Calls the draw for the GameObject
    /// </summary>
    /// <param name="gameTime">Current GameTime</param>
    /// <param name="spriteBatch">The spritebatch</param>
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    /// <summary>
    /// Gets or sets the parent
    /// </summary>
    public virtual GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    /// <summary>
    /// Gets or sets the layer
    /// </summary>
    public virtual int Layer
    {
        get { return layer; }
        set { layer = value; }
    }

    /// <summary>
    /// Gets the ID
    /// </summary>
    public string Id
    {
        get { return id; }
    }

    /// <summary>
    /// Gets or sets the position
    /// </summary>
    public virtual Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    /// <summary>
    /// Gets the global position
    /// </summary>
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

    /// <summary>
    /// Calculates the boundingbox
    /// </summary>
    public virtual Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
        }
    }

    /// <summary>
    /// Reset the gameobject
    /// </summary>
    public virtual void Reset()
    {
    }
}
