using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RandomEnemy : Enemy
{
    int Counter = 0;
    float speed = 0.2f;
    Vector2 direction;

    public RandomEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/RandomEnemy", Difficulty, layer, id)
    {
        position = startPosition;
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet");
        basevelocity = new Vector2(0.06f, 0.06f);
        velocity = basevelocity;
        bulletdamage = 5 * statmultiplier;
        health = 120 * statmultiplier;
        maxhealth = 100 * statmultiplier;
        expGive = 120 * statmultiplier;
        contactdamage = 2 * statmultiplier;
    }

    public void Shoot(GameTime gameTime)
    {
        Counter -= 1 * gameTime.ElapsedGameTime.Milliseconds;

        if (Counter <= 0)
        {
            Vector2 bulletPosition = new Vector2(sprite.Width / 2, 50);
            EnemyBullet bullet1 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(0, -1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            EnemyBullet bullet2 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(1, -1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            EnemyBullet bullet3 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(1, 0), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            EnemyBullet bullet4 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(1, 1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            EnemyBullet bullet5 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(0, 1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            EnemyBullet bullet6 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-1, 1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            EnemyBullet bullet7 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-1, 0), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            EnemyBullet bullet8 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-1, -1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet1);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet2);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet3);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet4);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet5);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet6);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet7);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet8);
            Counter = 1000;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        direction = (PlayingState.player.position - position);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
        {
            Shoot(gameTime);
            Chase();
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/RandomEnemy"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}