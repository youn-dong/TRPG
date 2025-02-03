using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    internal class Item
    {
        public string Name {  get; set; }
        public float Armor {  get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }
        public float Price { get; set; }
        public string ItemInfo {  get; set; }
        public Item(string name, float attack, float armor, float health, string itemInfo, float price) 
        {
            Name = name;
            Armor = armor; 
            Attack = attack;
            Health = health;
            Price = price;
            ItemInfo =  itemInfo;
        }
    }
}
