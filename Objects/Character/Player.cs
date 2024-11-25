namespace TextAdventureGame.Objects.Character
{
    public class Player : Character
    {


        public Player(string name, int maxHealth, int attackPoints, int defensePoints) : base(name, maxHealth, attackPoints, defensePoints) { }

        protected override void Attack(Character character)
        {
            throw new NotImplementedException();
        }
    }
}
