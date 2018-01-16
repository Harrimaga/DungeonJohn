using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class CraftingSlots : GameObjectList
{
    Button craftingB;
    Recipe recipe;
    public CraftingSlot itemSlot1, itemSlot2, itemNew;
    new Vector2 position;
    protected bool clicked1 = false, clicked2 = false ;

    public CraftingSlots(Vector2 position) : base()
    {
        this.position = position;
        recipe = new Recipe();
        itemSlot1 = new CraftingSlot(position, null,false);
        itemSlot2 = new CraftingSlot(position + new Vector2(500,0),null,false);
        itemNew = new CraftingSlot(position + new Vector2(245, 0), null,true);
        craftingB = new Button(position + new Vector2(-230, 50), "Craft", "CraftButton", "CraftButtonPressed", true, 1);
        Add(itemSlot1);
        Add(itemSlot2);
        Add(itemNew);
    }

    public override Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            itemSlot1.position = position;
            itemSlot2.position = position + new Vector2(500, 0);
            itemNew.position = position + new Vector2(245, 0);
        }
    }

    public CraftingSlot CheckForEmptySlot()
    {
        if (itemSlot1.item == null)
        {
            return itemSlot1;
        }
        if (itemSlot2.item == null)
        {
            return itemSlot2;
        }
        return null;
    }

    public void FillSlot(CraftingSlot slot, Item item)
    {
        slot.AddItem(item);
    }

    public void RecipeCheck()
    {
        if (itemSlot1.item != null && itemSlot2.item != null)
        {
            for (int i = 0; i < recipe.list1.Count; i++)
            {
                if (recipe.list1[i].itemName == itemSlot1.item.itemName && recipe.list2[i].itemName == itemSlot2.item.itemName)
                {
                    itemNew.AddItem(recipe.listNewItem[i]);
                }
                
                else if (recipe.list2[i].itemName == itemSlot1.item.itemName && recipe.list1[i].itemName == itemSlot2.item.itemName)
                {
                    itemNew.AddItem(recipe.listNewItem[i]);
                }
            }
        }
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        craftingB.HandleInput(inputHelper);
        children[0].HandleInput(inputHelper);
        children[1].HandleInput(inputHelper);
        children[2].HandleInput(inputHelper);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (CraftingSlot slot in children)
        {
            slot.Draw(gameTime, spriteBatch);
        }
        craftingB.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        craftingB.Update(gameTime);
        itemSlot1.Update(gameTime);
        itemSlot2.Update(gameTime);
        itemNew.Update(gameTime);
        itemNew.item = null;
        RecipeCheck();
        
        if (craftingB.Pressed && itemNew.item != null)
        {
            Player.inventory.addItemToInventory(itemNew.item);
            itemSlot1.item = null;
            itemSlot2.item = null;
            itemNew.item = null;
        }
    }
}
