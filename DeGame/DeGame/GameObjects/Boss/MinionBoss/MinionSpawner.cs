using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MinionSpawner : Enemy
{
    int counter = 100;
    Vector2 Roomposition;

    public MinionSpawner(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        maxhealth = 200;
        health = 200;
        position = startPosition;
        Roomposition = roomposition;
        backgroundenemy = true;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        counter--;
        //if (EndRoom.trigger)
        {

            if (counter <= 0)
            {
                ChasingMinion minion = new ChasingMinion(position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Spawner").Height * 0.8f), Roomposition);
                PlayingState.currentFloor.currentRoom.addedenemies.Add(minion);
                counter = 250;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Spawner"), position);
    }
}
