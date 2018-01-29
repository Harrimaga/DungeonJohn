using Microsoft.Xna.Framework;
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
        expGive = (int)(240 * statmultiplier);
        maxhealth = 400 * statmultiplier;
        bulletdamage = 35 * statmultiplier;
        max = 1500 / statmultiplier;
        shootcounter = (int)max;
        health = maxhealth;  
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!EndRoom.trigger || health < 150)
            Shoot(gameTime);
        if (health < 300 && !EndRoom.cleared)
        {
            EndRoom.trigger = true;
        }
        FinalStage();
    }

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

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (LevelofBoss == 0 || poisoncounter > 0)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss"), position, color);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss"), position, Color.MediumVioletRed);

    }
}
