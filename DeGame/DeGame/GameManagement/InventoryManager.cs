using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InventoryManager
{
    public Item currentHelmet, currentArmour, currentBoots, currentWeapon, currentShield;

    public Item[] currentPassives;
    public List<Item> items;

    public InventoryManager()
    {
        currentHelmet = null;
        currentArmour = null;
        currentBoots = new Cool_Boots();
        currentWeapon = new StandardBow();
        currentShield = null;
        currentPassives = new Item[2];
        items = new List<Item>();
    }

    public void equip(Item item)
    {
        switch(item.Type)
        {
            case "helmet":
                if (currentHelmet != null)
                {
                    currentHelmet.unequip();
                    addItemToInventory(currentHelmet);
                }
                currentHelmet = item;
                removeItemFromInventory(item);
                currentHelmet.equip();
                break;
            case "armour":
                if (currentArmour != null)
                {
                    currentArmour.unequip();
                    addItemToInventory(currentArmour);
                }
                currentArmour = item;
                removeItemFromInventory(item);
                currentArmour.equip();
                break;
            case "boots":
                if (currentBoots != null)
                {
                    currentBoots.unequip();
                    addItemToInventory(currentBoots);
                }
                currentBoots = item;
                removeItemFromInventory(item);
                currentBoots.equip();
                break;
            case "weapon":
                if (currentWeapon != null)
                {
                    currentWeapon.unequip();
                    addItemToInventory(currentWeapon);
                }
                currentWeapon = item;
                removeItemFromInventory(item);
                currentWeapon.equip();
                PlayingState.player.CalculateAmmo();
                PlayingState.player.CalculateDamage();
                break;
            case "shield":
                if (currentShield != null)
                {
                    currentShield.unequip();
                    addItemToInventory(currentShield);
                }
                currentShield = item;
                removeItemFromInventory(item);
                currentShield.equip();
                break;
            case "passive":
                if (currentPassives.Count() == 2)
                {
                    currentPassives[1].unequip();
                    currentPassives[1] = currentPassives[0];
                    addItemToInventory(currentPassives[1]);
                }
                currentPassives[0] = item;
                removeItemFromInventory(item);
                currentPassives[0].equip();
                PlayingState.player.CalculateAmmo();
                PlayingState.player.CalculateDamage();
                break;
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