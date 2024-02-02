using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class SaveData
{
    public List<Pokemon> Pokemons = new List<Pokemon>();
    public Inventory Inventory = new Inventory();
    public int[] playerPos = new int[2];
    public string NamePlayer;

    public SaveData(List<Pokemon> pokemons, Inventory inventorys, int[] player)
    {
        NamePlayer = "test";
        Pokemons = pokemons;
        Inventory = inventorys;
        playerPos = player;
    }
}