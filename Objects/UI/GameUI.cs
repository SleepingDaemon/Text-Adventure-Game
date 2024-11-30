using TextAdventureGame.Objects.Character;

namespace TextAdventureGame.Objects.UI
{
    public class GameUI
    {
        public static void DisplayBattleFrames(CharacterBase player, List<CharacterBase> enemies)
        {
            Console.Clear();

            Console.WriteLine($"Battle Encounter!");
            Console.WriteLine($"{player.Name} vs {enemies[0].Name}");

            DisplayEnemiesHorizontally(enemies, ConsoleColor.Red);
            Console.WriteLine();
            DisplayPlayerBox(player, Console.ForegroundColor);
        }

        private static void DisplayPlayerBox(CharacterBase player, ConsoleColor boxColor)
        {
            Console.ForegroundColor = boxColor;
            foreach(var line in GetCharacterBoxLines(player))
            {
                Console.WriteLine(line);
            }

            Console.ResetColor();
        }

        private static void DisplayEnemiesHorizontally(List<CharacterBase> characters, ConsoleColor boxColor)
        {
            // Prepare ASCII lines for all characters
            List<string[]> boxes = new();

            foreach(var character in characters)
            {
                boxes.Add(GetCharacterBoxLines(character));
            }

            // Print each line of all boxes horizontally
            int boxHeight = boxes[0].Length; // All boxes have the same height
            for (int line = 1; line < boxHeight; line++)
            {
                foreach(var box in boxes)
                {
                    Console.ForegroundColor = boxColor;
                    Console.Write(box[line]); // Print the current line of this box
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
                $"{character.Type}",
                $"┌─────────────────────────┐",
                $"│ {FormatBoxText(character.Name, maxTextWidth)}│",
                $"│ HP  {FormatBoxText(character.Health.ToString() + "/" + character.MaxHealth.ToString(), maxTextWidth - 4)}│",
                $"│ AP  {FormatBoxText(character.AttackPoints.ToString(), maxTextWidth - 4)}│",
                $"│ DEF {FormatBoxText(character.DefensePoints.ToString(), maxTextWidth - 4)}│",
                $"│ WEP                     │",
                $"│ ARM                     │",
                $"└─────────────────────────┘"
            ];
        }

        static string FormatBoxText(string text, int maxWidth)
        {
            return text.PadRight(maxWidth);
        }

        public static void DisplayLog(string message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayTurnTransition(string turnOwner)
        {
            Console.WriteLine("\n============================");
            Console.WriteLine($"{turnOwner}'s Turn");
            Console.WriteLine("============================");
        }

        public static void DisplayBattleStatus(CharacterBase player, List<CharacterBase> enemies)
        {
            // Status
            Console.WriteLine("Battle Status:");
            Console.WriteLine("==============");
            GameUI.DisplayBattleFrames(player, enemies);
        }

        public static void DisplayBattleFeedback(CharacterBase player, CharacterBase enemy, string feedback)
        {
            //Console.WriteLine("Feedback:");
            Console.WriteLine("---------");
            Console.WriteLine(feedback);
            Console.WriteLine();
        }
    }
}
