namespace TextAdventureGame.Objects.Character
{
    public class Enemy : CharacterBase
    {
        public Enemy(CharacterType type, string name, int maxHealth, int attackPoints, int defensePoints) : base(type, name, maxHealth, attackPoints, defensePoints) { }
    }
}
