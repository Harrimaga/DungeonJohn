﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MinionBoss : Boss
{
    Vector2 Roomposition;
    int shootcounter;
    float bulletdamage, speed = 0.8f, max;

    public MinionBoss(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Boss") : base(startPosition, roomposition, "Sprites/Enemies/MinionBoss", Difficulty,layer, id)
    {
        Roomposition = roomposition;
        position = startPosition;
        contactdamage = 20;
        expGive = 250 * statmultiplier;
        maxhealth = 400 * statmultiplier;
        bulletdamage = 35 * statmultiplier;
        max = 1500 / statmultiplier;
        shootcounter = (int)max;
        health = maxhealth;  
    }

    /// <summary>
    /// Executes the shoot method and turns the spawntrigger for its minions true when lower than 300hp.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Shoot(gameTime);
        if (health < 300 && !EndRoom.cleared)
        {
            EndRoom.trigger = true;
        }
        FinalStage();
    }

    /// <summary>
    /// Handles the shooting aimed at he player
    /// </summary>
    /// <param name="gameTime"></param>
    public void Shoot(GameTime gameTime)
    {
        shootcounter -= gameTime.ElapsedGameTime.Milliseconds;
        if (shootcounter <= 0)
        {
            Vector2 bulletposition = new Vector2(sprite.Width / 2, sprite.Height * .6f);
            Vector2 direction = PlayerOrigin - (position + bulletposition);
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + bulletposition, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/MinionBossBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
            shootcounter = (int)max;
        }
    }

    /// <summary>
    /// Draws the Boss depending on if its poisoned or not or level 0 or not
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (LevelofBoss == 0 || poisoncounter > 0)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss"), position, color);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss"), position, Color.MediumVioletRed);
    }
}
