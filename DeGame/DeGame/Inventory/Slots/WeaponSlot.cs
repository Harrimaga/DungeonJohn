using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeaponSlot : SpriteGameObject
{
    Texture2D sprite;
    public Vector2 position;

    public WeaponSlot(Vector2 position, int layer = 0, string id = "WeaponSlot") : base ("Sprites/InventorySlots/WeaponSlot", layer, id)
    {
        this.position = position;
        sprite = GameEnvironment.assetManager.GetSprite("Sprites/InventorySlots/WeaponSlot");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }
}

