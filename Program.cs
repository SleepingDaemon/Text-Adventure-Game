using Microsoft.Win32;
using TextAdventureGame.Objects.BattleSystem;
using TextAdventureGame.Objects.Character;
using TextAdventureGame.Objects.InventorySystem;
using TextAdventureGame.Objects.RoomSystem;
using TextAdventureGame.Objects.StorySystem;
using TextAdventureGame.Objects.UI;

internal class Program
{
    private static void Main(string[] args)
    {

        Item naniteInjector = new(ItemType.Consumable, "Nanite Injector", 20, "Restores 20 HP");
        Item psiGlove = new(ItemType.Weapon, "Psi Glove", 3, "A Psionic Weapon with electricity output");
        Item lightArmor = new(ItemType.Armor, "Shabby Armor", 4, "An implant that enhances AP +4");

        Player player = new(CharacterType.Player, "Hero", 50, 8, 2);

        player.Inventory.Add(naniteInjector.Clone());
        player.Inventory.Add(naniteInjector.Clone());
        player.Inventory.Add(psiGlove.Clone());
        player.Inventory.Add(lightArmor.Clone());


        NodeManager nodeManager = new();
        player.xPos = 0;
        player.yPos = 1;
        nodeManager.InitializeHeartMap();
        nodeManager.ConnectHeartNodes();
        Node[,]? heartMap = nodeManager.HeartMap;
        MapUI.DisplayMap(heartMap, player.xPos, player.yPos);
        while (true)
        {
            Console.Write("\n Choose a direction to move: [up/down/left/right] - ");
            string direction = Console.ReadLine();
            player.MoveTo(direction, heartMap);
        }


        Console.ReadLine();
    }
}