using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class MinionSpawner : Enemy
{
    new int counter = 100;
    Vector2 Roomposition;

    public MinionSpawner(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/Spawner", Difficulty,layer, id)
    {
        health = 200 * statmultiplier;
        maxhealth = 200 * statmultiplier;
        position = startPosition;
        Roomposition = roomposition;
        backgroundenemy = true;
        drop = false;
        bossenemy = true;
        expGive = 0;
    }

    /// <summary>
    /// If the Boss has triggered it: Spawns new enemies
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        counter--;
        if (EndRoom.trigger)
        {

            if (counter <= 0)
            {
                ChasingMinion minion = new ChasingMinion(position + new Vector2(0, GameEnvironment.assetManager.GetSprite("Sprites/Enemies/Spawner").Height * 0.8f), Roomposition, PlayingState.currentFloor.displayint);
                PlayingState.currentFloor.currentRoom.addedenemies.Add(minion);
                counter = 400;
            }
        }
        else
            counter = 100;
    }

    /// <summary>
    /// Draw Enemy
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/Spawner"), position, color);
    }
}
