using TextAdventureGame.Objects.InventorySystem;

namespace TextAdventureGame.Objects.Character
{
    public class Player : CharacterBase
    {
        public Player(CharacterType type, string name, int maxHealth, int attackPoints, int defensePoints) : base(type, name, maxHealth, attackPoints, defensePoints)
        {
            _inventory = new Inventory();
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
