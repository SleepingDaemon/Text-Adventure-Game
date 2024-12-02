using TextAdventureGame.Objects.Character;

namespace TextAdventureGame.Objects.InventorySystem
{
    public class Item
    {
        public string Name;
        public string Description;
        public ItemType Type;
        public int Value;
        public int Quantity = 1;
        public bool IsEquipped;

        public Item(ItemType type, string name, int value, string description)
        {
            Type = type;
            Name = name;
            Description = description;
            Value = value;
            IsEquipped = false;
        }

        public Item Clone()
        {
            return new Item(Type, Name, Value, Description);
        }

        public override string ToString()
        {
            return $"{Name} (x{Quantity})";
        }
    }
}
