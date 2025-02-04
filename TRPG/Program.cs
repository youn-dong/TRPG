using System;
using System.Reflection;

namespace TRPG
{
    internal class Program
    {
        public static List<Item> Items = new List<Item>(); //아이템은 List화해서 관리
        public void Description() //아이템설명서 
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
            game.Description(); //상점Main에서는 재귀함수를 통해서 출력하고 있기 때문에
                                //Description이 한번만 호출될 수 있는 구간을 만들어서 입력
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
        public void SelectJob(string name) //Character메서드를 통해서 switch문을 통해 
        {
            Console.WriteLine("원하는 직업을 선택하세요. \n");
            Console.WriteLine("1. 전사 (Warrior) ");
            Console.WriteLine("2. 궁수 (Archor) ");
            Console.WriteLine("3. 마법사 (Magician)\n ");
            Console.Write(">>>>> ");
            Characters characters = null;
            int jobChoice = int.Parse(Console.ReadLine());
            switch (jobChoice)
            {
                case 1:
                    characters = Characters.CreateCharacters(name, Characters.jobType.전사); // 전사
                    break;
                case 2:
                    characters = Characters.CreateCharacters(name, Characters.jobType.궁수); // 궁수
                    break;
                case 3:
                    characters = Characters.CreateCharacters(name, Characters.jobType.마법사); // 마법사
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    return;
            }
            SelectingBehaviour(characters);
        }
        public void SelectingBehaviour(Characters characters) //메인화면 구현
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다. \n");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점 ");
            Console.WriteLine("4. 휴식하기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요. ");
            Console.Write(">>>>> ");
            int actions = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (actions)
            {
                case 1:
                    PlayerInfomation(characters);
                    break;
                case 2:
                    GoToInventory(characters);
                    break;
                case 3:
                    GoToShop(characters);
                    break;
                case 4:
                    GoToRest(characters);
                    break;
                default:
                    Console.WriteLine("잘못 입력하셨습니다. 다시 입력해주세요.");
                    SelectingBehaviour(characters);
                    return;
            }
        }
        public void PlayerInfomation(Characters characters)
        {
            float totalAttack = 0; //아이템의 스텟을 상태보기에 업데이트 하기위해서 착용한 아이템의 전체 공격력
            float totalHealth = 0; //아이템의 스텟을 상태보기에 업데이트 하기위해서 착용한 아이템의 전체 체력
            float totalArmor = 0;  //아이템의 스텟을 상태보기에 업데이트 하기위해서 착용한 아이템의 전체 방어력
            foreach (var item in Items)
            {
                if (item.isEquipped)  //foreach문을 통해서 item리스트 중 착용된 장비만 각종 스텟에 포함되도록
                {
                    totalAttack += item.Attack;
                    totalHealth += item.Health;
                    totalArmor += item.Armor;
                }
            }
            Console.Clear();
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            if (characters != null)
            {
                Console.WriteLine($"Lv. {characters.Level}");
                Console.WriteLine($"이름: {characters.Name} ({characters.Job})");
                Console.WriteLine($"공격력: {characters.Attack} (+{totalAttack})");    //포함된 아이템의 스텟도 같이 명시
                Console.WriteLine($"방어력: {characters.Armor} (+{totalArmor})");
                Console.WriteLine($"체력: {characters.Health} (+{totalHealth})");
                Console.WriteLine($"소지금: {characters.Gold} \n");
            }
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
        public void GoToShop(Characters characters)
        {
            //상점이동시 시작화면 구현
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine(characters.Gold);

            Console.WriteLine("\n[아이템 목록]");

            int index = 1;
            foreach (var item in Items)
            {

                string priceShow = item.isPurchased ? "구매완료" : $"{item.Price} G";
                Console.WriteLine($"{index}.{item.Name} |  공격력 + {item.Attack}  |  방어력 + {item.Armor}  |  체력 + {item.Health}  |  {item.ItemInfo}  |  {priceShow}");
                index++;

            }
            Console.WriteLine("\n1.아이템 구매 ");
            Console.WriteLine("2.아이템 판매");
            Console.WriteLine("0.나가기 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요. ");
            Console.Write(">>>>> ");
            int actions = int.Parse(Console.ReadLine());
            if (actions == 0)
            {
                Console.Clear();
                SelectingBehaviour(characters); //다시 나가기 로직
            }
            else if (actions == 1)
            {
                Console.WriteLine("구매하고 싶은 장비를 선택해주세요.");
                int selectedNumber = int.Parse(Console.ReadLine()) - 1; //index는 0부터 시작하기 때문에
                if (selectedNumber >= 0 && selectedNumber < Items.Count)
                {
                    var selectedItem = Items[selectedNumber];
                    if (selectedItem.isPurchased)
                    {
                        Console.WriteLine("이미 구매하신 아이템입니다.");
                        Thread.Sleep(500);
                        GoToShop(characters);
                    }
                    else
                    {
                        if (characters.Gold > selectedItem.Price)
                        {
                            characters.Gold -= selectedItem.Price;
                            selectedItem.isPurchased = true;
                            Console.WriteLine($"{selectedItem.Name}을 구매하셨습니다.");
                            Thread.Sleep(500);
                            GoToShop(characters);

                        }
                        else
                        {
                            Console.WriteLine($"금액이 부족하여 {selectedItem.Name}을 구매하실 수 없습니다.");
                            Thread.Sleep(500);
                            GoToShop(characters);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 번호입니다.");
                    Thread.Sleep(1000);
                    GoToShop(characters); // 상점 재진입
                }
            }
            else if (actions == 2)
            {
                Console.Clear();
                Console.WriteLine("상점-아이템");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                index = 1;

                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    if (item.isPurchased) //보유중인 아이템을 확인, 목록에 출력
                    {

                        Console.WriteLine($"{index}.{item.Name} |  공격력 + {item.Attack}  |  방어력 + {item.Armor}  |  체력 + {item.Health}  |  {item.ItemInfo}  |  {item.Price}");
                        index++;
                    }
                }
                Console.WriteLine("0.나가기 \n");
                Console.WriteLine("판매를 원하시는 아이템을 입력하거나 0번을 눌러 나갈 수 있습니다.");
                Console.Write(">>>>> ");
                int selectedNumber = int.Parse(Console.ReadLine()) - 1; //index는 0부터 시작하기 때문에
                if (selectedNumber >= 0 && selectedNumber < Items.Count)
                {
                    var selectedItem = Items[selectedNumber];
                    if (selectedItem.isPurchased)
                    {
                        characters.Gold += selectedItem.SellPrice;
                        selectedItem.isPurchased = false;
                        selectedItem.isEquipped = false;
                        Items.RemoveAt(selectedNumber);  //판매하더라도 index가 계속 남아있었지만
                                                         //판매된 아이템의 index까지 제거하여 다시 index를 정렬해서 다시 판매창으로 
                        Console.WriteLine($"{selectedItem.Name}을 판매하여 {selectedItem.SellPrice} G만큼 획득했습니다.");
                        Thread.Sleep(1000);
                        GoToShop(characters);
                    }
                    else
                    {
                        Console.WriteLine("보유 중인 아이템이 아닙니다.");
                        Thread.Sleep(1000);
                        GoToShop(characters);
                    }
                }
            }
            else
            {
                Console.WriteLine("숫자를 잘못 입력하셨습니다. 제대로 된 숫자를 입력해주세요.");
                Thread.Sleep(1000);
                GoToShop(characters);
            }
        }
        public void GoToInventory(Characters characters)
        {
            bool isOwnedItem = false; //소유중인 아이템을 bool값을 통해 선언
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            foreach (var item in Items)
            {
                if (item.isPurchased)
                {
                    isOwnedItem = true;
                    string equippedMark = item.isEquipped ? "[E]" : "";
                    Console.WriteLine($"{equippedMark}{item.Name} |  공격력 + {item.Attack}  |  방어력 + {item.Armor}  |  체력 + {item.Health}  |  {item.ItemInfo}");
                }
            }
            if (!isOwnedItem)
            {
                Console.WriteLine("\n아이템을 보유하고 있지 않습니다.");
            }

            Console.WriteLine("1.장착 관리 ");
            Console.WriteLine("0.나가기 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요. ");
            Console.Write(">>>>> ");
            int actions = int.Parse(Console.ReadLine());
            if (actions == 0)
            {
                SelectingBehaviour(characters); //다시 나가기 로직
                return;
            }
            else if (actions == 1)
            {
                ManageEquipment(characters);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(500);
                GoToInventory(characters);
            }
        }
        public void ManageEquipment(Characters characters) //아이템 장착 구현하기
        {
            Console.Clear();
            Console.WriteLine("장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]");
            int index = 1;
            foreach (var item in Items)
            {
                if (item.isPurchased)  // 구매한 아이템만 출력
                {
                    string equippedMark = item.isEquipped ? "[E]" : "";  // 장착된 아이템은 [E] 표시
                    Console.WriteLine($"{index}. {equippedMark}{item.Name} | 공격력 + {item.Attack} | 방어력 + {item.Armor} | 체력 + {item.Health} | {item.ItemInfo}");
                    index++;
                }
            }
            Console.WriteLine("\n0.나가기.");
            Console.WriteLine("\n장착하고 싶은 아이템을 입력해주세요.\n");
            Console.WriteLine("원하시는 행동을 입력해주세요 ");
            Console.Write(">>>>> ");
            int selectedNumber = int.Parse(Console.ReadLine());
            if (selectedNumber == 0)
            {
                SelectingBehaviour(characters);
            }
            else if (selectedNumber > 0 && selectedNumber <= Items.Count)
            {
                var selectedItem = Items[selectedNumber - 1]; //index번호는 0부터 시작하므로
                                                              //내가 선택한 번호에서 -1을 하면 원하는 아이템과 index넘버 일치

                if (selectedItem.isPurchased) // 먼저 구매된 아이템만 장착할 수 있도록
                {
                    if (selectedItem.isEquipped) //장착 로직 구현
                    {
                        selectedItem.isEquipped = false;
                        Console.WriteLine($"{selectedItem.Name}장착을 해제하였습니다.");
                        characters.maxHealth -= selectedItem.Health;
                        Thread.Sleep(500);
                        GoToInventory(characters);
                    }
                    else
                    {
                        selectedItem.isEquipped = true;
                        Console.WriteLine($"{selectedItem.Name}을 장착하였습니다.");
                        characters.maxHealth += selectedItem.Health;
                        Thread.Sleep(500);
                        GoToInventory(characters);
                    }
                }
                else
                {
                    Console.WriteLine("구매하지 않은 아이템은 장착할 수 없습니다.");
                    Thread.Sleep(500);
                    GoToInventory(characters);
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                Thread.Sleep(500);
                ManageEquipment(characters);
            }
        }
        public void GoToRest(Characters characters) //휴식하기 기능 구현하기
        {
            int recoverGold = 500;  //휴식시 필요한 Gold양
            int recoverHealth = 100; //휴식시 회복되는 체력의 양
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"휴식을 선택하면 {recoverGold}G로 체력을 회복합니다");
            Console.WriteLine($"휴식시 체력은 {recoverHealth}이 회복됩니다.");
            Console.WriteLine($"현재 보유Gold : {characters.Gold} ");
            Console.WriteLine("\n1.휴식하기");
            Console.WriteLine("0.나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요 ");
            Console.Write(">>>>> ");
            int actions = int.Parse(Console.ReadLine());  //휴식시 할 수 있는 행동화
            if (actions == 0)
            {
                SelectingBehaviour(characters); //다시 나가기 로직
                return;
            }
            else if (actions == 1)  // 휴식버튼 클릭시 현재체력이 최대체력일 때 골드가 차감되지 않고 휴식되지 않기
                                    // 휴식버튼 클릭시 현재체력만큼 체력이 회복되고 골드가 차감되도록
                                    // 
            {
                if (characters.Health + recoverHealth >= characters.maxHealth) //최대체력을 만들기위한 변수 maxHealth가져오기
                {
                    Console.WriteLine("현재 체력이 최대체력이므로 휴식할 수 없습니다.");
                    Thread.Sleep(500);
                    GoToRest(characters);
                }
                if (characters.Gold < recoverGold)
                {
                    Console.WriteLine("Gold가 부족하여 휴식할 수 없습니다. Gold를 획득 후 다시 방문해주세요.");
                    Thread.Sleep(500);
                    GoToRest(characters);
                }
                if (characters.Gold >= recoverGold)
                {
                    characters.Gold -= 500;
                    if (characters.Health + recoverHealth > characters.maxHealth)
                    {
                        characters.Health = characters.maxHealth;
                        Console.WriteLine($"휴식하여 {characters.maxHealth}만큼 회복되었습니다.");
                        Thread.Sleep(500);
                        GoToRest(characters);
                    }
                    else
                    {
                        characters.Health = characters.Health + 100;
                        Console.WriteLine($"휴식하여 {recoverHealth}만큼 체력이 회복되었습니다.");
                        Thread.Sleep(500);
                        GoToRest(characters);
                    }
                }
            }
            else
            {
                Console.WriteLine("잘못된 숫자를 입력하셨습니다. 다시 입력해주세요.");
                Thread.Sleep(500);
                GoToRest(characters);
            }
        }
    }
}
