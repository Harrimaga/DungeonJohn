using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class CraftingSlots : GameObjectList
{
    public CraftingSlot itemSlot1, itemSlot2;
    new Vector2 position;
    protected bool clicked1 = false, clicked2 = false ;

    public CraftingSlots(Vector2 position) : base()
    {
        this.position = position;
        itemSlot1 = new CraftingSlot(position, new HardHelmet());
        itemSlot2 = new CraftingSlot(position + new Vector2(212,0),null);

        Add(itemSlot1);
        Add(itemSlot2);
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
            itemSlot2.position = position + new Vector2(212, 0);
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

    public override void HandleInput(InputHelper inputHelper)
    {
        // TODO: Implement
        // Kijk of er twee items in zitten
        // Als er 2 in zitten en je klikt op de +, kijk of ze samen een ander item vormen
        // Als dit zo is, verwijder de items in de slots (gewoon het item weghalen en niet in Player.inventory toevoegen)
        //      en voeg het gecrafte item toe aan Player.inventory

        // Deze handle input moet alleen kijken naar de + (of whatever je ervan maakt), dus niet naar de twee slots.
        //      De craftingslots hebben zelf hun HandleInput (in CraftingSlot.cs), dus houdt hier rekening mee dat je de goede boundingbox gebruikt! :p

        children[0].HandleInput(inputHelper);
        children[1].HandleInput(inputHelper);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (CraftingSlot slot in children)
        {
            slot.Draw(gameTime, spriteBatch);
        }
        // TODO: draw functie implementeren.
        // Hier wss de twee slots tekenen (met evt een + ertussen ofzo, verzin wat)
    }

    public override void Update(GameTime gameTime)
    {
        // TODO: imlement
        // Weet niet of hier veel mee gedaan moet worden, misschien alleen de positie updaten ofzo? 
        //      Hoeft niet als je in Crafting state elke keer in de draw of update een nieuwe instance maakt hiervan
    }
}
