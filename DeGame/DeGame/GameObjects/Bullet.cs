using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Bullet : SpriteGameObject
{
    public Bullet(Vector2 Startposition, int Direction, int layer = 0, string id = "bullet")
    : base("Sprites/Random", layer, id)
    {
        position = Startposition;

        // Determine the direction of the bullets
        if (Direction == 0)
            velocity.Y = 10;
        else if (Direction == 1)
            velocity.X = -10;
        else if (Direction == 2)
            velocity.Y = -10;
        else if (Direction == 3)
            velocity.X = 10;
    }

    // Update the bullets
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position += velocity;
        CheckCollisionEnemies(); // Laat de game laggen als de tering
    }

    public void CheckCollisionEnemies()
    {
        /*if (CollidesWith(PlayingState.enemy))
        {
            PlayingState.enemy.Die = true;
        }*/
        // Als enemies in de room class zitten, en ze hebben op een of andere manier t goede ID, zou dit moeten werken
    }

    // Draw the bullets
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }
}
