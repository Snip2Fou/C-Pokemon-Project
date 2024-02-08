using System.Collections.Generic;

public class Trainer
{
    public string Name { get; set; }
    public int PokeMoney { get; set; }
    public Inventory Inventory { get; set; }
    public List<Pokemon> Team { get; set; }
    public List<Pokemon> BattleTeam { get; set; }
    public List<Pokemon> Pokedex { get; set; }

    public Trainer(string name)
    {
        Name = name;
        Team = new List<Pokemon>();
        Inventory = new Inventory();
        BattleTeam = new List<Pokemon>();
        Pokedex = new List<Pokemon>();
        PokeMoney = 10000;
    }


    public void AddPokemon(Pokemon pokemon)
    {
        Team.Add(pokemon);
    }
}