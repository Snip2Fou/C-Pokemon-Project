using System;
using System.Collections.Generic;
using System.IO;

public class SaveData
{
    public string NamePlayer;
    public List<Pokemon> Pokemons = new List<Pokemon>();
    public List<Object> Inventory = new List<Object>();
    public int[] playerPos = new int[2];

    public SaveData(string nameplayer, List<Pokemon> pokemons, List<Object> inventorys, int[] player)
    {
        NamePlayer = nameplayer;
        Pokemons = pokemons;
        Inventory = inventorys;
        playerPos = player;
    }
}