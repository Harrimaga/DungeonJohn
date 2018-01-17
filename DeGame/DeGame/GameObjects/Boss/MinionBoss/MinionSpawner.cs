using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MinionSpawner : Enemy
{
    new int counter = 100;
    Vector2 Roomposition;

    public MinionSpawner(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/Spawner",layer, id)
    {
        maxhealth = 200;
        health = 200;
        position = startPosition;
        Roomposition = roomposition;
        backgroundenemy = true;
        drop = false;
        bossenemy = true;
        expGive = 0;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        counter--;
        if (EndRoom.trigger)
        {

            if (counter <= 0)
            {
                ChasingMinion minion = new ChasingMinion(position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/Spawner").Height * 0.8f), Roomposition);
                PlayingState.currentFloor.currentRoom.addedenemies.Add(minion);
                counter = 400;
            }
        }
        else
            counter = 100;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/Spawner"), position);
    }
}
