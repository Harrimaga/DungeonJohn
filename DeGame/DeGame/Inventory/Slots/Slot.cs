using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Slot : SpriteGameObject
{
    public Texture2D itemSprite;
    public Item item;
    public bool text = false;

    public Slot(string name, int layer = 0, string id = "slot") : base(name, layer, id)
    {

    }

    public override void Update(GameTime gameTime)
    {
        
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(text)
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), item.itemName, position, Color.White);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains(inputHelper.MousePosition))
        {
            switch (id)
            {
                case "ArmourSlot":
                    Player.inventory.currentArmour.unequip();
                    Player.inventory.currentArmour = null;
                    break;
                case "BootsSlot":
                    Player.inventory.currentBoots.unequip();
                    Player.inventory.currentBoots = null;
                    break;
                case "HelmetSlot":
                    Player.inventory.currentHelmet.unequip();
                    Player.inventory.currentHelmet = null;
                    break;
                case "PassiveSlot":
                    PassiveSlot slot = (PassiveSlot)this;
                    slot.item.unequip();
                    slot.item = null;
                    break;
                case "ShieldSlot":
                    Player.inventory.currentShield.unequip();
                    Player.inventory.currentShield = null;
                    break;
                case "WeaponSlot":
                    Player.inventory.currentWeapon.unequip();
                    Player.inventory.currentWeapon = null;
                    break;
            }
        }
        if (BoundingBox.Contains(inputHelper.MousePosition))
        {
            text = true;
        }
        else
        {
            text = false;
        }
    }
}
