using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShopItem : SpriteGameObject
{
    public ShopItem(Vector2 startPosition, int layer = 0, string id = "Rock")
    : base("Sprites/Rock Sprite", layer, id)
    {

    }
}

