using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Lava : Tiles
{
    float LavaDamage = 10;
    bool check = false;

    public Lava(Vector2 startPosition, int layer = 0, string id = "Lava")
    : base(startPosition, layer, id)
    {
        position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        /// When the player wears coolboots, the damage the lava does is reduced
        if (PlayingState.player.CoolBoots && !check)
        {
            LavaDamage -= 5;
            check = true;
        }
        /// When the player stops wearing the coolboots, th lavadamage is being reset
        if (!PlayingState.player.CoolBoots && check)
        {
            LavaDamage += 5;
            check = false;
        }
        /// Here the player takes damage when he/she walks over lava and he/she is not wearing a helicopterhat
        if (BoundingBox.Intersects(PlayingState.player.collisionhitbox) && PlayingState.currentFloor.currentRoom.lavatimer == 0)
        {
            if(!PlayingState.player.HelicopterHat)
            {
                PlayingState.player.TakeDamage(LavaDamage);
                PlayingState.currentFloor.currentRoom.lavatimer += 30;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Lava"), position);
    }
}
