using TextAdventureGame.Objects.Character;
using TextAdventureGame.Objects.InventorySystem;

internal class Program
{
    private static void Main(string[] args)
    {
        Character player = new Player("Hero", 50, 4, 2);
        Character enemy1 = new EnemyBase("Warbot", 25, 4, 3);

        Item naniteInjector = new(ItemType.Consumable, "Nanite Injector", 20);
        Item psiGlove = new(ItemType.Weapon, "Psi Glove", 3);
        Item aggressivePsiChip = new(ItemType.Implant, "Aggressive Psi Chip", 4);
        player.GetInventory().Add(naniteInjector);
        player.GetInventory().Add(naniteInjector);

        Console.WriteLine("Player Health: " + player.GetHealth().ToString() + "/" + player.GetMaxHealth().ToString());

        player.Attack(enemy1);
        enemy1.Attack(player);
        player.Attack(enemy1);

        Console.WriteLine("Enemy Health: " + enemy1.GetHealth().ToString() + "/" + enemy1.GetMaxHealth().ToString());
        Console.WriteLine("Player Health: " + player.GetHealth().ToString() + "/" + player.GetMaxHealth().ToString());
        Console.WriteLine("Player Inventory: " + player.GetInventory()[0].Name);

        player.UseItem(naniteInjector);

        Console.WriteLine("Player Health: " + player.GetHealth().ToString() + "/" + player.GetMaxHealth().ToString());

        if(player.GetInventory().Contains(naniteInjector))
            Console.WriteLine("Player Inventory: " + player.GetInventory()[0].Name);

        Console.WriteLine("Player Attack: " + player.GetAttackPoints());
        player.EquipItem(psiGlove);
        Console.WriteLine("Player Attack: " + player.GetAttackPoints());

        Console.ReadLine();
    }
}