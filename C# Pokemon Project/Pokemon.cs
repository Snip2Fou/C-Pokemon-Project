class Pokemon
{
    public string Name { get; }
    public int Health { get; set; }
    public int Attack { get; }
    public int AttackSpecial { get; }
    public int Defense { get; }

    public Pokemon(string name, int health, int attack, int attackspecial, int defense)
    {
        Name = name;
        Health = health;
        Attack = attack;
        AttackSpecial = attackspecial;
        Defense = defense;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    public void Heal()
    {
        Health += 5;
    }
}