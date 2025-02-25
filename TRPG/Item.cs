﻿using System;


namespace TRPG
{
    internal class Item
    {
        public int Id { get; set; }  // 아이템을 판매 구매 방식에 있어 index오류로 인해
                                     // 고유한 ID를 부여하여 판매하는 방식
        public string Name {  get; set; }
        public int Armor {  get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public float Price { get; set; }
        public float SellPrice { get; set; } 
        public string ItemInfo {  get; set; }
        public bool isPurchased {  get; set; }
        public bool isEquipped { get; set; }

        public Item(string name, int attack, int armor, int health, string itemInfo, float price) 
        {
            Name = name;
            Armor = armor; 
            Attack = attack;
            Health = health;
            Price = price;
            ItemInfo =  itemInfo;
            SellPrice = price * 0.85f;
        }
       
    }
}
