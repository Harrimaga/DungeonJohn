using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ChasingEnemy : Enemy
{
    public ChasingEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/BearEnemyPixel", Difficulty, layer, id)
    {
        basevelocity = new Vector2(1.3f, 1.3f);
        health = 120 * statmultiplier;
        maxhealth = 120 * statmultiplier;
        expGive = 120 * statmultiplier;
    }

    public override void Update(GameTime gameTime)
    {
        
        if(CollidesWith(PlayingState.player))
        {
            velocity = Vector2.Zero;
            counter--;
            if (counter == 0)
            {
               
                PlayingState.player.health -= 0;
                counter = 100;
            }
        }

        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }

        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Chase();
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/BearEnemyPixel"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


   