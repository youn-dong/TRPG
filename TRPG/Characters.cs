using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    internal class Characters
    {
        public enum jobType {전사, 궁수, 마법사}
        public string Name { get; set; } 
        public float Health { get; set; }
        public float Attack { get; set; }
        public float Level { get; set; }
        public float Armor { get; set; }
        public float Gold { get; set; }
        bool IsDead => Health <= 0;
        public jobType Job {  get; set; }
        public void TakeDamage(float Attack)
        {
            Health -= Attack;
            if (IsDead)
            {
                Console.WriteLine($"{Name}이(가) 죽었습니다.");
            }
            else
            {
                Console.WriteLine($"{Name}이(가) {Attack}만큼 피해를 입었습니다.");
            }
        }
        public Characters(string name, jobType job)
        {

            Name = name;
            Job = job;
            Level = 1;
            Health = 100;
            Attack = 20;
            Gold = 1500;
            Armor = 5;
        }
        public static Characters CreateCharacter(string name, jobType job)
        {
            return new Characters(name, job);  // 직업을 enum으로 받기}
        }
    }
}
