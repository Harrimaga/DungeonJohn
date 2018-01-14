using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MinionBoss : Boss
{
    Vector2 Roomposition;
    int shootcounter = 150, spawncounter;
    float bulletdamage = 20, speed = 2;
    EnemyBullet bullet;

    public MinionBoss(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        Roomposition = roomposition;
        position = startPosition;
        expGive = 240;
        maxhealth = 400;
        health = maxhealth;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!EndRoom.trigger || health < 150)
            Shoot();
        if (health < 300)
        {
            EndRoom.trigger = true;
        }
    }

    public void Shoot()
    {
        Vector2 bulletposition;
        Vector2 direction;
        shootcounter--;
        if (shootcounter <= 0)
        {
            direction = (PlayingState.player.position - position);
            bulletposition = position + new Vector2(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss").Width / 2, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss").Height * .6f);
            bullet = new EnemyBullet(bulletdamage, speed, bulletposition, direction,GameEnvironment.assetManager.GetSprite("Sprites/Bullets/MinionBossBullet"));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
            shootcounter = 150;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss"), position);
    }
}
