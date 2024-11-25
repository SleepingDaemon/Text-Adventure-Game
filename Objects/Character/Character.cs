namespace TextAdventureGame.Objects.Character
{
    public abstract class Character(string name, int maxHealth, int attackPoints, int defensePoints)
    {
        protected Inventory _inventory = new Inventory();
        protected string _name = name;
        protected int _health = maxHealth;
        protected int _maxHealth;
        protected int _attackPoints = attackPoints;
        protected int _defensePoints = defensePoints;

        protected abstract void Attack(Character character);

    }
}
