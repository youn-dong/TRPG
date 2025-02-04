using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    internal class Characters
    {
        public enum jobType { 전사, 궁수, 마법사 }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public float Level { get; set; }
        public int Armor { get; set; }
        public int Gold { get; set; }
        bool IsDead => Health <= 0;
        public jobType Job { get; set; }
        public int maxHealth { get; set; }
        
        
        public Characters(string name, jobType job)
        {
            Name = name;
            Job = job;
            Level = 1;
            Health = 100;  //아이템 장착까지 고려한 체력을 0으로 초기화
            Attack = 20;  //아이템 장착까지 고려한 공격력을 0으로 초기화
            Gold = 1500;
            Armor = 5;   //아이템 장착까지 고려한 방어력을 0으로 초기화
            
        }
        public static Characters CreateCharacters(string name, jobType job) //enum으로 받은 직업을 통해서
                                                               //캐릭터 생성시 매개변수로 이름과 직업을 선택하도록
        {
            return new Characters(name, job);
        }
    }
}
