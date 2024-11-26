namespace TextAdventureGame.Objects.InventorySystem
{
    public class Item
    {
        public ItemType Type;
        public string Name;
        public string? Description;
        public int Value;
        public int Amount;
        public bool IsEquipped {  get; set; }

        public Item(ItemType type, string name, int value)
        {
            Type = type;
            Name = name;
            Value = value;
        }
    }
}
