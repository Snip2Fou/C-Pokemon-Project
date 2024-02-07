using System.Collections.Generic;

public class Trainer
{
    public string Name { get; set; }
    public List<Pokemon> Team { get; }
    public Inventory Inventory { get; set; }

    public Trainer(string name)
    {
        Name = name;
        Team = new List<Pokemon>();
        Inventory = new Inventory();
    }


    public void AddPokemon(Pokemon pokemon)
    {
        Team.Add(pokemon);
    }
}