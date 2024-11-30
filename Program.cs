using TextAdventureGame.Objects.BattleSystem;
using TextAdventureGame.Objects.Character;
using TextAdventureGame.Objects.InventorySystem;

internal class Program
{
    private static void Main(string[] args)
    {
        CharacterBase player = new Player(CharacterType.Player, "Hero", 50, 8, 2);
        List<CharacterBase> enemies = new();
        CharacterBase enemy1 = new Enemy(CharacterType.Enemy, "Warbot", 25, 8, 3);
        CharacterBase enemy2 = new Enemy(CharacterType.Enemy, "Warbot", 25, 8, 3);
        enemies.Add(enemy1);
        enemies.Add(enemy2);

        Item naniteInjector = new(ItemType.Consumable, "Nanite Injector", 20, "Restores 20 HP");
        Item psiGlove = new(ItemType.Weapon, "Psi Glove", 3, "A Psionic Weapon with electricity output");
        Item aggressivePsiChip = new(ItemType.Implant, "Aggressive Psi Chip", 4, "An implant that enhances AP +4");
        player.Inventory.Add(naniteInjector);
        player.Inventory.Add(naniteInjector);

        foreach (CharacterBase enemy in enemies)
        {
            enemy.Inventory.Add(naniteInjector);
        }

        BattleManager.StartBattle(player, enemies);

        Console.ReadLine();
    }
}