namespace TextAdventureGame.Objects.RoomSystem
{
    public class Node
    {
        public static int NodeCount = 0;
        public int NodeID {  get; set; }
        public int EnergyChange { get; set; }       // A toxic node reduces energy
        public string EnvironmentEffect { get; set; } // "toxic zone," "calm chamber"
        public string Description { get; set; }
        public List<string> Characters { get; set; }
        public string MemoryFragment { get; set; }
        public bool HasItem { get; set; }
        public bool HasEnemy { get; set; }


        // Puzzle Properties
        public bool HasPuzzle { get; set; }
        public string PuzzleDescription { get; set; }
        public bool IsPuzzleSolved { get; set; }

        // Room Properties
        public string NodeType { get; set; }
        public bool IsAccessible { get; set; }
        public bool IsCleared { get; set; }
        public bool IsDiscovered { get; set; }

        public Dictionary<string, Node> Connections { get; set; }

        public Node(string description, bool hasItem, bool hasEnemy, string nodeType)
        {
            NodeCount++;
            NodeID = NodeCount;
            Description = description;
            HasItem = hasItem;
            HasEnemy = hasEnemy;
            IsAccessible = true;
            IsCleared = false;
            IsDiscovered = false;
            Connections = [];
        }

        public string GetNodeSymbol()
        {
            if (!IsDiscovered) return "  ?  │";
            if (IsCleared) return "  C  │";
            if (IsPuzzleSolved) return "  PS │";
            if (NodeType == "Locked") return "  L  │";
            return NodeType switch
            {
                "Safe" => "  S  │",
                "Enemy" => "  E  │",
                "Puzzle" => "  PU │",
                "Memory" => "  M  │",
                _ => "     │"
            };
        }
    }
}
