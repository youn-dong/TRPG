using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace TRPG
{
    internal class Program
    {
        public static List<Item> Items = new List<Item>();
        public void Description()
        {
            Items.Add(new Item("수련자갑옷", 0, 5, 50, "수련에 도움을 주는 갑옷입니다.", 200));
            Items.Add(new Item("무쇠갑옷", 0, 10, 100, "무쇠로 만들어져 튼튼한 갑옷입니다.", 400));
            Items.Add(new Item("스파르타의 갑옷", 0, 15, 200, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 800));
            Items.Add(new Item("낡은 검", 2, 0, 50, "쉽게 볼 수 있는 낡은 검입니다.", 300));
            Items.Add(new Item("청동 도끼", 5, 0, 100, "어디선가 사용됐던 거 같은 도끼입니다.", 500));
            Items.Add(new Item("스파르타의 창", 7, 0, 200, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 500));
            Items.Add(new Item("엑스칼리버", 20, 10, 200, "전설 속 왕의 검입니다.", 3000));
            Items.Add(new Item("파이어보우", 30, 10, 150, "불꽃을 발사하는 활입니다.", 3000));
            Items.Add(new Item("고목나무지팡이", 25, 10, 180, "고목나무로 만든 마법 지팡이입니다.", 3000));
        }
        static void Main(string[] args)
        {
            Program game = new Program(); // Program 인스턴스 생성
            game.StartGame();
           
        }
        void StartGame()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.\n");
            string name = Console.ReadLine();
            Console.WriteLine($"입력하신 이름은 {name}입니다.\n");
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>>>> ");
            while (true)
            {
                int inputNumber = int.Parse(Console.ReadLine());
                if (inputNumber == 1)
                {
                    Console.Clear();
                    Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                    Console.WriteLine("원하시는 직업을 선택해주세요.");
                    SelectJob(name);
                    break;
                }
                else if (inputNumber == 2)
                {
                    StartGame();
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 1 또는 2를 입력해주세요.");
                    continue;
                }
            }
        }
        public void SelectJob(string name)
        {
            Console.WriteLine("원하는 직업을 선택하세요. \n");
            Console.WriteLine("1. 전사 (Warrior) ");
            Console.WriteLine("2. 궁수 (Archor) ");
            Console.WriteLine("3. 마법사 (Magician)\n ");
            Console.Write(">>>>> ");
            Characters character = null;
            int jobChoice = int.Parse(Console.ReadLine());
            switch (jobChoice)
            {
                case 1:
                    character = Characters.CreateCharacter(name, Characters.jobType.전사); // 전사
                    break;
                case 2:
                    character = Characters.CreateCharacter(name, Characters.jobType.궁수); // 궁수
                    break;
                case 3:
                    character = Characters.CreateCharacter(name, Characters.jobType.마법사); // 마법사
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    return;
            }
            SelectingBehaviour(character);
        }
        public void SelectingBehaviour(Characters character)
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다. \n");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요. ");
            Console.Write(">>>>> ");
            int actions = int.Parse(Console.ReadLine());
            Console.Clear();
            Description();
            switch (actions)
            {
                case 1:
                    Console.WriteLine("1. 상태 보기로 이동합니다.");
                    PlayerInfomation(character);
                    break;
                case 2:
                    Console.WriteLine("2. 인벤토리창으로 이동합니다.");
                    //인벤토리 함수
                    break;
                case 3:
                    Console.WriteLine("3. 상점으로 이동합니다. ");
                    GoToShop(character);
                    break;
                default:
                    Console.WriteLine("잘못 입력하셨습니다. 다시 입력해주세요.");
                    SelectingBehaviour(character);
                    return;
            }
        }
        public void PlayerInfomation(Characters character)
        {
            
            Console.Clear();
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            if (character != null)
            {
                Console.WriteLine($"Lv. {character.Level}");
                Console.WriteLine($"이름: {character.Name} ({character.Job})");
                Console.WriteLine($"공격력: {character.Attack}");
                Console.WriteLine($"방어력: {character.Armor}");
                Console.WriteLine($"체력: {character.Health}");
                Console.WriteLine($"소지금: {character.Gold} \n");
            }
            else
            {
                Console.WriteLine("캐릭터 정보가 존재하지 않습니다.");
            }    
            Console.WriteLine("0. 나가기 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요. ");
            Console.Write(">>>>> ");
            int actions = int.Parse(Console.ReadLine());
            if (actions == 0)
            {
                SelectingBehaviour(character);
            }
            else
            {
                Console.WriteLine("숫자를 잘못 입력하셨습니다. 제대로 된 숫자를 입력해주세요.");
                PlayerInfomation(character);
                return;
            }
        }
        public void GoToShop(Characters character)
        {
            
            //상점이동시 시작화면 구현
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine(character.Gold);

            Console.WriteLine("\n[아이템 목록]"); 
            foreach (var item in Items)
            {
                Console.WriteLine($"-{item.Name} |  공격력 + {item.Attack}  |  방어력 + {item.Armor}  |  체력 + {item.Health}  |  {item.ItemInfo}  |  {item.Price} G");
            }
            Console.WriteLine("\n1.아이템 구매 ");
            Console.WriteLine("0.나가기 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요. ");
            Console.Write(">>>>> ");
            int actions = int.Parse(Console.ReadLine());
            if (actions == 0)
            {
                SelectingBehaviour(character);
            }
            else
            {
                Console.WriteLine("숫자를 잘못 입력하셨습니다. 제대로 된 숫자를 입력해주세요.");
                PlayerInfomation(character);
                return;
            }
        }

       

    }
}
