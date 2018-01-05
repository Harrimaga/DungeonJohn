using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        if (PlayingState.player.Cool_Boots == true && check == false)
        {
            LavaDamage -= 5;
            check = true;
        }
        if (CollidesWith(PlayingState.player) && PlayingState.currentFloor.currentRoom.lavatimer == 0)
        {
            PlayingState.player.health -= LavaDamage;
            PlayingState.currentFloor.currentRoom.lavatimer += 30;
        }
    }

         public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Lava"), position);
    }
}
