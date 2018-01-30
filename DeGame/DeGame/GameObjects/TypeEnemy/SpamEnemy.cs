using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpamEnemy : Enemy
{
    int Counter = 100;
    int BulletCounter = 0;
    float speed = 0.2f;

    public SpamEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/CutieEnemyPixel", Difficulty, layer, id)
    {
        position = startPosition;
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet");
        basevelocity = new Vector2(0, 0);
        bulletdamage = 3 * statmultiplier;
        health = 100 * statmultiplier;
        maxhealth = 100 * statmultiplier;
        expGive = 120 * statmultiplier;
        contactdamage = 2 * statmultiplier;
    }

    public void Range(GameTime gameTime)
    {
        Counter -= 1 * gameTime.ElapsedGameTime.Milliseconds;

        if (Counter <= 0)
        {
            Shoot();
            BulletCounter += 1;
            Counter = 100;
        }
        if (BulletCounter == 20)
        {
            BulletCounter = 0;
            Counter = 2000;
        }
    }

    public void Shoot()
    {
        Vector2 direction = (PlayingState.player.position - position);
        Vector2 middleofsprite = new Vector2(sprite.Width / 4, sprite.Height / 4);
        EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + middleofsprite, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/FireBullet"));
        PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);

    }
    public override void Update(GameTime gameTime)
    {
        direction = (PlayingState.player.position - position);
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Range(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CutieEnemyPixel"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}