using TextAdventureGame.Objects.InventorySystem;

namespace TextAdventureGame.Objects.Character
{
    public abstract class Character
    {
        protected Inventory _inventory;
        protected string _name;
        protected int _health;
        protected int _maxHealth;
        protected int _attackPoints;
        protected int _defensePoints;

        public Character(string name, int maxHealth, int attackPoints, int defensePoints)
        {
            _inventory = new Inventory();
            _name = name;
            _maxHealth = maxHealth;
            _health = maxHealth;
            _attackPoints = attackPoints;
            _defensePoints = defensePoints;
        }

        public virtual void Attack(Character opponent) => opponent._health -= _attackPoints;
        public virtual void EquipItem(Item item)
        {
            if(item.Type == ItemType.Weapon)
            {
                item.IsEquipped = true;
                _attackPoints += item.Value;
                Console.WriteLine($"{item.Name} is equipped.");
            }
            else if(item.Type == ItemType.Armor)
            {
                item.IsEquipped = true;
                _defensePoints += item.Value;
                Console.WriteLine($"{item.Name} is worn.");
            }
        }
        public virtual void UseItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Consumable:
                    if (_health == _maxHealth)
                    {
                        Console.WriteLine("Cannot use item. Health is Full");
                        break;
                    }

                    if(_health < _maxHealth)
                    {
                        _health += item.Value;
                        if (_health > _maxHealth)
                            _health = _maxHealth;
                    }

                    _inventory.Remove(item);
                    break;
                case ItemType.Weapon:
                case ItemType.Armor:
                case ItemType.Implant:
                case ItemType.KeyCard:
                    break;
                default:
                    break;
            }

            Console.WriteLine($"{_name} used " + item.Name);
        }

        public int GetHealth() => _health;
        public int GetMaxHealth() => _maxHealth;

        public List<Item> GetInventory() => _inventory.GetInventory();

        public int GetAttackPoints() => _attackPoints;
    }
}
