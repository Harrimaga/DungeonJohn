using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ItemAltar : SpriteGameObject
{
    //betere naam voor de class is welkom
    //als het een shopitem moet zijn dan is price true
    bool itemGone = false, price;
    public ItemAltar(Vector2 startPosition,bool Price, int layer = 0, string id = "ItemAltar")
    : base("Sprites/Items/Altar", layer, id)
    {
        position = startPosition;
        price = Price;
        RandomItem();
    }
    void RandomItem()
    {
        //TODO kiest een random item
    }
    void AddItemPlayer()
    {
        //TODO weet nog niet hoe je items aan player geeft
    }
    public override void Update(GameTime gameTime)
    {
        if (CollidesWith(PlayingState.player) && (PlayingState.player.gold > 10 || !price))
        {
            AddItemPlayer();
            itemGone = true;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/Altar"), position);
        if (price)
        {
            //price voor onder de shopitem
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "10", position + new Vector2(18, 60), Color.Yellow);
        }
        if (itemGone)
        {
            //TODO draw item van op de altar tot het gepakt is
        }
    }
}

