using System;

class Object
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Object(string name, string description) 
    { 
        Name = name;
        Description = description;
    }

    public void UseThis(Pokemon pokemon)
    { 
        if (Name == "potion de soin")
        {
            pokemon.Heal();
        }else if (Name == "pokeball")
        {
            Console.WriteLine("je lance ma pokeball");
        }
    }
}