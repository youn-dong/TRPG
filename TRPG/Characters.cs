using System;


namespace TRPG
{
    internal class Characters
    {
        private Program program; 
        private Item Item;
        public enum jobType { 전사, 궁수, 마법사 }
        public string Name { get; set; }
        public int Health { get; set; } 
        public int Attack { get; set; }
        public float Level { get; set; }
        public int Armor { get; set; }
        public int Gold { get; set; }
        bool IsDead { get; set; } = false;
        public jobType Job { get; set; }
        public int maxHealth { get; set; }
        public int ClearCount {  get; set; }
        public Characters(string name, jobType job)
        {
            Name = name;
            Job = job;
            Level = 1;
            Health = 100;  //아이템 장착까지 고려한 체력을 0으로 초기화
            
            Attack = 20;  //아이템 장착까지 고려한 공격력을 0으로 초기화
            Gold = 1500;
            Armor = 5;   //아이템 장착까지 고려한 방어력을 0으로 초기화
            ClearCount = 0; //클리어횟수를 0으로 만들고 클리어할 때마다
                            //Count가 늘어나도록 만들어서 Level을 증가시키도록

        }
        public void LevelUp() //레벨업을 시킬 수 있는 함수를 통해서 
        {
            if (ClearCount == 1 && Level == 1) //if문을 통해서 클리어횟수를 1번 할 경우 레벨업을 만들고
                                               //그에 상응하는 능력치 부여
            {
                Level++; 
                Attack += 1;
                Armor += 1;
                Console.WriteLine($"{Name}님이 레벨업! 현재 레벨 : {Level}");
            }
            else if (ClearCount == 3 && Level == 2) //나머지 반복을 통해 레벨업을 만들 수 있도록
            {
                Level++;
                Attack += 1;
                Armor += 1;
                Console.WriteLine($"{Name}님이 레벨업! 현재 레벨 : {Level}");
            }
            else if(ClearCount == 7 &&  Level == 3)
            {
                Level++;
                Attack += 1;
                Armor += 1;
                Console.WriteLine($"{Name}님이 레벨업! 현재 레벨 : {Level}");
            }
            else if( ClearCount == 12 &&  Level == 4)
            {
                Level++;
                Attack += 1;
                Armor += 1;
                Console.WriteLine($"{Name}님이 레벨업! 현재 레벨 : {Level}");
            }
        }
        public static Characters CreateCharacters(string name, jobType job) //enum으로 받은 직업을 통해서
                                                               //캐릭터 생성시 매개변수로 이름과 직업을 선택하도록
        {
            return new Characters(name, job);
        }
        public void IsDie(Program program) //Program 객체를 매개변수로 받음
        {
            maxHealth = 0;
            Console.WriteLine($"캐릭터의 체력이 {maxHealth}이 되어 죽었습니다. 게임을 다시 실행해주세요");
            Console.WriteLine($"글자 아무거나 누르면 게임이 재시작됩니다.");
            Console.ReadLine();
            program.StartGame();
        }
    }
}
