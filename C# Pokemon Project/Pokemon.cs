class Pokemon
{
    public string Name { get; set; }
    public string TypeOne { get; set; }
    public string TypeTwo { get; set; }
    public int Total { get; set; }
    public float Health { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float AttackSpecial { get; set; }
    public float DefenseSpecial { get; set; }
    public float Speed { get; set; }
    public int Generation { get; set; }
    public bool Legendary { get; set; }
    private int _level { get; set; }

    

    public Pokemon(string name, string typeOne, string typeTwo, int total ,float health, float attack, float defense, float attackspecial, float defensespecial, float speed, int generation, bool legendary)
    {
        Name = name;
        TypeOne = typeOne;
        TypeTwo = typeTwo;
        Total = total;
        Health = health;
        Attack = attack;
        Defense = defense;
        AttackSpecial = attackspecial;
        DefenseSpecial = defensespecial;    
        Speed = speed;
        Generation = generation;    
        Legendary = legendary;
        _level = 1;
    }

    public void TakeDamage(float damage)
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

    public void StatUp()
    {
        Health = Health * 1.2f;
        Attack = Attack * 1.2f;
        Defense = Defense * 1.2f;
        AttackSpecial = AttackSpecial * 1.2f;
        DefenseSpecial = DefenseSpecial * 1.2f;
        Speed = Speed * 1.2f;
    }

    public void LevelUp()
    {
        _level += 1;
        StatUp();
    }

}