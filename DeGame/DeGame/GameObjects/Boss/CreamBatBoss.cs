using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class CreamBatBoss : Boss
{
    Vector2 Roomposition;
    int shootcounter1, shootcounter2, shootcounter3 = 50;
    int MoveCounter = 0;
    int Stage = 1;
    int MoveTimer = 0;
    float bulletdamage = 10, speed = 0.3f;
    Vector2 moving, MoveDirection, bulletPosition;
    Texture2D creambatsprite = GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite1");

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
        SolidCollision();
        if (Stage == 1)
        {
            velocity = new Vector2(0.13f, 0.13f);
            Shoot();
            Move(gameTime);
            if (health <= 0)
            {
                Stage = 2;
                maxhealth = 500;
                health = 500;
                creambatsprite = GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite2");
            }
        }
        if (Stage == 2)
        {
            velocity = new Vector2(0.26f, 0.26f);
            Shoot();
            BossChase(gameTime);
            if (health <= 0)
            {
                Stage = 3;
                maxhealth = 800;
                health = 800;
                creambatsprite = GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite3");
            }
        }
        if (Stage == 3)
        {
            velocity = new Vector2(0.1f, 0.1f);
            Spam();
            BossChase(gameTime);
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
            Vector2 ShootDirection = PlayerOrigin - (position + bulletPosition);
            ShootDirection.Normalize();
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

    public void BossChase(GameTime gameTime)
    {
        MoveDirection = PlayingState.player.position - position;
        MoveDirection.Normalize();
        moving = MoveDirection * velocity;
        position += moving * gameTime.ElapsedGameTime.Milliseconds;
      
    }
    public void Move(GameTime gameTime)
    {
        if (MoveTimer <= 50)
            MoveDirection = new Vector2(0,1.5f);
        else if (MoveTimer <= 150 && MoveTimer >= 50)
            MoveDirection = new Vector2(-2, -1);
        else if (MoveTimer <= 250 && MoveTimer >= 150)
            MoveDirection = new Vector2(2, -1);
        else if (MoveTimer <= 350 && MoveTimer >= 250)
            MoveDirection = new Vector2(2, 1);
        else if (MoveTimer <= 450 && MoveTimer >= 350)
            MoveDirection = new Vector2(-2, 1);
        else if (MoveTimer >= 450)
            MoveTimer = 50;
        moving = MoveDirection * velocity;
        position.X += moving.X * gameTime.ElapsedGameTime.Milliseconds;
        position.Y += moving.Y * gameTime.ElapsedGameTime.Milliseconds;
    }

    protected void SolidCollision()
    {
        //Rectangle BossBox = new Rectangle((int)position.X - 61, (int)position.Y - 61, sprite.Width + 122, sprite.Height + 122);
        foreach (Solid s in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].solid.Children)
        {
            //if (BossBox.Intersects(s.BoundingBox) == false)
            //{
                if (CollidesWith(s))
                {
                    Console.WriteLine("Fucksafdssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
                    if (moving.X > 0)
                        position.X -= moving.X;
                    if (moving.X < 0)
                        position.X += moving.X;
                    if (moving.Y > 0)
                        position.Y -= moving.Y;
                    if (moving.Y < 0)
                        position.Y += moving.Y;
                }
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
            // }
            // else
            //{
            //if (CollidesWith(s))
            //{
            //    Console.WriteLine("Fucksafdssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
            //    if (moving.X > 0)
            //        position.X -= moving.X;
            //    if (moving.X < 0)
            //        position.X += moving.X;
            //    if (moving.Y > 0)
            //        position.Y -= moving.Y;
            //    if (moving.Y < 0)
            //        position.Y += moving.Y;
            //}
            // }
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(creambatsprite, position, color);
    }
}

