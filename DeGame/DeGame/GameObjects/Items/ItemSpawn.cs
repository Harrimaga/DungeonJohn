using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class ItemSpawn : SpriteGameObject
{
    //betere naam voor de class is welkom
    //als het een shopitem moet zijn dan is price true
    Item item;
    ItemList itemList;
    bool price, pickedUp = false;
    Random random = new Random();

    public ItemSpawn(Vector2 startPosition,bool Price, int randomint, int layer = 0, string id = "ItemAltar")
    : base("Sprites/Items/Altar", layer, id)
    {
        position = startPosition;
        price = Price;
        itemList = new ItemList();
        RandomItem(randomint);
    }
    void RandomItem(int randomint)
    {
        if (!price)
        {
            int r = randomint % itemList.RoomList.Count;
            item = itemList.RoomList[r];
        }
        if(price)
        {
            int r = randomint % itemList.ShopList.Count;
            item = itemList.ShopList[r];
        }
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
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "5", position + new Vector2(18, 60), Color.Yellow);
        }
        if (!pickedUp)
        {
            Texture2D itemSprite = GameEnvironment.assetManager.GetSprite("Sprites/Items/" + item.itemName);
            float scale = 50f / itemSprite.Height;
            if (itemSprite.Width * scale > 50f)
            {
                scale = 50f / itemSprite.Width;
            }
            Vector2 itemPosition = new Vector2(0, 0);
            itemPosition += position;
            itemPosition.Y -= 30;
            itemPosition.X -= -5 - 25 + (itemSprite.Width * scale) / 2;
            spriteBatch.Draw(itemSprite, itemPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}

