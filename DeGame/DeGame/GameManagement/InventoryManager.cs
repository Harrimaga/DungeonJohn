using System.Collections.Generic;

public class InventoryManager
{
    public Item currentHelmet, currentArmour, currentBoots, currentWeapon, currentShield;

    public Item[] currentPassives;
    public List<Item> items;

    public InventoryManager()
    {
        currentWeapon = new StandardBow();
        currentPassives = new Item[2];
        items = new List<Item>();
    }

    /// <summary>
    /// Right here is the code to activate/equip a specific item
    /// </summary>
    public void equip(Item item)
    {
        switch (item.Type)
        {
            /// Right here a helmet is being equiped
            case "helmet":
                if (currentHelmet != null)
                {
                    /// Right here the current helmet is being removed and the new helmet is set to currentHelmet
                    currentHelmet.unequip();
                    items[items.IndexOf(item)] = currentHelmet;
                }
                /// Right here the new helmet is being equiped and removed from the inventory
                currentHelmet = item;
                currentHelmet.equip();
                removeItemFromInventory(item);
                break;
            /// Same for the armour
            case "armour":
                if (currentArmour != null)
                {
                    currentArmour.unequip();
                    items[items.IndexOf(item)] = currentArmour;
                }
                currentArmour = item;
                currentArmour.equip();
                removeItemFromInventory(item);
                break;
            /// Same for the boots
            case "boots":
                if (currentBoots != null)
                {
                    currentBoots.unequip();
                    items[items.IndexOf(item)] = currentBoots;
                }
                currentBoots = item;
                currentBoots.equip();
                removeItemFromInventory(item);
                break;
            /// Same for a weapon
            case "weapon":
                if (currentWeapon != null)
                {
                    currentWeapon.unequip();
                    items[items.IndexOf(item)] = currentWeapon;
                }
                currentWeapon = item;
                currentWeapon.equip();
                removeItemFromInventory(item);
                PlayingState.player.CalculateAmmo();
                PlayingState.player.CalculateDamage();
                break;
            /// Same for a shield
            case "shield":
                if (currentShield != null)
                {
                    currentShield.unequip();
                    items[items.IndexOf(item)] = currentShield;
                }
                currentShield = item;
                currentShield.equip();
                removeItemFromInventory(item);
                break;
            /// There is 2 passive slots, so it is the same code as above but twice
            case "passive":
                if (currentPassives[0] == null)
                {
                    currentPassives[0] = item;
                    currentPassives[0].equip();
                    removeItemFromInventory(item);
                }
                else if (currentPassives[1] == null)
                {
                    currentPassives[1] = item;
                    currentPassives[1].equip();
                    removeItemFromInventory(item);
                }
                else
                {
                    currentPassives[1].unequip();
                    items[items.IndexOf(item)] = currentPassives[1];
                    currentPassives[1] = currentPassives[0];
                    currentPassives[0] = item;
                    currentPassives[0].equip();
                }
                PlayingState.player.CalculateAmmo();
                PlayingState.player.CalculateDamage();
                break;
        }
    }

    /// <summary>
    /// Here the standard items are being equiped to start the game
    /// </summary>
    public void startUp()
    {
        if (currentArmour != null)
        {
            currentArmour.equip();
        }
        if (currentBoots != null)
        {
            currentBoots.equip();
        }
        if (currentHelmet != null)
        {
            currentHelmet.equip();
        }
        if (currentPassives[0] != null)
        {
            currentPassives[0].equip();
        }
        if (currentPassives[1] != null)
        {
            currentPassives[1].equip();
        }
        if (currentShield != null)
        {
            currentShield.equip();
        }
        if (currentWeapon != null)
        {
            currentWeapon.equip();
        }
    }

    public void addItemToInventory(Item item)
    {
        items.Add(item);
    }

    public void removeItemFromInventory(Item item)
    {
        items.Remove(item);
    }

    public override string ToString()
    {
        string res;

        res = "Equipped Items: ";
        if (currentHelmet != null)
        {
            res += "Helmet: " + currentHelmet.ToString();
        }
        if (currentArmour != null)
        {
            res += ", Armour: " + currentArmour.ToString();
        }
        if (currentBoots != null)
        {
            res += ", Boots: " + currentBoots.ToString();
        }
        if (currentWeapon != null)
        {
            res += ", Weapon: " + currentWeapon.ToString();
        }
        if (currentPassives[0] != null)
        {
            res += ", Passive1: " + currentPassives[0].ToString();
        }
        if (currentPassives[1] != null)
        {
            res += ", Passive2: " + currentPassives[1].ToString();
        }
        res += ". || Inventory: ";

        foreach (Item item in items)
        {
            res += item.ToString() + ", ";
        }

        return res;
    }
}