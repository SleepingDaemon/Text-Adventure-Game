namespace TextAdventureGame.Objects.InventorySystem
{

    public class Inventory
    {
        private List<Item> _items;
        public Inventory()
        {
            _items = [];
        }

        public List<Item> GetInventory() => _items;

        public void Add(Item item)
        {
            if (!_items.Contains(item))
                _items.Add(item);
            else if (_items.Contains(item))
            {
                item.Amount++;
                _items.Add(item);
            }
        }

        public void Remove(Item item) => _items.Remove(item);

        public void Clear() => _items.Clear();
    }
}
