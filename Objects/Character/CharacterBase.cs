using TextAdventureGame.Objects.InventorySystem;
using TextAdventureGame.Objects.UI;

namespace TextAdventureGame.Objects.Character
{
    public enum CharacterType { Player, Enemy, Boss }

    public abstract class CharacterBase
    {
        public event Action<CharacterBase> OnDeath;
        public event Action<CharacterBase, int> OnHealthChanged;

        protected Inventory _inventory;
        protected CharacterType _type;
        protected string _name;
        protected int _health;
        protected int _maxHealth;
        protected int _attackPoints;
        protected int _defensePoints;

        public Inventory Inventory => _inventory;
        public CharacterType Type => _type;
        public string Name => _name;
        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public int AttackPoints => _attackPoints;
        public int DefensePoints => _defensePoints;

        public CharacterBase(CharacterType type, string name, int maxHealth, int attackPoints, int defensePoints)
        {
            _inventory = new Inventory();
            _type = type;
            _name = name;
            _maxHealth = maxHealth;
            _health = maxHealth;
            _attackPoints = attackPoints;
            _defensePoints = defensePoints;
        }

        public virtual void Attack(CharacterBase opponent)
        {
            // Ensure the opponent's state is consistent after an attack or defense action.
            if (!opponent.IsAlive())
            {
                Console.WriteLine($"{opponent.Name} is dead.");
                return;
            }

            opponent.TakeDamage( CalculateAttackDamage(opponent));
        }

        public virtual void Defend(CharacterBase opponent)
        {
            int damageReceived = CalculateAttackDamage(opponent);
            _health = Math.Max(_health, damageReceived);
        }

        public int CalculateAttackDamage(CharacterBase opponent)
        {
            return CalculateDamage(opponent.AttackPoints, _defensePoints);
        }

        public virtual void TakeDamage(int amount)
        {
            _health -= amount;
            OnHealthChanged?.Invoke(this, _health);

            if (_health <= 0)
            {
                OnDeath?.Invoke(this);
                _health = 0;

                if (this is Enemy)
                    Console.WriteLine($"{_name} is dead.");
                else if (this is Player)
                    Console.WriteLine("You have been defeated.");
            }
        }

        public void Use(Item item)
        {
            if (!_inventory.GetInventory().Contains(item))
            {
                Console.WriteLine($"{item.Name} is not in the inventory.");
                return;
            }

            switch (item.Type)
            {
                case ItemType.Consumable:
                    Heal(item);
                    break;
                case ItemType.Weapon:
                    Equip(item);
                    break;
                case ItemType.Armor:
                    break;
                default:
                    Console.WriteLine($"Cannot use {item.Name}. Unknown item type.");
                    break;
            }

            Console.WriteLine($"{_name} used " + item.Name);
        }

        private void Heal(Item item)
        {
            if(!_inventory.GetInventory().Contains(item)) return;

            if (_health == _maxHealth)
            {
                Console.WriteLine("Cannot use item. Health is Full");
                return;
            }

            if (_health > 0 && _health < _maxHealth)
                _health += item.Value;

            if (_health > _maxHealth)
                _health = _maxHealth;

            _inventory.Remove(item);
        }

        public void Equip(Item equippableItem)
        {
            if (!_inventory.GetInventory().Contains(equippableItem))
            {
                Console.WriteLine($"{equippableItem.Name} is not in the inventory.");
                return;
            }

            equippableItem.IsEquipped = true;
            if(equippableItem.Type == ItemType.Weapon)
            {
                _attackPoints += equippableItem.Value;
            }
            else if(equippableItem.Type == ItemType.Armor)
            {
                _defensePoints += equippableItem.Value;
            }
                
        }

        public void UnEquip(Item equippableItem)
        {
            equippableItem.IsEquipped = false ;
        }

        public bool IsAlive() => _health > 0;

        public int CalculateDamage(int amount, int defense) => Math.Max(amount - defense, 0);

        public List<Item> GetInventory() => _inventory.GetInventory();

    }
}
