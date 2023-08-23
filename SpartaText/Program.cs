using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SpartaText
{
    internal class Program
    {
        private static string m_filePath = @"C:\Users\yejun\Desktop\sparta\C#\Program\SpartaText\save\";

        private static Character player;
        private static List<Item> inventory;
        private static List<Item> shop;

        private static Item[] items;

        

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
            items = new Item[5];

            items[0] = new Item("무쇠값옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 1, 0, 5, 0, 200);
            items[1] = new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 0, 2, 0, 0, 300);
            items[2] = new Item("판금값옷", "판금으로 만들어져 단단하고 무거운 갑옷입니다.", 1, 0, 6, 0, 400);
            items[3] = new Item("철검", "철로 만들어진 검입니다.", 0, 3, 0, 0, 500);
        

            // 상점 정보 세팅
            shop = new List<Item>();
            for (int i = 0; i < 4; i++)
            {
                shop.Add(items[i]);
            }

            // 캐릭터 인벤토리 세팅
            inventory = new List<Item>();
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 저장하기 & 불러오기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;

                case 3:
                    DisplayShop();
                    break;

                case 4:
                    DisplaySaveLoad();
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
                    if (inventory[i].Atk > 0) Console.WriteLine($"- [E]{inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-2} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- [E]{inventory[i].Name,-5} | 방어력 +{inventory[i].Def,-2} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"-[E]{inventory[i].Name,-5} |  체력 +{inventory[i].Hp,-3} | {inventory[i].Description}");
                }
                else
                {
                    if (inventory[i].Atk > 0) Console.WriteLine($"- {inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- {inventory[i].Name,-5} | 방어력 +{inventory[i].Def,-5} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"- {inventory[i].Name,-5} |  체 력 +{inventory[i].Hp,-5} | {inventory[i].Description}");
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
                    Displayitems();
                    break;
            }
        }

        static void Displayitems()
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
                    if (inventory[i].Atk > 0) Console.WriteLine($"- {i + 1} [E]{inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-2} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- {i + 1} [E]{inventory[i].Name,-5} | 방어력 +{inventory[i].Def,-2} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"- {i + 1} [E]{inventory[i].Name,-5} |  체력 +{inventory[i].Hp,-3} | {inventory[i].Description}");
                }
                else
                {
                    if (inventory[i].Atk > 0) Console.WriteLine($"- {i + 1} {inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- {i + 1} {inventory[i].Name,-5} | 방어력 +{inventory[i].Def,-5} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"- {i + 1} {inventory[i].Name,-5} |  체 력 +{inventory[i].Hp,-5} | {inventory[i].Description}");
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
                    Displayitems();
                    break;
            }
        }

        static void DisplayShop()
        {
            Console.Clear();

            Console.WriteLine("상점");
            Console.WriteLine("아이템을 구매하거나 판매할 수 있습니다.");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 구매");
            Console.WriteLine("2. 판매");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayShopPurchasing();
                    break;
                case 2:
                    DisplayShopSale();
                    break;
            }
        }

        static void DisplayShopPurchasing()
        {
            Console.Clear();

            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("아이템을 구매할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[상점 아이템 목록]");
            for (int i = 0; i < shop.Count; i++)
            {
                    if (shop[i].Atk > 0) Console.WriteLine($"- {i + 1} {shop[i].Name,-5} | 공격력 +{shop[i].Atk,-5} | {shop[i].Description}");
                    else if (shop[i].Def > 0) Console.WriteLine($"- {i + 1} {shop[i].Name,-5} | 방어력 +{shop[i].Def,-5} | {shop[i].Description}");
                    else if (shop[i].Hp > 0) Console.WriteLine($"- {i + 1} {shop[i].Name,-5} |  체 력 +{shop[i].Hp,-5} | {shop[i].Description}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine($"소지 Gold : {player.Gold}");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, shop.Count);
            switch (input)
            {
                case 0:
                    DisplayShop();
                    break;
                default:
                    if (player.Gold >= shop[input - 1].Price)
                    {
                        inventory.Add(shop[input - 1]);
                        player.Gold -= shop[input - 1].Price;
                        Console.WriteLine($"{shop[input - 1].Name}을(를) 구매하였습니다.");
                        Thread.Sleep(1000);
                        DisplayShopPurchasing();
                    }
                    else
                    {
                        Console.WriteLine($"{shop[input - 1].Price - player.Gold}Gold 부족합니다.");
                        Thread.Sleep(1000);
                        DisplayShopPurchasing();
                    }
                    break;
            }
        }

        static void DisplayShopSale()
        {
            Console.Clear();

            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("아이템을 판매할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Equipped)
                {
                    if (inventory[i].Atk > 0) Console.WriteLine($"- {i + 1} [E]{inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-2} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- {i + 1} [E]{inventory[i].Name,-5} | 방어력 +{inventory[i].Def,-2} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"- {i + 1} [E]{inventory[i].Name,-5} |  체력 +{inventory[i].Hp,-3} | {inventory[i].Description}");
                }
                else
                {
                    if (inventory[i].Atk > 0) Console.WriteLine($"- {i + 1} {inventory[i].Name,-5} | 공격력 +{inventory[i].Atk,-5} | {inventory[i].Description}");
                    else if (inventory[i].Def > 0) Console.WriteLine($"- {i + 1} {inventory[i].Name,-5} | 방어력 +{inventory[i].Def,-5} | {inventory[i].Description}");
                    else if (inventory[i].Hp > 0) Console.WriteLine($"- {i + 1} {inventory[i].Name,-5} |  체 력 +{inventory[i].Hp,-5} | {inventory[i].Description}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine($"소지 Gold : {player.Gold}");
            Console.WriteLine("원하시는 행동을 입력해주세요.");


            int input = CheckValidInput(0, inventory.Count);
            switch (input)
            {
                case 0:
                    DisplayShop();
                    break;
                default:
                    if (!inventory[input - 1].Equipped)
                    {
                        inventory.RemoveAt(input - 1);
                        player.Gold += inventory[input - 1].Price / 2;
                        Console.WriteLine($"{inventory[input - 1].Name}을(를) 판매하여 {inventory[input - 1].Price / 2}Gold를 얻었습니다.");
                        Thread.Sleep(1000);
                        DisplayShopSale();
                    }
                    else
                    {
                        Console.WriteLine("장착중인 장비는 판매할 수 없습니다.");
                        Thread.Sleep(1000);
                        DisplayShopSale(); 
                    }
                    break;
            }
        }

        static void DisplaySaveLoad()
        {
            Console.Clear();

            Console.WriteLine("저장하기 & 불러오기");
            Console.WriteLine("현재 게임의 정보를 저장하거나 이전 게임의 정보를 불러옵니다.");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");


            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    CharacterSave(m_filePath + "PlayerData.dat", player);
                    InventorySave(m_filePath +"InventoryData.dat", inventory);
                    DisplaySaveLoad();
                    break;
                case 2:
                    player = CharacterLoad(m_filePath + "PlayerData.dat");
                    inventory = InventoryLoad(m_filePath +"InventoryData.dat");
                    DisplaySaveLoad();
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

        static void CharacterSave(string filePath, Character data)
        {
            try
            {
                using (Stream ws = new FileStream(filePath + "InventoryData.dat", FileMode.Create))
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(ws, data);
                }
                Console.WriteLine("저장되었습니다.");
            }
            catch (Exception e)
            {
                Console.WriteLine("저장이 실패하였습니다.");
                Console.WriteLine(e.ToString());
            }
        }
        static void InventorySave(string filePath, List<Item> data)
        {
            try
            {
                using (Stream ws = new FileStream(filePath + "InventoryData.dat", FileMode.Create))
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(ws, data);
                }
                Console.WriteLine("저장되었습니다.");
            }
            catch (Exception e)
            {
                Console.WriteLine("저장이 실패하였습니다.");
                Console.WriteLine(e.ToString());
            }
        }

        static Character CharacterLoad(string filePath)
        {
            Character loadData = null;

            using (Stream rs = new FileStream(filePath, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                loadData = (Character)binaryFormatter.Deserialize(rs);
            }

            return loadData;
        }
        static List<Item> InventoryLoad(string filePath)
        {
            List<Item> loadData = null;

            using (Stream rs = new FileStream(filePath, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                loadData = (List<Item>)binaryFormatter.Deserialize(rs);
            }

            return loadData;
        }

        
    }

    [Serializable]
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; set; }

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

    [Serializable]
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