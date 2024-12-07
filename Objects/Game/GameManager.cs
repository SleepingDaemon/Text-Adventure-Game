using TextAdventureGame.Objects.Character;
using TextAdventureGame.Objects.UI;

namespace TextAdventureGame.Objects.Game
{
    public enum GameState
    {
        None,
        MainMenu,
        Active,
        World,
        Battle,
        Puzzle,
    }

    public class GameManager
    {
        private static GameManager? _instance;
        private GameState _state;
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
                    if (opponent.IsDefending)
                        GameUI.DisplayBattleFeedback(player, opponent, $" {player.Name} attacks {opponent.Name} for {player.Damage / 2}");
                    else
                        GameUI.DisplayBattleFeedback(player, opponent, $" {player.Name} attacks {opponent.Name} for {player.Damage}");
                    break;
                case 2:
                    player.Defend();
                    GameUI.DisplayBattleFeedback(player, opponent, $" {player.Name} reduces {opponent.Name}'s next attack by half.");
                    break;
                case 3:
                    player.CheckInventory(player.Inventory.GetInventory());
                    break;
                default:
                    Console.WriteLine("Please Enter a Valid Input");
                    break;
            }

            Console.ReadLine();
        }
    }
}
