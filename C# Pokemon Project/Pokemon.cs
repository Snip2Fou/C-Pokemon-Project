class Pokemon
{
    public string Name { get; }
    public int Health { get; set; }
    public int Attack { get; }
    public int Defense { get; }

    public Pokemon(string name, int health, int attack, int defense)
    {
        Name = name;
        Health = health;
        Attack = attack;
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
}