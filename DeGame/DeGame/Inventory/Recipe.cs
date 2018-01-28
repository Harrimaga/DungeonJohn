using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    }
}

