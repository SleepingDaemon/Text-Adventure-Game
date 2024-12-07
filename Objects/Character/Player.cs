using TextAdventureGame.Objects.BattleSystem;
using TextAdventureGame.Objects.InventorySystem;
using TextAdventureGame.Objects.RoomSystem;
using TextAdventureGame.Objects.UI;

namespace TextAdventureGame.Objects.Character
{
    public class Player : CharacterBase
    {
        public int xPos { get; set; }
        public int yPos { get; set; }
        public HashSet<string> CompletedEvents { get; set; } = [];

        public Player(CharacterType type, string name, int maxHealth, int attackPoints, int defensePoints) : base(type, name, maxHealth, attackPoints, defensePoints)
        {
            _inventory = new Inventory();
        }

        public void MoveTo(string direction, Node[,] map)
        {
            Node currentNode = map[xPos, yPos];
            currentNode.IsDiscovered = true;

            if (currentNode.Connections.ContainsKey(direction))
            {
                if (direction == "up") xPos--;
                else if (direction == "down") xPos++;
                else if(direction == "left") yPos--;
                else if(direction == "right") yPos++;

                // Reveal the new node
                Node newNode = map[xPos, yPos];
                newNode.IsDiscovered = true;

                Console.Clear();
                Console.WriteLine($"\n You moved {direction}");
                Console.Write(newNode.Description);
                Console.ReadLine();

                if(newNode.HasEnemy && !newNode.IsCleared)
                {
                    List<CharacterBase> enemies = new();
                    CharacterBase enemy1 = new Enemy(CharacterType.Enemy, "Warbot", 25, 8, 3);
                    CharacterBase enemy2 = new Enemy(CharacterType.Enemy, "Warbot", 25, 8, 3);
                    enemies.Add(enemy1);
                    enemies.Add(enemy2);

                    BattleManager.StartBattle(this, enemies, newNode);
                }

                MapUI.DisplayMap(map, xPos, yPos);
            }
            else
                Console.WriteLine(" You can't move in that direction!");
        }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);

            if (_health < _maxHealth * 0.5)
            {
                GameUI.UpdatePortrait(this, "hurt");
                if(BattleManager.battleEnemies != null)
                    GameUI.DisplayBattleFrames(this, BattleManager.battleEnemies);
                else
                    Console.WriteLine("Battle enemies collection is null");
            }
        }

        public void CheckInventory(List<Item> inventory)
        {
            Dictionary<int, Item> items = new Dictionary<int, Item>();

            Console.WriteLine("\n INVENTORY:");
            Console.WriteLine($" Choose an item below. Press [0] to go back.");
            Console.WriteLine(new string('-', 30));

            if(inventory.Count == 0)
                Console.WriteLine(" Your inventory is empty.");
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    items.Add(i + 1, inventory[i]);
                    Console.WriteLine($" [{i + 1}] {inventory[i]} - {inventory[i].Description}");
                }
            }

            Console.WriteLine($"{'-', 30}");

            Console.Write("\n Choose item Number to use or equip: ");
            string? input = Console.ReadLine();

            if(int.TryParse(input, out int inputNumber) && inputNumber > 0 && inputNumber <= items.Count)
            {
                if(items.TryGetValue(inputNumber, out Item selectedItem))
                {
                    Console.WriteLine($"\n You selected: {selectedItem.Name}");
                    if(selectedItem.Type == ItemType.Weapon || selectedItem.Type == ItemType.Armor)
                    {
                        Equip(selectedItem);
                    }
                    else if(selectedItem.Type == ItemType.Consumable)
                    {
                        Use(selectedItem);
                    }
                    else
                    {
                        Console.WriteLine(" This item cannot be used.");
                    }
                }
            }
            else if(inputNumber == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine(" Invalid input. Please try again.");
            }
        }
    }
}
