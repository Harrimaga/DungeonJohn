using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

class ItemSpawn : SpriteGameObject
{
    Item item;
    ItemList itemList;
    Rectangle hitbox;
    bool price,buy = false, pickedUp = false;
    Random random = new Random();

    public ItemSpawn(Vector2 startPosition,bool Price, int randomint, int layer = 0, string id = "ItemAltar")
    : base("Sprites/Items/Altar", layer, id)
    {
        position = startPosition;
        price = Price;
        itemList = new ItemList();
        hitbox =new Rectangle((int)position.X, (int)position.Y + 140, Width, Height);
        RandomItem(randomint);
        GameEnvironment.soundManager.loadSoundEffect("Pickup");
    }
    void RandomItem(int randomint)
    {
        if (!price)
        {
            int r = randomint % itemList.RoomList.Count;
            item = itemList.RoomList[r];
        }
        else if(price)
        {
            int r = randomint % itemList.ShopList.Count;
            item = itemList.ShopList[r];
        }
    }
    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if(inputHelper.KeyPressed(Keys.Space) && hitbox.Intersects(PlayingState.player.BoundingBox))
        {
            buy = true;
        }
    }
    public override void Update(GameTime gameTime)
    {

        if (((hitbox.Intersects(PlayingState.player.BoundingBox) && price) || CollidesWith(PlayingState.player)) && !pickedUp && ((PlayingState.player.gold >= item.Cost && buy) || !price))
        {
            Player.inventory.addItemToInventory(item);
            GameEnvironment.soundManager.playSoundEffect("Pickup");
            pickedUp = true;
            if(price)
            {
                PlayingState.player.gold -= item.Cost;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Items/Altar"), position);
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
        if (hitbox.Intersects(PlayingState.player.BoundingBox) && price)
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Press spacebar to buy", position + new Vector2(-65, 20), Color.White);
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), Convert.ToString(item.Cost), position + new Vector2(25, -50), Color.Yellow);
        }
    }        
}

