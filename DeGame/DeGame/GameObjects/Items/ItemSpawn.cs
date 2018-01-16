using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ItemSpawn : SpriteGameObject
{
    //betere naam voor de class is welkom
    //als het een shopitem moet zijn dan is price true
    Item item;
    ItemList itemList;
    bool price, pickedUp = false;
    Random random = new Random();

    public ItemSpawn(Vector2 startPosition,bool Price, int layer = 0, string id = "ItemAltar")
    : base("Sprites/Items/Altar", layer, id)
    {
        position = startPosition;
        price = Price;
        itemList = new ItemList();
        RandomItem();
    }
    void RandomItem()
    {
        if (!price)
        {
            int r = random.Next(itemList.RoomList.Count);
            item = itemList.RoomList[r];
        }
        if(price)
        {
            int r = random.Next(itemList.ShopList.Count);
            item = itemList.ShopList[r];
        }

        /*
        int r = random.Next(2);
        if (r == 0)
            item = new Mac10();
        else
            item = new HardHelmet();*/
        //TODO kiest een random item
    }
    public override void Update(GameTime gameTime)
    {
        if (CollidesWith(PlayingState.player) && !pickedUp && (PlayingState.player.gold >= 5 || !price))
        {
            Player.inventory.addItemToInventory(item);
            pickedUp = true;
            if(price)
            {
                PlayingState.player.gold -= 5;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/Altar"), position);
        if (price)
        {
            //price voor onder de shopitem
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "5", position + new Vector2(18, 60), Color.Yellow);
        }
        if (!pickedUp)
        {
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName), position - new Vector2(0, 30));
            //TODO draw item van op de altar tot het gepakt is
        }
    }
}

