using System.Collections.Generic;

class Recipe
{
    public List<Item> list1, list2, listNewItem;

    public Recipe()
    {
        list1 = new List<Item>();
        list2 = new List<Item>();
        listNewItem = new List<Item>();
        MakeList();
    }

    void MakeList()
    {
        list1.Add(new Mac10());
        list2.Add(new Mac10());
        listNewItem.Add(new BigMac());

        list1.Add(new StandardBow());
        list2.Add(new Mac10());
        listNewItem.Add(new DoubleGun());

        list1.Add(new HardHelmet());
        list2.Add(new HardHelmet());
        listNewItem.Add(new PowerHelmet());

        list1.Add(new BloodRing());
        list2.Add(new BloodRing());
        listNewItem.Add(new SimpleArmour());

        list1.Add(new PowerHelmet());
        list2.Add(new MageJacket());
        listNewItem.Add(new HelicopterHat());

        list1.Add(new CrestShield());
        list2.Add(new CrestShield());
        listNewItem.Add(new Mirror());
    }
}

