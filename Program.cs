using TextAdventureGame.Objects.BattleSystem;
using TextAdventureGame.Objects.Character;
using TextAdventureGame.Objects.InventorySystem;

internal class Program
{
    private static void Main(string[] args)
    {
        Item naniteInjector = new(ItemType.Consumable, "Nanite Injector", 20, "Restores 20 HP");
        Item psiGlove = new(ItemType.Weapon, "Psi Glove", 3, "A Psionic Weapon with electricity output");
        Item lightArmor = new(ItemType.Armor, "Shabby Armor", 4, "An implant that enhances AP +4");

        CharacterBase player = new Player(CharacterType.Player, "Hero", 50, 8, 2);
        List<CharacterBase> enemies = new();                                                
        CharacterBase enemy1 = new Enemy(CharacterType.Enemy, "Warbot", 25, 8, 3);
        CharacterBase enemy2 = new Enemy(CharacterType.Enemy, "Warbot", 25, 8, 3);
        enemies.Add(enemy1);
        enemies.Add(enemy2);

        player.Inventory.Add(naniteInjector.Clone());
        player.Inventory.Add(naniteInjector.Clone());
        player.Inventory.Add(psiGlove.Clone());
        player.Inventory.Add(lightArmor.Clone());


        foreach (CharacterBase enemy in enemies)
        {
            enemy.Inventory.Add(naniteInjector.Clone());
            enemy.Inventory.Add(psiGlove.Clone());
            enemy.Inventory.Add(lightArmor.Clone());
        }

        BattleManager.StartBattle(player, enemies);

        Console.ReadLine();
    }
}