using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class ItemList
{
    public List<Item> RoomList, ShopList;
    public ItemList()
    {
        RoomList = new List<Item>();
        ShopList = new List<Item>();
        MakeItemList();
        MakeShopList();
    }

    public void MakeItemList()
    {
        RoomList.Add(new Mac10());
        RoomList.Add(new DoubleGun());
        RoomList.Add(new SlimyBoots());
        RoomList.Add(new CoolBoots());
        RoomList.Add(new MageJacket());
        RoomList.Add(new Mirror());
        RoomList.Add(new BloodRing());
        RoomList.Add(new HardHelmet());
        RoomList.Add(new PowerHelmet());
        RoomList.Add(new HelicopterHat());
        RoomList.Add(new VialOfPoison());
        RoomList.Add(new SimpleArmour());
    }

    public void MakeShopList()
    {
        ShopList.Add(new Mac10());
        ShopList.Add(new DoubleGun());
        ShopList.Add(new SlimyBoots());
        ShopList.Add(new CoolBoots());
        ShopList.Add(new MageJacket());
        ShopList.Add(new Mirror());
        ShopList.Add(new BloodRing());
        ShopList.Add(new HardHelmet());
        RoomList.Add(new PowerHelmet());
        RoomList.Add(new HelicopterHat());
        RoomList.Add(new VialOfPoison());
        RoomList.Add(new SimpleArmour());
    }

}
