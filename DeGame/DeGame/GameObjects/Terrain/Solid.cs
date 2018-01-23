using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Solid : SpriteGameObject     
{
    public bool OnThisTile = false, hittable = true;

    public Solid(Vector2 startPosition, int layer = 0, string id = "Rock")
    : base("Sprites/Tiles/Rock Sprite", layer, id)
    {
        position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
        {
            OnThisTile = true;
        }
        else
            OnThisTile = false;
        SolidCollision();
    }

    void SolidCollision()
    {
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Center.X, PlayingState.player.collisionhitbox.Top)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.Y++;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Center.X, PlayingState.player.collisionhitbox.Bottom)))
            while (CollidesWith(PlayingState.player))
            {
                PlayingState.player.position.Y--;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Left, PlayingState.player.collisionhitbox.Center.Y)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.X++;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Right, PlayingState.player.collisionhitbox.Center.Y)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.X--;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Rock Sprite"), position);
    }
}

