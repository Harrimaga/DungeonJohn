using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MinionBoss : Boss
{
    Vector2 Roomposition;
    int Counter = 150, spawncounter = 0, spawncountercooldown;
    float bulletdamage = 20, speed = 2;
    EnemyBullet bullet;

    public MinionBoss(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        Roomposition = roomposition;
        expGive = 240;
        maxhealth = 400;
        health = maxhealth;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!EndRoom.trigger)
            Shoot();
        if (spawncountercooldown > 0)
            spawncountercooldown--;
        else
            spawncounter = 400;
        if (spawncounter > 0)
        {
            if (health < 300 && spawncountercooldown <= 0)
            {
                EndRoom.trigger = true;
                spawncounter--;
            }
        }
        else
        {
            spawncountercooldown = 400;
            EndRoom.trigger = false;
        }        
    }

    public void Shoot()
    {
        Vector2 bulletposition;
        Vector2 direction;
        Counter--;
        if (Counter <= 0)
        {
            direction = (PlayingState.player.position - position);
            bulletposition = position + new Vector2(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss").Width / 2, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss").Height * .6f);
            bullet = new EnemyBullet(bulletdamage, speed, bulletposition, direction,GameEnvironment.assetManager.GetSprite("Sprites/Bullets/MinionBossBullet"));
            Room.enemybullets.Add(bullet);
            Counter = 150;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/MinionBoss"), position);
    }
}
