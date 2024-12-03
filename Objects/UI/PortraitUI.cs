namespace TextAdventureGame.Objects.UI
{
    public class PortraitUI
    {
        private static readonly Dictionary<string, string[]> PlayerExpressions = [];
        private static readonly Dictionary<string, string[]> EnemyPortraits = [];
        private const int MAX_TEXT_WIDTH = 16;

        static PortraitUI()
        {
            InitializePlayerExpressions(MAX_TEXT_WIDTH);
            InitializeEnemyPortraits(MAX_TEXT_WIDTH);
        }

        private static void InitializeEnemyPortraits(int maxTextWidth)
        {
            PlayerExpressions["regular"] =
            [
                " ",
                $"──────────────────┐",
                $" {GameUI.FormatBoxText(@" /_/ _ '\' ─ \  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@" /  /_\| \_'\'\ ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"/_│ |o   o\/│ |\", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"  |/   U   /|/  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"    \ --- /     ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"   /|\───/|\    ", maxTextWidth)} │",
                $"──────────────────┘"
            ];

            PlayerExpressions["hurt"] =
            [
                " ",
                $"──────────────────┐",
                $" {GameUI.FormatBoxText(@" /_/ _ '\' ─ \  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@" /  /_\| \_'\'\ ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"/_│ |_   _\/│ |\", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"  |/   U   /|/  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"    \ (_) /     ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"   /|\───/|\    ", maxTextWidth)} │",
                $"──────────────────┘"
            ];

            PlayerExpressions["cocky"] = new[]
            {
                " ",
                $"──────────────────┐",
                $" {GameUI.FormatBoxText(@" /_/ _ '\' ─ \  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@" /  /_\| \_'\'\ ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"/_│ |*   *\/│ |\", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"  |/   U   /|/  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"    \ <_> /     ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"   /|\───/|\    ", maxTextWidth)} │",
                $"──────────────────┘"
            };

            PlayerExpressions["dead"] = [
                " ",
                $"──────────────────┐",
                $" {GameUI.FormatBoxText(@" /_/ _ '\' ─ \  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@" /  /_\| \_'\'\ ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"/_│ |X   X\/│ |\", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"  |/   U   /|/  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"    \ [X] /     ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"   /|\───/|\    ", maxTextWidth)} │",
                $"──────────────────┘"
            ];
        }

        private static void InitializePlayerExpressions(int maxTextWidth)
        {
            EnemyPortraits["Warbot"] = [
                " ",
                $"──────────────────┐",
                $" {GameUI.FormatBoxText(@"  _/ _ -  _\_   ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@" / // \ / \\ \  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"| \\[+] [+]// | ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@" \|/  / \  \|/  ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"    \|===|/     ", maxTextWidth)} │",
                $" {GameUI.FormatBoxText(@"  _/ \───/ \_   ", maxTextWidth)} │",
                $"──────────────────┘"
            ];
        }

        public static string[] GetPlayerExpression(string expression)
        {
            return PlayerExpressions.TryGetValue(expression, out var portrait) 
                ? portrait 
                : [];
        }

        public static string[] GetEnemyPortrait(string enemyName)
        {
            return EnemyPortraits.TryGetValue(enemyName, out var portrait)
                ? portrait
                : [];
        }
    }
}
