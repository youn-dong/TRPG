using System;


namespace TRPG
{
    internal class Dungeon
    {    //던전 난이도 확인
         // 난이도에 맞는 방어력, 공격력으로 보상 계산
         // 난이도에 걸맞는 방어력에 따른 체력감소 로직 구현
         // 던젼난이도 클리어시 다음 던젼 해금 시스템 
        public enum DungeonType
        {
            Easy,
            Normal,
            Hard
        }
        private Item item;
        private Characters character;
        public DungeonType type { get; set; }
        public int RecommendedArmor { get; set; }
        public int ClearReward { get; set; }
        public int Damage { get; set; }
        public bool IsClear { get; set; } 

        public Dungeon(DungeonType dungeonType, int recommendedArmor, Characters characters, int clearReward)
        {
            type = dungeonType;
            RecommendedArmor = recommendedArmor;
            ClearReward = clearReward;
            character = characters; //캐릭터를 받아오면서
            switch (type)
            {
                case DungeonType.Easy:
                    clearReward = 1000;
                    break;
                case DungeonType.Normal:
                    clearReward = 1700;
                    break;
                case DungeonType.Hard:
                    clearReward = 2500;
                    break;
            }
        }
        public void AttemptDungeon(Program program)
        {
            if (character.Armor < RecommendedArmor) //캐릭터 방어력이 요구되는 방어력보다 낮을 때
            {
                Random random = new Random();  //클리어시 감소되는 체력 계산하기 20~35 기본 감소 랜덤값
                if (random.Next(0, 101) < 41)  //40퍼센트로 맞추기 위해서 random을 1~100%이므로 맞추고
                                               //그중 40퍼센트로 맞추기 위함.
                {
                    Console.WriteLine($"던전 클리어 실패ㅠㅠ 체력이 {Damage * 2}으로 감소합니다.");
                    character.Health -= Damage * 2;
                    if (character.maxHealth <= 0)
                    {
                        character.IsDie(program); //캐릭터가 데미지를 입을 때는 isDie함수를 호출하고
                        return;                   // 그 값을 반환하도록 
                    }
                }
                else //클리어시
                {
                    int ArmorDifference = (int)(character.Armor - RecommendedArmor); // 유저방어력에 따른 체력감소값 정하기
                    int playerMinDamage = 20 - ArmorDifference;
                    int playerMaxDamage = 35 - ArmorDifference;
                    Damage = random.Next(playerMinDamage, playerMaxDamage);
                    character.Health -= Damage;
                    if (character.maxHealth <= 0)
                    {
                        character.IsDie(program); 
                        return;
                    }
                    IsClear = true;
                    ClearDungeon();
                }
            }
            else  // 요구되는 방어력보다 높을 때
            {
                Random random = new Random(); 
                int ArmorDifference = (int)(character.Armor - RecommendedArmor);
                int playerMinDamage = 20 - ArmorDifference;
                int playerMaxDamage = 35 - ArmorDifference;
                if (ArmorDifference >= 20) //요구되는 방어력이 높기 때문에 체력을 증가시키는 현상이 발생하므로
                                           //캐릭터의 방어력이 높을 때는 요구되는 방어력의 차이를 20으로 고정
                {
                    ArmorDifference = 20;
                }
                Damage = random.Next(playerMinDamage, playerMaxDamage);
                character.Health -= Damage;
                if (character.maxHealth <= 0)
                { 
                    character.IsDie(program); 
                    return;
                }
                IsClear = true;
                ClearDungeon();
            }
        }
        public void ClearDungeon() //던전 클리어시 
        {
            Console.Clear();
            Console.WriteLine("던전 클리어");
            Console.WriteLine("축하드립니다.");
            Console.WriteLine($"{type}던전을 클리어하셨습니다.");
            Console.WriteLine("\n[탐험 결과]");
            Console.Write($"체력이 {Damage*2}만큼 감소하였습니다. ");
            Random random = new Random();
            int clearRewardPercentage = random.Next(1, 101);
            int additionalReward = (int)(ClearReward * (character.Attack * 2 / 100));
            int totalReward = (int)ClearReward + additionalReward;
            Console.Write($"Gold {character.Gold} G ->");
            character.Gold += totalReward;
            Console.WriteLine($" {character.Gold} G");
            character.ClearCount++; // 레벨업을 위한 클리어횟수 카운트 증가
            character.LevelUp(); //레벨업 함수 실행을 통해 클리어횟수에 따른 Level증가 
        }
    }
}