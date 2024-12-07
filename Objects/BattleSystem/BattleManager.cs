using TextAdventureGame.Objects.Character;
using TextAdventureGame.Objects.Game;
using TextAdventureGame.Objects.InventorySystem;
using TextAdventureGame.Objects.RoomSystem;
using TextAdventureGame.Objects.UI;

public enum Turn { PlayerTurn, EnemyTurn }

namespace TextAdventureGame.Objects.BattleSystem
{

    public class BattleManager
    {
        public static Turn Turn;
        public static CharacterBase? currentPlayer;
        public static int currentEnemyIndex;
        public static List<CharacterBase>? battleEnemies;

        public static void StartBattle(CharacterBase player, List<CharacterBase> enemies, Node currentNode)
        {
            battleEnemies = new(enemies);
            currentPlayer = player;
            currentEnemyIndex = 0;

            InitEventHandlers(player, enemies);

            if(currentNode.HasEnemy && !currentNode.IsCleared)
            {
                while (player.IsAlive() && battleEnemies.Any(e => e.IsAlive()))
                {
                    Turn = Turn.PlayerTurn;
                    GameUI.DisplayBattleStatus(player, battleEnemies);
                    HandlePlayerTurn(player, battleEnemies[currentEnemyIndex]);

                    Turn = Turn.EnemyTurn;
                    CycleEnemyTurn(player);

                    //Console.ReadLine();
                }
            }

            currentNode.IsCleared = true;
            CleanupEventHandlers(player, enemies);
        }

        private static void InitEventHandlers(CharacterBase player, List<CharacterBase> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.OnDeath += HandleEnemyDeath;
                enemy.OnHealthChanged += (character, newHealth) => HandleOnHealthChanged(character, newHealth);
            }

            player.OnHealthChanged += (character, newHealth) => HandleOnHealthChanged(character, newHealth);
            player.OnDeath += HandlePlayerDeath;
        }

        private static void CleanupEventHandlers(CharacterBase player, List<CharacterBase> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.OnDeath -= HandleEnemyDeath;
                enemy.OnHealthChanged -= HandleOnHealthChanged;
            }

            player.OnHealthChanged -= HandleOnHealthChanged;
            player.OnDeath -= HandlePlayerDeath;
        }

        private static void HandlePlayerDeath(CharacterBase player)
        {
            // Display Reset Options
        }

        private static void HandleOnHealthChanged(CharacterBase character, int newHealth)
        {
            //Console.Clear();
            var characterTurn = character is Enemy ? "Enemy" : "Player";
            GameUI.DisplayTurnTransition(characterTurn);
            GameUI.DisplayBattleFeedback(character, null, $" {character.Name}'s health is now {newHealth}/{character.MaxHealth}.");
            GameUI.DisplayBattleFrames(currentPlayer, battleEnemies);
        }

        private static void HandleEnemyDeath(CharacterBase enemy)
        {
            enemy = null;
        }

        private static void CycleEnemyTurn(CharacterBase player)
        {
            if (!player.IsAlive()) return;

            int enemiesCount = battleEnemies.Count;

            for (int i = 0; i < enemiesCount; i++)
            {
                currentEnemyIndex = (currentEnemyIndex + 1) % enemiesCount;
                var currentEnemy = battleEnemies[currentEnemyIndex];

                if (currentEnemy.IsAlive())
                {
                    GameUI.DisplayBattleStatus(player, battleEnemies);
                    HandleEnemyTurn(player, currentEnemy);
                }
            }
        }

        private static void HandleEnemyTurn(CharacterBase player, CharacterBase currentEnemy)
        {
            Random random = new();
            Thread.Sleep(2000);

            if (currentEnemy.Health < currentEnemy.MaxHealth * 0.2 &&
                currentEnemy.Inventory.GetInventory().Any(i => i.Type == ItemType.Consumable))
            {
                UseConsumables(currentEnemy);
            }
            else
            {
                //Thread.Sleep(2000);
                //random.Next(1, 2);
                if(random.Next(0, 3) == 0)
                {
                    currentEnemy.Defend();
                    GameUI.DisplayBattleFeedback(player, currentEnemy, $" {currentEnemy.Name} reduces {player.Name}'s attack by half.");
                }
                else
                {
                    currentEnemy.Attack(player);
                    GameUI.DisplayBattleFeedback(player, currentEnemy, $" {currentEnemy.Name} attacks {player.Name} for {currentEnemy.CalculateAttackDamage(player)}");
                }
            }

            Console.ReadLine();
        }

        private static void UseConsumables(CharacterBase currentEnemy)
        {
            foreach (var item in currentEnemy.Inventory.GetInventory())
            {
                if (item.Type == ItemType.Consumable)
                {
                    currentEnemy.Use(item);
                    return; // use one consumable at a time
                }
            }
        }

        private static void HandlePlayerTurn(CharacterBase player, CharacterBase currentEnemy)
        {
            Console.WriteLine("\n Actions:");
            Console.Write(" [1] Attack    [2] Defend    [3] Inventory    [ENTER] End Turn    ");

            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int inputNumber) || inputNumber < 1 || inputNumber > 4)
            {
                Console.WriteLine(" Invalid choice. Please enter a number between 0 and 4.");
                return;
            }

            GameManager.Instance.HandleCombatCommand((Player)player, currentEnemy, inputNumber);
        }
    }
}
