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

            foreach(var character in characters)
            {
                statsBoxes.Add(GetCharacterBoxLines(character));
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

        static string[] GetCharacterBoxLines(CharacterBase character)
        {
            int maxTextWidth = 24;

            return
            [
                $" {FormatBoxText(character.Type.ToString(), maxTextWidth + 3)}",
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
            string[]? portraitExpressions = null;
            int maxTextWidth = 16;

            switch (enemyName)
            {
                case "Warbot":
                    portraitExpressions = [
                        " ",
                        $"──────────────────┐",
                        $" {FormatBoxText(@"  _/ _ -  _\_   ", maxTextWidth)} │",
                        $" {FormatBoxText(@" / // \ / \\ \  ", maxTextWidth)} │",
                        $" {FormatBoxText(@"| \\[+] [+]// | ", maxTextWidth)} │",
                        $" {FormatBoxText(@" \|/  / \  \|/  ", maxTextWidth)} │",
                        $" {FormatBoxText(@"    \|===|/     ", maxTextWidth)} │",
                        $" {FormatBoxText(@"  _/ \───/ \_   ", maxTextWidth)} │",
                        $"──────────────────┘"
                    ];
                    break;
                default:
                    Console.WriteLine("No portrait found.");
                    break;
            }

            return portraitExpressions;
        }

        static string[] GetPlayerExpression(string expression)
        {
            string[]? portraitExpressions = null;
            int maxTextWidth = 16;

            switch (expression)
            {
                case "regular":
                    portraitExpressions = [
                        " ",
                        $"──────────────────┐",
                        $" {FormatBoxText(@" /_/ _ '\' ─ \  ", maxTextWidth)} │",
                        $" {FormatBoxText(@" /  /_\| \_'\'\ ", maxTextWidth)} │",
                        $" {FormatBoxText(@"/_│ |o   o\/│ |\", maxTextWidth)} │",
                        $" {FormatBoxText(@"  |/   U   /|/  ", maxTextWidth)} │",
                        $" {FormatBoxText(@"    \ --- /     ", maxTextWidth)} │",
                        $" {FormatBoxText(@"   /|\───/|\    ", maxTextWidth)} │",
                        $"──────────────────┘"
                    ];
                    break;
                case "hurt":
                    portraitExpressions = [
                        " ",
                        $"──────────────────┐",
                        $" {FormatBoxText(@" /_/ _ '\' ─ \  ", maxTextWidth)} │",
                        $" {FormatBoxText(@" /  /_\| \_'\'\ ", maxTextWidth)} │",
                        $" {FormatBoxText(@"/_│ |@   @\/│ |\", maxTextWidth)} │",
                        $" {FormatBoxText(@"  |/   U   /|/  ", maxTextWidth)} │",
                        $" {FormatBoxText(@"    \ (_) /     ", maxTextWidth)} │",
                        $" {FormatBoxText(@"   /|\───/|\    ", maxTextWidth)} │",
                        $"──────────────────┘"
                    ];
                    break;
                case "cocky":
                    portraitExpressions = [
                        " ",
                        $"──────────────────┐",
                        $" {FormatBoxText(@" /_/ _ '\' ─ \  ", maxTextWidth)} │",
                        $" {FormatBoxText(@" /  /_\| \_'\'\ ", maxTextWidth)} │",
                        $" {FormatBoxText(@"/_│ |*   *\/│ |\", maxTextWidth)} │",
                        $" {FormatBoxText(@"  |/   U   /|/  ", maxTextWidth)} │",
                        $" {FormatBoxText(@"    \ <_> /     ", maxTextWidth)} │",
                        $" {FormatBoxText(@"   /|\───/|\    ", maxTextWidth)} │",
                        $"──────────────────┘"
                    ];
                    break;
                case "dead":
                    portraitExpressions = [
                        " ",
                        $"──────────────────┐",
                        $" {FormatBoxText(@" /_/ _ '\' ─ \  ", maxTextWidth)} │",
                        $" {FormatBoxText(@" /  /_\| \_'\'\ ", maxTextWidth)} │",
                        $" {FormatBoxText(@"/_│ |X   X\/│ |\", maxTextWidth)} │",
                        $" {FormatBoxText(@"  |/   U   /|/  ", maxTextWidth)} │",
                        $" {FormatBoxText(@"    \ [X] /     ", maxTextWidth)} │",
                        $" {FormatBoxText(@"   /|\───/|\    ", maxTextWidth)} │",
                        $"──────────────────┘"
                    ];
                    break;
                default:
                    Console.WriteLine("No portrait found.");
                    break;
            }

            return portraitExpressions;
        }

        static string[] GetPlayerPortrait(CharacterBase character)
        {
            int maxTextWidth = 16;
            Dictionary<string, string[]> expressions = new Dictionary<string, string[]>();
            string[] expressionNames = 
            {
                "regular",
                "hurt",
                "cocky",
                "dead"
            };

            for (int i = 0; i < expressions.Count; i++)
            {
                if (!expressions.ContainsKey(expressionNames[i]))
                {
                    expressions.Add(expressionNames[i], GetPlayerExpression(expressionNames[i]));
                }
            }

            

            return GetPlayerExpression("regular");
        }

        static string FormatBoxText(string text, int maxWidth)
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
    }
}
