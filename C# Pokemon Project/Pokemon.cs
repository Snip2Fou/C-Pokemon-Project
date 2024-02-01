class Pokemon
{
    public string Name { get; }
    public float Health { get; set; }
    public float Attack { get; set; }
    public float AttackSpecial { get; set; }
    public float Defense { get; set; }
    private float _level { get; set; }

    public Pokemon(string name, float health, float attack, float attackspecial, float defense)
    {
        Name = name;
        Health = health;
        Attack = attack;
        AttackSpecial = attackspecial;
        Defense = defense;
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
        Health = Health * 1.2f ;
        Attack = Attack * 1.2f ;
        AttackSpecial = AttackSpecial * 1.2f;
        Defense = Defense * 1.2f;
    }

    public void LevelUp()
    {
        _level += 1;
        StatUp();
    }

    public bool IsAlive()
    {
        if(Health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}