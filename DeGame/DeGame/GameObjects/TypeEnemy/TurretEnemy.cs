using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TurretEnemy : Enemy
{
    int Counter = 0, Directioncount;
    float bulletdamage;
    float speed = 3f;
    Vector2 direction, MiddleOfSprite;

    public TurretEnemy(Vector2 startPosition, Vector2 roomposition, int directioncount, int Difficulty = 0, int layer = 0, string id = "TurretEnemy") : base(startPosition, roomposition, "Sprites/Enemies/TurretEnemyUp", Difficulty, layer, id)
    {
        Directioncount = directioncount;
        switch (Directioncount)
        {
            case 1:
                direction = new Vector2(0, -1);
                MiddleOfSprite = new Vector2(sprite.Width / 2 - 35, -10);
                break;
            case 2:
                direction = new Vector2(0, 1);
                MiddleOfSprite = new Vector2(sprite.Width / 2 - 25, sprite.Height - 25);
                break;
            case 3:
                direction = new Vector2(-1, 0);
                MiddleOfSprite = new Vector2(-10, sprite.Height / 2 - 25);
                break;
            default:
                direction = new Vector2(1, 0);
                MiddleOfSprite = new Vector2(sprite.Width - 35, sprite.Height / 2 - 35);
                break;
        }
        bulletdamage = 3 * statmultiplier;
        killable = false;
    }

    public void Shoot()
    {
        Counter++;

        if (Counter >= 50)
        {
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + MiddleOfSprite, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
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
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyUp"), position);
                break;
            case 2:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyDown"), position);
                break;
            case 3:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyLeft"), position);
                break;
            default:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyRight"), position);
                break;
        }
    }
}
