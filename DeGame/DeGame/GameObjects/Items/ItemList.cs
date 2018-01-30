﻿using System.Collections.Generic;

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

    /// <summary>
    /// Here items are added to a item list to make sure items spawn in the shop and/or in the rooms
    /// </summary>
    public void MakeItemList()
    {
        for(int x = 0; x < 2; x++)
        {
            RoomList.Add(new Mac10());
            RoomList.Add(new SimpleArmour());
            RoomList.Add(new CrestShield());
            RoomList.Add(new BloodRing());
            RoomList.Add(new HardHelmet());
            RoomList.Add(new CoolBoots());
            RoomList.Add(new SlimyBoots());
        }
        
        RoomList.Add(new DoubleGun());
        RoomList.Add(new Mirror());       
        RoomList.Add(new VialOfPoison());
        RoomList.Add(new MageJacket());
    }

    public void MakeShopList()
    {
        for (int x = 0; x < 2; x++)
        {
            ShopList.Add(new Mac10());
            ShopList.Add(new MageJacket());
            ShopList.Add(new SimpleArmour());
            ShopList.Add(new CrestShield());
            ShopList.Add(new BloodRing());
            ShopList.Add(new HardHelmet());
            ShopList.Add(new CoolBoots());
        }

        ShopList.Add(new DoubleGun());
        ShopList.Add(new SlimyBoots());
        ShopList.Add(new Mirror());
        ShopList.Add(new VialOfPoison());
        ShopList.Add(new PowerHelmet());
        ShopList.Add(new HelicopterHat());
    }

}
