﻿using System;
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
        private Characters character;
        public DungeonType type { get; set; }
        public float RecommendedArmor { get; set; }
        public float ClearReward { get; set; }


        public Dungeon(DungeonType dungeonType, float recommendedArmor, Characters characters, float clearReward)
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
        public void AttemptDungeon()
        {
           
           Random random = new Random();  
            if(character.Armor<RecommendedArmor)  // 요구되는 방어력보다 낮을 때
                if(random.Next(0,101) < 41)  //40퍼센트로 맞추기 위해서 random을 1~100%이므로 맞추고
                                             //그중 40퍼센트로 맞추기 위함.
                {
                    Console.WriteLine("던전 클리어 실패ㅠㅠ 체력이 절반으로 감소합니다.");
                    character.Health -= (character.maxHealth / 2); 
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
            Console.Clear();
            Console.WriteLine("던전 클리어");
            Random random = new Random();  //클리어시 감소되는 체력 계산하기 20~35 기본 감소 랜덤값
            int ArmorDifference = (int)(character.Armor - RecommendedArmor); // 유저방어력에 따른 체력감소값 정하기
            int playerMinDamage = 20 + ArmorDifference;
            int playerMaxDamage = 35 + ArmorDifference;

            int damage = random.Next(playerMinDamage, playerMaxDamage); //플레이어 최소데미지,
                                                                        //플레이어 최대데미지까지 랜덤으로 결정
            character.Health -= damage;

            Console.WriteLine("축하드립니다.");
            Console.WriteLine($"{type}던전을 클리어하셨습니다.");
            Console.WriteLine("\n[탐험 결과]");
            Console.Write($"체력 {character.Health} -> ");
            character.Health -= damage;
            Console.WriteLine($" {character.Health} ");
            int clearRewardPercentage = random.Next(1,101);
            int additionalReward = (int)(ClearReward * (character.Attack * 2 / 100));
            int totalReward = (int)ClearReward + additionalReward;
            Console.Write($"Gold {character.Gold} G ->");
            character.Gold += totalReward;
            Console.WriteLine($" {character.Gold} G");
        }
    }
}