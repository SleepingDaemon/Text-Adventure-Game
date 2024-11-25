namespace TextAdventureGame.Objects.Inventory
{
    public class Inventory
    {
        private List<Item> _items;
        public Inventory()
        {
            _items = [];
        }

        public void Add(Item item)
        {
            if (!_items.Contains(item))
                _items.Add(item);
        }

        public void Remove(Item item)
        {
            if (_items.Contains(item))
                _items.Remove(item);
        }
    }
}
