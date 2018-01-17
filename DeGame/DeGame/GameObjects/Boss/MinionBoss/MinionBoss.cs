using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MinionBoss : Boss
{
    Vector2 Roomposition;
    int shootcounter = 150, spawncounter;
    float bulletdamage = 20, speed = 2;
    EnemyBullet bullet;

    public MinionBoss(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, "Sprites/Enemies/MinionBoss",layer, id)
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
        if (health < 300 && !EndRoom.cleared)
        {
            EndRoom.trigger = true;
        }
    }

    public void Shoot()
    {
        shootcounter--;
        if (shootcounter <= 0)
        {
            Vector2 bulletposition = new Vector2(sprite.Width / 2, sprite.Height * .6f);
            Vector2 direction = (PlayingState.player.position - position);
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + bulletposition, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/MinionBossBullet"));
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
