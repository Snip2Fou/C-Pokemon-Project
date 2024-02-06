using System.Collections.Generic;

public class Trainer
{
    public string Name { get; set; }
    public List<Pokemon> Team { get; }

    public Trainer(string name)
    {
        Name = name;
        Team = new List<Pokemon>();
    }

    public void AddPokemon(Pokemon pokemon)
    {
        Team.Add(pokemon);
    }
}