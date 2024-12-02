using TextAdventureGame.Objects.InventorySystem;
using TextAdventureGame.Objects.UI;

namespace TextAdventureGame.Objects.Character
{
    public enum CharacterType { Player, Enemy, Boss }

    public abstract class CharacterBase(CharacterType type, string name, int maxHealth, int attackPoints, int defensePoints)
    {
        public event Action<CharacterBase>? OnDeath;
        public event Action<CharacterBase, int>? OnHealthChanged;
        public event Action<Item>? OnItemEquipped;
        public event Action<Item>? OnItemUnEquipped;

        protected Inventory? _inventory;
        protected Item? _equippedWeapon;
        protected Item? _equippedArmor;
        protected CharacterType _type = type;
        protected string _name = name;
        protected int _health = maxHealth;
        protected int _maxHealth = maxHealth;
        protected int _attackPoints = attackPoints;
        protected int _defensePoints = defensePoints;
        protected bool _isDefending;

        public Inventory Inventory => _inventory;
        public CharacterType Type => _type;
        public string Name => _name;
        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public int AttackPoints => _attackPoints;
        public int DefensePoints => _defensePoints;
        public double Damage { get; set; }
        public Item? EquippedWeapon { get => _equippedWeapon; private set => _equippedWeapon = value; }
        public Item? EquippedArmor { get => _equippedArmor; private set => _equippedArmor = value; }
        public bool IsDefending { get => _isDefending; set => _isDefending = value; }

        public virtual void Attack(CharacterBase opponent)
        {
            // Ensure the opponent's state is consistent after an attack or defense action.
            if (!opponent.IsAlive())
            {
                Console.WriteLine($" {opponent.Name} is dead.");
                return;
            }

            double damage = CalculateAttackDamage( opponent );

            if (opponent._isDefending)
            {
                damage /= 2;
                opponent._isDefending = false;
            }

            Damage = (int)damage;
            
            opponent.TakeDamage((int)Math.Round(Damage));
        }

        public virtual void Defend() => _isDefending = true;

        public int CalculateAttackDamage(CharacterBase opponent)
        {
            return Math.Max(this.AttackPoints, opponent.DefensePoints);
        }

        public virtual void TakeDamage(int amount)
        {
            _health -= amount;
            OnHealthChanged?.Invoke(this, _health);

            if (_health <= 0)
            {
                _health = 0;
                OnDeath?.Invoke(this);

                if (this is Enemy)
                    Console.WriteLine($" {_name} is dead.");
                else if (this is Player)
                    Console.WriteLine(" You have been defeated.");
            }
        }

        public void Use(Item item)
        {
            if (!_inventory.GetInventory().Contains(item))
            {
                Console.WriteLine($" {item.Name} is not in the inventory.");
                return;
            }

            switch (item.Type)
            {
                case ItemType.Consumable:
                    Heal(item);
                    _inventory.Remove(item);
                    Console.WriteLine($" {_name} used " + item.Name);
                    break;
                case ItemType.Weapon or ItemType.Armor:
                    Equip(item);
                    Console.WriteLine($" {_name} equipped " + item.Name);
                    break;
                default:
                    Console.WriteLine($" Cannot use {item.Name}. Unknown item type.");
                    break;
            }
        }

        private void Heal(Item item)
        {
            if(!_inventory.GetInventory().Contains(item)) return;

            if (_health == _maxHealth)
            {
                Console.WriteLine(" Cannot use item. Health is Full");
                return;
            }

            if (_health > 0 && _health < _maxHealth)
            {
                _health += item.Value;
                OnHealthChanged?.Invoke(this, _health);
            }

            if (_health > _maxHealth)
                _health = _maxHealth;
        }

        public void Equip(Item equippableItem)
        {
            if (!_inventory.GetInventory().Contains(equippableItem))
            {
                Console.WriteLine($" {equippableItem.Name} is not in the inventory.");
                return;
            }

            if (equippableItem.IsEquipped)
            {
                UnEquip(equippableItem);
                return;
            }

            equippableItem.IsEquipped = true;
            if(equippableItem.Type == ItemType.Weapon)
            {
                _equippedWeapon = equippableItem;
                _attackPoints += equippableItem.Value;
                OnItemEquipped?.Invoke(equippableItem);
            }
            else if(equippableItem.Type == ItemType.Armor)
            {
                _equippedArmor = equippableItem;
                _defensePoints += equippableItem.Value;
                OnItemEquipped?.Invoke(equippableItem);
            }
                
        }

        public void UnEquip(Item equippableItem)
        {
            if (equippableItem.Type == ItemType.Weapon && EquippedWeapon == equippableItem)
            {
                _attackPoints -= equippableItem.Value;
                EquippedWeapon = null;
                OnItemUnEquipped?.Invoke(equippableItem);
            }
            else if (equippableItem.Type == ItemType.Armor && EquippedArmor == equippableItem)
            {
                _defensePoints -= equippableItem.Value;
                EquippedArmor = null;
                OnItemUnEquipped?.Invoke(equippableItem);
            }
        }

        public bool IsAlive() => _health > 0;

        public int CalculateDamage(int amount, int defense) => Math.Max(amount - defense, 0);

        public List<Item> GetInventory() => _inventory.GetInventory();

    }
}
