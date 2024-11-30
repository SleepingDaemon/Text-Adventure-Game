using TextAdventureGame.Objects.Character;
using TextAdventureGame.Objects.UI;

namespace TextAdventureGame.Objects.Game
{
    public class GameManager
    {
        private static GameManager? _instance;
        private bool _foundKey = false;

        public static GameManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }

        public void HandleCombatCommand(Player player, CharacterBase opponent, int playerInput)
        {
            switch (playerInput)
            {
                case 1:
                    player.Attack(opponent);
                    GameUI.DisplayBattleFeedback(player, opponent, $"{player.Name} attacks {opponent.Name} for {player.CalculateAttackDamage(opponent)}");
                    break;
                case 2:
                    int reducedDamage = opponent.AttackPoints - player.DefensePoints;
                    player.Defend(opponent);
                    GameUI.DisplayBattleFeedback(player, opponent, $"{player.Name} reduces attack damage from {reducedDamage} to {opponent.CalculateAttackDamage(player)}");
                    break;
                case 3:
                    player.DisplayInventory(player.Inventory.GetInventory());
                    break;
                default:
                    Console.WriteLine("Please Enter a Valid Input");
                    break;
            }

            Console.ReadLine();
        }
    }
}
