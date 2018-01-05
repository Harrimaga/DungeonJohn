using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Lava : Tiles
{
    int counter = 0;
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
        if (CollidesWith(PlayingState.player) && counter == 0)
        {
                PlayingState.player.health -= LavaDamage;
                counter += 80;
        }
        if (counter > 0)
            counter--;
        if (counter < 0)
            counter = 0;
    }

         public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Lava"), position);
    }
}
