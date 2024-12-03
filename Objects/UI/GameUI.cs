using TextAdventureGame.Objects.BattleSystem;
using TextAdventureGame.Objects.Character;

namespace TextAdventureGame.Objects.UI
{
    public class GameUI
    {
        public static void DisplayBattleFrames(CharacterBase player, List<CharacterBase> enemies)
        {
            Console.Clear();

            Console.WriteLine( " ********************");
            Console.WriteLine($" * Battle Initiated *");
            Console.WriteLine( " ********************");
            //Console.WriteLine($" {player.Name} vs {enemies[0].Name}\n");

            DisplayEnemiesHorizontally(enemies, ConsoleColor.Red);
            Console.WriteLine();
            DisplayPlayerBox(player, Console.ForegroundColor);

            var characterTurn = BattleManager.Turn == Turn.PlayerTurn ? "Player" : "Enemy";
            DisplayTurnTransition(characterTurn);
        }

        private static void DisplayPlayerBox(CharacterBase player, ConsoleColor boxColor)
        {
            Console.ForegroundColor = boxColor;

            var statsLines = GetCharacterBoxLines(player);
            var portraitLines = GetPlayerPortrait(player);

            int maxHeight = Math.Max(statsLines.Length, portraitLines.Length);

            for (int i = 0; i < maxHeight; i++)
            {
                string statsLine = i < statsLines.Length ? statsLines[i] : new string('_', statsLines[0].Length);
                string portraitLine = i < portraitLines.Length ? portraitLines[i] : new string(' ', portraitLines[0].Length);
                Console.WriteLine(statsLine + portraitLine);
            }

            Console.ResetColor();
        }

        private static void DisplayEnemiesHorizontally(List<CharacterBase> characters, ConsoleColor boxColor)
        {
            // Prepare ASCII lines for all characters
            List<string[]> statsBoxes = new();
            List<string[]> portraits = new();

            for (int i = 0; i < characters.Count; i++)
            {
                CharacterBase? character = characters[i];
                statsBoxes.Add(GetCharacterBoxLines(character, i + 1));
                portraits.Add(GetEnemyPortrait(character.Name));
            }

            int maxBoxHeight = statsBoxes[0].Length;
            int maxPortraitHeight = portraits[0].Length;
            int maxHeight = Math.Max(maxBoxHeight, maxPortraitHeight);

            for (int line = 0; line < maxHeight; line++)
            {
                foreach(var index in Enumerable.Range(0, characters.Count))
                {
                    string statsLine = line < statsBoxes[index].Length ? statsBoxes[index][line] : new string(' ', statsBoxes[index][0].Length);
                    string portraitLine = line < portraits[index].Length ? portraits[index][line] : new string(' ', portraits[index][0].Length);

                    Console.ForegroundColor = boxColor;
                    Console.Write(statsLine + portraitLine); // Print the current line of this box
                    Console.ResetColor();
                    Console.Write("  ");
                }

                Console.WriteLine(); // Move to the next line
            }
        }

        static string[] GetCharacterBoxLines(CharacterBase character, int? enemyIndex = null)
        {
            int maxTextWidth = 24;

            string characterType = enemyIndex.HasValue
                ? $"{character.Type}({enemyIndex.Value})"
                : character.Type.ToString();

            return
            [
                $" {FormatBoxText(characterType, maxTextWidth + 21)}",
                $" ┌─────────────────────────┬",
                $" │ {FormatBoxText(character.Name, maxTextWidth)}│",
                $" │ HP  {FormatBoxText(character.Health.ToString() + "/" + character.MaxHealth.ToString(), maxTextWidth - 4)}│",
                $" │ AP  {FormatBoxText(character.AttackPoints.ToString(), maxTextWidth - 4)}│",
                $" │ DEF {FormatBoxText(character.DefensePoints.ToString(), maxTextWidth - 4)}│",
                $" │ WEP {FormatBoxText(character.EquippedWeapon?.Name ?? "None", maxTextWidth - 4)}│",
                $" │ ARM {FormatBoxText(character.EquippedArmor?.Name ?? "None", maxTextWidth - 4)}│",
                $" └─────────────────────────┴"
            ];
        }

        static string[] GetEnemyPortrait(string enemyName)
        {
            return PortraitUI.GetEnemyPortrait(enemyName);
        }

        static string[] GetPlayerPortrait(CharacterBase character)
        {
            return PortraitUI.GetPlayerExpression("regular");
        }

        public static string FormatBoxText(string text, int maxWidth)
        {
            return text.PadRight(maxWidth);
        }

        public static void DisplayTurnTransition(string turnOwner)
        {
            Console.WriteLine("\n ============================");
            Console.WriteLine($" {turnOwner}'s Turn");
            Console.WriteLine(" ============================");
        }

        public static void DisplayBattleStatus(CharacterBase player, List<CharacterBase> enemies)
        {
            // Status
            Console.WriteLine(" Battle Status:");
            Console.WriteLine(" ==============");
            GameUI.DisplayBattleFrames(player, enemies);
        }

        public static void DisplayBattleFeedback(CharacterBase player, CharacterBase enemy, string feedback)
        {
            //Console.WriteLine("Feedback:");
            Console.WriteLine(" ---------");
            Console.WriteLine(feedback);
            Console.WriteLine();
        }

        public static void UpdatePortrait(CharacterBase character, string expression)
        {
            string[] portrait = character is Player 
                ? PortraitUI.GetPlayerExpression(expression.ToLower()) 
                : PortraitUI.GetEnemyPortrait(character.Name.ToLower());
        }
    }
}
