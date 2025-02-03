using System.Threading;

namespace TRPG
{
    internal class Program
    {
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
                    character = new Characters("전사", Characters.jobType.전사); // 전사
                    break;
                case 2:
                    character = new Characters("궁수", Characters.jobType.궁수); // 궁수
                    break;
                case 3:
                    character = new Characters("마법사", Characters.jobType.마법사); // 마법사
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    return;
            }


        }
        public void SelectingBehaviour(Characters characters)
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
            switch (actions)
            {
                case 1:
                    Console.WriteLine("1. 상태 보기로 이동합니다.");
                    // PlayerInfomation(characters);
                    break;
                case 2:
                    Console.WriteLine("2. 인벤토리창으로 이동합니다.");
                    //인벤토리 함수
                    break;
                case 3:
                    Console.WriteLine("3. 상점으로 이동합니다. ");
                    // GoToShop(characters);
                    break;
                default:
                    Console.WriteLine("잘못 입력하셨습니다. 다시 입력해주세요.");
                    SelectingBehaviour(characters);
                    return;
            }
        }
        public void PlayerInfomation(Characters characters)
        {
            Console.Clear();
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {characters. Level}");
            Console.WriteLine($"이름: {characters.Name} ({characters.jobtype})");
            Console.WriteLine($"공격력: {characters.Attack}");
            Console.WriteLine($"방어력: {characters.Armor}");
            Console.WriteLine($"체력: {characters.Health}");
            Console.WriteLine($"소지금: {characters.Gold} \n");
            Console.WriteLine("0. 나가기 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요. ");
            Console.Write(">>>>> ");
            int actions = int.Parse(Console.ReadLine());
            if (actions == 0)
            {
                SelectingBehaviour(characters);
            }
            else
            {
                Console.WriteLine("숫자를 잘못 입력하셨습니다. 제대로 된 숫자를 입력해주세요.");
                PlayerInfomation(characters);
                return;
            }
        }

        
    }
}
