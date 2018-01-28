using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TurretEnemy : Enemy
{
    int Counter = 0, Directioncount;
    float speed = 0.2f;
    Vector2 BulletPosition;

    public TurretEnemy(Vector2 startPosition, Vector2 roomposition, int directioncount, int Difficulty = 0, int layer = 0, string id = "TurretEnemy") : base(startPosition, roomposition, "Sprites/Enemies/TurretEnemyUp", Difficulty, layer, id)
    {
        Directioncount = directioncount;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyRight");
        bulletdamage = 7 * statmultiplier;
        killable = false;

        switch (Directioncount)
        {
            case 1:
                direction = new Vector2(0, -1);
                BulletPosition = new Vector2(sprite.Width / 2 - 15, 0);
                break;
            case 2:
                direction = new Vector2(0, 1);
                BulletPosition = new Vector2(sprite.Width / 2 - 15, sprite.Height);
                break;
            case 3:
                direction = new Vector2(-1, 0);
                BulletPosition = new Vector2(0, sprite.Height / 2 - 15);
                break;
            default:
                direction = new Vector2(1, 0);
                BulletPosition = new Vector2(sprite.Width, sprite.Height / 2 - 15);
                break;
        }
        
    }

    public void Shoot()
    {
        Counter++;

        if (Counter >= 70)
        {
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + BulletPosition, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
            Counter = 0;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Shoot();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        switch (Directioncount)
        {
            case 1:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyUp"), position, color);
                break;
            case 2:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyDown"), position, color);
                break;
            case 3:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyLeft"), position, color);
                break;
            default:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyRight"), position, color);
                break;
        }
    }
}
