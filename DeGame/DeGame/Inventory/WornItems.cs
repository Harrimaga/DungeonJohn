using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Class that is used to draw the worn item slots
/// </summary>
public class WornItems : GameObjectList
{
    public WeaponSlot weaponSlot;
    public HelmetSlot helmetSlot;
    public ArmourSlot armourSlot;
    public BootsSlot bootsSlot;
    public ShieldSlot shieldShot;
    public PassiveSlot passiveSlot1, passiveSlot2;
    new Vector2 position;


    public WornItems(Vector2 position) : base()
    {
        this.position = position;
        weaponSlot = new WeaponSlot(position + new Vector2(0, 74));
        helmetSlot = new HelmetSlot(position + new Vector2(74, 0));
        armourSlot = new ArmourSlot(position + new Vector2(74, 74));
        bootsSlot = new BootsSlot(position + new Vector2(74, 212));
        shieldShot = new ShieldSlot(position + new Vector2(212, 74));
        passiveSlot1 = new PassiveSlot(position, 1, Player.inventory.currentPassives[0]);
        passiveSlot2 = new PassiveSlot(position + new Vector2(212, 0), 2, Player.inventory.currentPassives[1]);

        Add(weaponSlot);
        Add(helmetSlot);
        Add(armourSlot);
        Add(bootsSlot);
        Add(shieldShot);
        Add(passiveSlot1);
        Add(passiveSlot2);
    }

    public override void Update(GameTime gameTime)
    {
        passiveSlot1.item = Player.inventory.currentPassives[0];
        passiveSlot2.item = Player.inventory.currentPassives[1];
        base.Update(gameTime);
    }

    /// <summary>
    /// Gets the position of the block, and sets the individual slots their position, if the main position is set
    /// </summary>
    public override Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            weaponSlot.position = position + new Vector2(0, 74);
            helmetSlot.position = position + new Vector2(74, 0);
            armourSlot.position = position + new Vector2(74, 74);
            bootsSlot.position = position + new Vector2(74, 212);
            shieldShot.position = position + new Vector2(212, 74);
            passiveSlot1.position = position;
            passiveSlot2.position = position + new Vector2(212, 0);
        }
    }

    /// <summary>
    /// Handles the input for each individual slot
    /// </summary>
    /// <param name="inputHelper"></param>
    /// <param name="gameTime"></param>
    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        weaponSlot.HandleInput(inputHelper, gameTime);
        helmetSlot.HandleInput(inputHelper, gameTime);
        armourSlot.HandleInput(inputHelper, gameTime);
        bootsSlot.HandleInput(inputHelper, gameTime);
        shieldShot.HandleInput(inputHelper, gameTime);
        passiveSlot1.HandleInput(inputHelper, gameTime);
        passiveSlot2.HandleInput(inputHelper, gameTime);
    }
}

