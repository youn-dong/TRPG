using System;
using TRPG;

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
        public DungeonType type { get; set; }
        public float RecommendedArmor { get; set; }
        public int PlayerArmor { get; set; } 

        public int PlayerAttack { get; set; }
        public int PlayerHealth { get; set; }
        public float ClearReward { get; set; }


        public Dungeon(DungeonType dungeonType, float recommendedArmor, Program game, float clearReward)
        {
            type = dungeonType;
            RecommendedArmor = recommendedArmor;
            PlayerArmor = game.totalArmor; //플레이어의 상태는 인스턴스를 통해서
                                           //프로그램의 멤버와 동일하도록 
            PlayerAttack = game.totalAttack;
            PlayerHealth = game.totalHealth;
            ClearReward = clearReward;

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
        public void AttemptDungeon()
        {
           
           Random random = new Random();  
            if(PlayerArmor<RecommendedArmor)  // 요구되는 방어력보다 낮을 때
                if(random.Next(0,101) < 41)  //40퍼센트로 맞추기 위해서 random을 1~100%이므로 맞추고
                                             //그중 40퍼센트로 맞추기 위함.
                {
                    Console.WriteLine("던전 클리어 실패ㅠㅠ 체력이 절반으로 감소합니다.");
                    PlayerHealth -= PlayerHealth / 2; 
                    return;
                    
                }
                else
                {
                    ClearDungeon();
                }
            else  // 요구되는 방어력보다 높을 때
            {
                ClearDungeon(); 
            }    
        }
        public void ClearDungeon()
        {
            Console.WriteLine("던전 클리어");
            Random random = new Random();  //클리어시 감소되는 체력 계산하기 20~35 기본 감소 랜덤값
            int ArmorDifference = (int)(PlayerArmor - RecommendedArmor); // 유저방어력에 따른 체력감소값 정하기
            int playerMinDamage = 20 - ArmorDifference;
            int playerMaxDamage = 35 + ArmorDifference;

            int damage = random.Next(playerMinDamage, playerMaxDamage); //플레이어 최소데미지,
                                                                        //플레이어 최대데미지까지 랜덤으로 결정
            PlayerHealth -= damage;

            Console.WriteLine($"{type}던전을 클리어하셨습니다. 축하드립니다. 추가 보상과 함께 {damage}만큼 체력이 감소됩니다.");

            int clearRewardPercentage = random.Next(1,101);
            int additionalReward = (int)(ClearReward * (PlayerAttack * 2 / 100));
            int totalReward = (int)ClearReward + additionalReward;
            Console.WriteLine($"던전 클리어 보상 : {totalReward}");
            
        }
    }

}