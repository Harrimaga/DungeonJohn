using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MinionSpawner : Enemy
{
    int counter = 100;
    Vector2 Roomposition;

    public MinionSpawner(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        maxhealth = 150;
        position = startPosition;
        Roomposition = roomposition;
    }

    void Update(GameTime gameTime)
    {
        if (EndRoom.trigger)
        {
            counter--;
            if (counter <= 0)
            {
                ChasingMinion minion = new ChasingMinion(position, Roomposition);
                Room.enemies.Add(minion);
                counter = 50;
            }
        }
    }

    void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

    }
}
