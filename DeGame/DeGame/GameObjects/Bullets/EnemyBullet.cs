using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//TODO: Damage regulation;
//TODO: Friendly Fire?;
class EnemyBullet : E_Bullet
{
    Vector2 direction;
    float speed, damage;
    Texture2D Bulletsprite;
    public EnemyBullet(float Damage, float Speed, Vector2 Startpositon, Vector2 Direction, Texture2D bulletsprite, int layer = 0, string id = "EnemyBullet") : base(Damage, Speed,"Sprites/Bullets/EnemyBullet", layer, id)
    {
        position = Startpositon;
        direction = (PlayingState.player.position - position);
        Position = Startpositon;
        speed = Speed;
        damage = Damage;
        direction = Direction;
        Bulletsprite = bulletsprite;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Position = position;
        direction.Normalize();
        position += direction * speed;
        //if (changedirection)
        {
            direction = CalculateReflect(direction);
            changedirection = false;
        }
    }

    Vector2 CalculateReflect(Vector2 direction)
    {
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2);
        Vector2 newdirection = direction;
        if (position.X < MiddleofPlayer.X - GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2 || position.X > MiddleofPlayer.X + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2)
        {
            newdirection.X = -direction.X;
        }
        if (position.Y < MiddleofPlayer.Y - GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2 || position.Y > MiddleofPlayer.Y + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2)
        {
            newdirection.Y = -direction.Y;
        }
        return newdirection;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"), position);

    }
}
