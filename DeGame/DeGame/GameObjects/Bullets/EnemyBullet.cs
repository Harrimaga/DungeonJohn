using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//TODO: Damage regulation;
//TODO: Friendly Fire?;
class EnemyBullet : E_Bullet
{
    public Vector2 direction;
    float speed, damage;
    public EnemyBullet(float Damage, float Speed, Vector2 Startpositon, Vector2 Direction, int layer = 0, string id = "EnemyBullet") : base(Damage, Speed, "Sprites/Random", layer, id)
    {
        position = Startpositon;
        speed = Speed;
        damage = Damage;
        direction = Direction;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        direction.Normalize();
        position += direction * speed;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/EnemyBullet"), position);
    }
}
