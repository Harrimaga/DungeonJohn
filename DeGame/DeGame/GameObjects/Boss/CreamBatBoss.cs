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
    float bulletdamage = 0, speed = 0.3f;
    Vector2 moving, MoveDirection, bulletPosition;
    Texture2D creambatsprite = GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite1");

    public CreamBatBoss(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Boss") : base(startPosition, roomposition, "Sprites/Enemies/CreamBatSprite1", Difficulty, layer, id)
    {
        Roomposition = roomposition;
        position = startPosition;
        expGive = 300 * statmultiplier;
        maxhealth = 300;
        contactdamage = 10;
        health = maxhealth;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CreamBatSprite1");
        velocity = Vector2.Zero;
        MoveDirection = Vector2.Zero;
        MoveDirection.Normalize();
        bulletPosition = new Vector2(sprite.Width / 2, 50);
        if (endless)
            EndRoom.finalboss = false;
        else
            EndRoom.finalboss = true;
    }
    
    /// <summary>
    /// Execute Solidcollision and Handle the Stages
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        shootcounter1 -= 1 * gameTime.ElapsedGameTime.Milliseconds;
        SolidCollision();

        if (Stage == 1)
        {
            if (CollidesWith(PlayingState.player))
            {

                velocity = Vector2.Zero;

            }
            else
            {
                velocity = new Vector2(0.15f, 0.15f);
                MoveTimer++;
            }
            bulletdamage = 10;
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
            if (CollidesWith(PlayingState.player))
            {

                velocity = Vector2.Zero;

            }
            else
            {
                velocity = new Vector2(0.20f, 0.20f);
            }
            bulletdamage = 10;
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
            if (CollidesWith(PlayingState.player))
            {

                velocity = Vector2.Zero;

            }
            else
            {
                velocity = new Vector2(0.1f, 0.1f);
            }
            bulletdamage = 5;
            Spam(gameTime);
            BossChase(gameTime);
            if (health <= 0)
            {
                FinalStage();
            }
        }
    }

    /// <summary>
    /// Shoots bullets directly at the player
    /// </summary>
    public void Shoot()
    {
        if (shootcounter1 <= 0)
        {
            Vector2 ShootDirection = PlayerOrigin - (position + bulletPosition);
            ShootDirection.Normalize();
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + bulletPosition, ShootDirection, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/CreamBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
            if (Stage == 1)
                shootcounter1 = 1000;
            if (Stage == 2)
                shootcounter1 = 1100;
        }
    }

    /// <summary>
    /// Shoots bullets in a pre-determined pattern 
    /// </summary>
    /// <param name="gameTime"></param>
    public void Spam(GameTime gameTime)
    {
        shootcounter2 -= 1 * gameTime.ElapsedGameTime.Milliseconds; ;
        shootcounter3 -= 1 * gameTime.ElapsedGameTime.Milliseconds; ;
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
            shootcounter2 = 1000;

        }
        if (shootcounter3 <= 250)
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
            shootcounter3 = 1750;
        }
        if (MoveCounter == 4)
        {
            MoveCounter = 0;
            shootcounter2 = 1000;
            shootcounter3 = 1000;
        }
    }
    
    /// <summary>
    /// Makes the Boss chase after the player (Stage 2 & 3)
    /// </summary>
    /// <param name="gameTime"></param>
    public void BossChase(GameTime gameTime)
    {
        MoveDirection = PlayingState.player.position - position;
        MoveDirection.Normalize();
        moving = MoveDirection * velocity;
        position += moving * gameTime.ElapsedGameTime.Milliseconds;
    }

    /// <summary>
    /// Makes the Boss follow a pre-determined path(Stage1)
    /// </summary>
    /// <param name="gameTime"></param>
    public void Move(GameTime gameTime)
    {
        if (MoveTimer < 50)
            MoveDirection = new Vector2(0,1.5f);
        else if (MoveTimer <= 150 && MoveTimer > 50)
            MoveDirection = new Vector2(-1.5f, -1);
        else if (MoveTimer <= 250 && MoveTimer > 150)
            MoveDirection = new Vector2(1.5f, -1);
        else if (MoveTimer <= 350 && MoveTimer > 250)
            MoveDirection = new Vector2(1.5f, 1);
        else if (MoveTimer <= 450 && MoveTimer > 350)
            MoveDirection = new Vector2(-1.5f, 1);
        else if (MoveTimer > 450)
            MoveTimer = 50;
        moving = MoveDirection * velocity;
        position += moving * gameTime.ElapsedGameTime.Milliseconds;       
    }

    /// <summary>
    /// While the boss isnt in stage 1, when de boss collides with the solids(wall etc.) place him back. Preventing him from going through the solids.
    /// </summary>
    protected void SolidCollision()
    {
        if (Stage != 1)
        {

            foreach (Solid s in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].solid.Children)
            {

                while (CollidesWith(s))
                {
                    if (moving.X > 0)
                        position.X -= moving.X;
                    if (moving.X < 0)
                        position.X += moving.X;
                    if (moving.Y > 0)
                        position.Y -= moving.Y;
                    if (moving.Y < 0)
                        position.Y += moving.Y;
                }
            }
        }
    }

    /// <summary>
    /// Draw the CreamBat, depending on the Stage's variables(sprites, etc.).
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(creambatsprite, position, color);
    }
}

