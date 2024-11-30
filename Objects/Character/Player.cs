using TextAdventureGame.Objects.InventorySystem;

namespace TextAdventureGame.Objects.Character
{
    public class Player : CharacterBase
    {
        public Player(CharacterType type, string name, int maxHealth, int attackPoints, int defensePoints) : base(type, name, maxHealth, attackPoints, defensePoints) { }

        public void DisplayInventory(List<Item> inventory)
        {
            Console.WriteLine("\nINVENTORY");
            Console.WriteLine(new string('-', 30));

            if(inventory.Count == 0)
                Console.WriteLine("Your inventory is empty.");
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {inventory[i]} - {inventory[i].Description}");
                }
            }

            Console.WriteLine($"{'-', 30}");

            Console.Write("\nChoose item Number to use or equip: ");
            string input = Console.ReadLine();
            int inputNum = int.Parse(input);

            Console.ReadLine();
        }
    }
}
