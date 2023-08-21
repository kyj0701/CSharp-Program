using System.Collections.Generic;

namespace SpartaText
{
    internal class Program
    {
        private static Character player;
        private static List<Item> inventory;

        private static Item[] equipment;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            equipment = new Item[5];

            equipment[0] = new Item("무쇠값옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 1, 0, 5, 0, 500);
            equipment[1] = new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 0, 2, 0, 0, 700);

            // 캐릭터 인벤토리 세팅
            inventory = new List<Item>();
            inventory.Add(equipment[0]);
            inventory.Add(equipment[1]);

        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            int atkPlus = 0;
            int defPlus = 0;
            int hpPlus = 0;

            for (int i = 0; i < inventory.Count; i++) 
            {
                if (inventory[i].Equipped)
                {
                    atkPlus += inventory[i].Atk;
                    defPlus += inventory[i].Def;
                    hpPlus += inventory[i].Hp;
                }
            }

            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            if (atkPlus > 0) Console.WriteLine($"공격력 :{player.Atk} (+{atkPlus})");
            else Console.WriteLine($"공격력 :{player.Atk}");
            if (defPlus > 0) Console.WriteLine($"방어력 :{player.Def} (+{defPlus})");
            else Console.WriteLine($"방어력 :{player.Def}");
            if (hpPlus > 0) Console.WriteLine($"체력 :{player.Hp} (+{hpPlus})");
            else Console.WriteLine($"체력 :{player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Equipped)
                {
                    if (inventory[i].Atk > 0) Console.WriteLine($"- [E]{inventory[i].Name,-5} | 공격력 +{inventory[i].Atk, -2} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- [E]{inventory[i].Name,-5} | 방어력 +{inventory[i].Atk, -2} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"-[E]{inventory[i].Name,-5} |  체력 +{inventory[i].Atk, -3} | {inventory[i].Description}");
                }
                else
                {
                    if (inventory[i].Atk > 0) Console.WriteLine($"- {inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- {inventory[i].Name,-5} | 방어력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"- {inventory[i].Name,-5} |  체 력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayEquipment();
                    break;
            }
        }

        static void DisplayEquipment()
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Equipped)
                {
                    if (inventory[i].Atk > 0) Console.WriteLine($"- {i+1} [E]{inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-2} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- {i+1} [E]{inventory[i].Name,-5} | 방어력 +{inventory[i].Atk,-2} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"- {i+1} [E]{inventory[i].Name,-5} |  체력 +{inventory[i].Atk,-3} | {inventory[i].Description}");
                }
                else
                {
                    if (inventory[i].Atk > 0) Console.WriteLine($"- {i+1} {inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- {i+1} {inventory[i].Name,-5} | 방어력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"- {i+1} {inventory[i].Name,-5} |  체 력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, inventory.Count);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                default:
                    inventory[input - 1].Equipped = !inventory[input - 1].Equipped; // 실제 인벤토리 index는 0부터 시작이기 때문에 input에서 1 빼고 시작
                    DisplayEquipment();
                    break;
            }
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Category { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Price { get; }
        public bool Equipped { get; set; }

        public Item(string name, string description ,int category, int atk, int def, int hp, int price)
        {
            Name = name;
            Description = description;
            Category = category;
            Atk = atk;
            Def = def;
            Hp = hp;
            Price = price;
            Equipped = false;
        }
    }
}