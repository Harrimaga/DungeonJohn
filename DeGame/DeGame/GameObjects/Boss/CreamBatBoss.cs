using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class CreamBatBoss : Boss
{
    Vector2 Roomposition;
    int shootcounter1, shootcounter2, shootcounter3 = 50;
    int MoveCounter = 0;
    int Stage = 1;
    int MoveTimer = 0;
    float bulletdamage = 10, speed = 5;
    Vector2 moving, MoveDirection, bulletPosition;

    public CreamBatBoss(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Boss") : base(startPosition, roomposition, "Sprites/Enemies/CreamBatSprite1", Difficulty, layer, id)
    {
        Roomposition = roomposition;
        position = startPosition;
        expGive = 240;
        maxhealth = 300;
        health = maxhealth;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite1");
        velocity = Vector2.Zero;
        MoveDirection = Vector2.Zero;
        MoveDirection.Normalize();
        bulletPosition = new Vector2(sprite.Width / 2, 50);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        MoveTimer++;
        shootcounter1--;
        if (Stage == 1)
        {
            velocity = new Vector2(2, 2);
            Shoot();
            Move();
            if (health <= 0)
            {
                Stage = 2;
                maxhealth = 500;
                health = 500;
            }
        }
        if (Stage == 2)
        {
            velocity = new Vector2(4, 4);
            Shoot();
            BossChase();
            if (health <= 0)
            {
                Stage = 3;
                maxhealth = 800;
                health = 800;
            }
        }
        if (Stage == 3)
        {
            velocity = new Vector2(1.5f, 1.5f);
            Spam();
            BossChase();
            if (health <= 0)
            {
                FinalStage();
            }
        }

    }

    public void Shoot()
    {
        if (shootcounter1 <= 0)
        {
            Vector2 ShootDirection = (PlayingState.player.position - position);
            ShootDirection.Normalize();
            Vector2 bulletPosition = new Vector2(sprite.Width / 2, 50);
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + bulletPosition, ShootDirection, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
            if (Stage == 1)
                shootcounter1 = 40;
            if (Stage == 2)
                shootcounter1 = 20;
            if (Stage == 3)
                shootcounter1 = 10;
        }
    }

    public void Spam()
    {
        shootcounter2--;
        shootcounter3--;
        if (shootcounter2 <= 0)
        {
            Vector2 bulletPosition = new Vector2(sprite.Width / 2, 50);
            EnemyBullet bullet1 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(0, -1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet2 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(1, -1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet3 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(1, 0), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet4 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(1, 1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet5 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(0, 1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet6 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-1, 1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet7 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-1, 0), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet8 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-1, -1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet1);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet2);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet3);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet4);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet5);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet6);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet7);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet8);
            MoveCounter++;
            shootcounter2 = 50;

        }
        if (shootcounter3 <= 25)
        {
            Vector2 bulletPosition = new Vector2(sprite.Width / 2, 50);
            EnemyBullet bullet1 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(0.5f, -1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet2 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(1, -0.5f), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet3 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(1, 0.5f), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet4 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(0.5f, 1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet5 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-0.5f, 1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet6 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-1, 0.5f), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet7 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-1f, -0.5f), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            EnemyBullet bullet8 = new EnemyBullet(bulletdamage, speed, position + bulletPosition, new Vector2(-0.5f, -1), GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet1);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet2);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet3);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet4);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet5);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet6);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet7);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet8);
            MoveCounter++;
            shootcounter3 = 75;

        }
        if (MoveCounter == 4)
        {
            MoveCounter = 0;
            shootcounter2 = 100;
            shootcounter3 = 100;
        }

    }

    public void BossChase()
    {
        MoveDirection = PlayingState.player.position - position;
        MoveDirection.Normalize();
        moving = MoveDirection * velocity;
        position += moving;
    }
    public void Move()
    {
        if (MoveTimer <= 50)
            MoveDirection = new Vector2(0,1.5f);
        if (MoveTimer <= 150 && MoveTimer >= 50)
            MoveDirection = new Vector2(-2, -1);
        if (MoveTimer <= 250 && MoveTimer >= 150)
            MoveDirection = new Vector2(2, -1);
        if (MoveTimer <= 350 && MoveTimer >= 250)
            MoveDirection = new Vector2(2, 1);
        if (MoveTimer <= 450 && MoveTimer >= 350)
            MoveDirection = new Vector2(-2, 1);
        if (MoveTimer >= 450)
            MoveTimer = 50;
        moving = MoveDirection * velocity;
        position += moving;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (Stage == 1)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite1"), position);
        if (Stage == 2)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite2"), position);
        if (Stage == 3)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite3"), position);
    }
}

