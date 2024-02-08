using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Newtonsoft.Json;

public class Game
{
    private static Game instance;

    public bool isRunning = true;
    protected Map map = new Map();
    protected int[] playerPos = new int[2];
    public List<Pokemon> pokemons = new List<Pokemon>();
    public List<string> type_list = new List<string>();
    public Trainer player = new Trainer("");
    public Dictionary<string, Dictionary<string, List<double>>> type_chart = new Dictionary<string, Dictionary<string, List<double>>>();
    public List<Capacity> all_capacity = new List<Capacity>();
    public PokeCenter pokeCenter;
    public House home;

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }
    }

    private Game()
    {
        LoadData();
        pokeCenter = new PokeCenter(player);
        home = new House(player);
    }

    public void LoadData()
    {
        string filePath = "data/pokemon.csv";
        string filePathCapacity = "data/moves.csv";
        string filePathCapacitySets = "data/movesets.csv";
        string filePathType = "data/type-chart.csv";

        if (File.Exists(filePathType))
        {
            using (StreamReader readerType = new StreamReader(filePathType))
            {
                string lineType = readerType.ReadLine();
                string[] valuesType = lineType.Split(',');

                Dictionary<string, List<double>> dico_type_int = new Dictionary<string, List<double>>();
                List<double> list_double = new List<double>();
                for (int i = 2; i < valuesType.Length; i++)
                {
                    type_list.Add(valuesType[i]);
                }
                while (!readerType.EndOfStream)
                {
                    lineType = readerType.ReadLine();
                    valuesType = lineType.Split(',');

                    list_double = new List<double>();
                    dico_type_int = new Dictionary<string, List<double>>();
                    for (int k = 2; k < valuesType.Length; k++)
                    {
                        if (double.TryParse(valuesType[k], out double value_double))
                        {
                            list_double.Add(value_double);
                        }
                        else
                        {
                            list_double.Add(0.5);
                        }
                    }
                    dico_type_int.Add(valuesType[1], list_double);
                    for (int i = 1; i < type_list.Count; i++)
                    {
                        lineType = readerType.ReadLine();
                        valuesType = lineType.Split(',');

                        list_double = new List<double>();
                        for (int k = 2; k < valuesType.Length; k++)
                        {
                            if (double.TryParse(valuesType[k], out double value_double))
                            {
                                list_double.Add(value_double);
                            }
                            else
                            {
                                list_double.Add(0.5);
                            }
                        }
                        dico_type_int.Add(valuesType[1], list_double);
                    }
                    type_chart.Add(valuesType[0], dico_type_int);
                }
            }
        }
        else
        {
            Console.WriteLine("Le fichier n'existe pas.");
        }

        if (File.Exists(filePathCapacity))
        {
            using (StreamReader readerCapacity = new StreamReader(filePathCapacity))
            {
                readerCapacity.ReadLine();

                while (!readerCapacity.EndOfStream)
                {
                    string line_capacity = readerCapacity.ReadLine();
                    string[] values_capacity = line_capacity.Split(',');

                    all_capacity.Add(new Capacity(values_capacity, type_list));
                }
            }
        }
        else
        {
            Console.WriteLine("Le fichier n'existe pas.");
        }

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    Pokemon pokemon = new Pokemon
                    {
                        Name = values[2],
                        TypeOne = values[4],
                        TypeTwo = values[5],
                        Health = int.Parse(values[9]),
                        Attack = int.Parse(values[10]),
                        Defense = int.Parse(values[11]),
                        AttackSpecial = int.Parse(values[12]),
                        DefenseSpecial = int.Parse(values[13]),
                        Speed = int.Parse(values[14]),
                        Total = int.Parse(values[15])
                    };
                    pokemon.Pv = pokemon.GetPvByFormule();
                    pokemon.PvMax = pokemon.GetPvByFormule();

                    bool pokemon_all_capacity_found = false;
                    if (File.Exists(filePathCapacitySets))
                    {
                        using (StreamReader readerCapacitySets = new StreamReader(filePathCapacitySets))
                        {
                            readerCapacitySets.ReadLine();

                            while (!pokemon_all_capacity_found && !readerCapacitySets.EndOfStream)
                            {
                                string line_all_capacity = readerCapacitySets.ReadLine();
                                string[] values_all_capacity = line_all_capacity.Split(',');
                                if (values_all_capacity[1] == pokemon.Name)
                                {
                                    pokemon.NextLearnCapacity = new string[values_all_capacity.Length];
                                    values_all_capacity.CopyTo(pokemon.NextLearnCapacity,0);
                                    for (int i = 3; i < values_all_capacity.Length; i++)
                                    {
                                        if (values_all_capacity[i].Contains("Start"))
                                        {
                                            string[] parts = values_all_capacity[i].Split('-');
                                            foreach (var capacity in all_capacity)
                                            {
                                                if (parts[1].Contains(capacity.Name))
                                                {
                                                    pokemon.AddCapacity(capacity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    pokemon_all_capacity_found = true;
                                }
                            }

                            pokemons.Add(pokemon);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Le fichier n'existe pas.");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Le fichier n'existe pas.");
        }
    }

    public void SaveGame()
    {
        try
        {
            SaveData game_save = new SaveData(player.Name, player.PokeMoney, player.Team, player.Pokedex, player.Inventory.SaveInventory(), playerPos);
            string jsonData = JsonConvert.SerializeObject(game_save);
            File.WriteAllText("data/game_save.json", jsonData);
            Console.WriteLine("sauvegarde");
            GameLoop();
        }
        catch (FieldAccessException)
        {
            Console.WriteLine("Aucun fichier trouvé");
            Console.ReadKey();
        }
        catch (JsonException)
        {
            Console.WriteLine("Erreur lors de la sauvegarde");
            Console.ReadKey();
        }       
    }

    public void LoadGame()
    {
        try
        {
            string jsonData = File.ReadAllText("data/game_save.json");
            SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonData);
            player.Name = data.NamePlayer;
            player.PokeMoney = data.PokeMoney;
            foreach (var poke in data.Pokemons)
            {
                player.Team.Add(poke);
            }
            foreach (var pokebis in data.Pokedex)
            {
                player.Pokedex.Add(pokebis);
            }
            foreach(var obj in data.Inventory)
            {
               player.Inventory.AddObject(obj, obj.Quantity);
            }
            playerPos = data.playerPos;
            Console.WriteLine("Partie chargée");
            Console.ReadKey();
            GameLoop();
        }
        catch (FieldAccessException) 
        {
            Console.WriteLine("Aucun sauvegarde trouvé");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la lecture du fichier de sauvegarde : {ex.Message}");
            Console.ReadKey();
        }
    }

    public void Start() 
    {
        playerPos[0] = map.size_x / 2;
        playerPos[1] = map.size_y / 2;
        map.map.SetValue('0', playerPos[0], playerPos[1]);
        Npc Pr_Chen = new NpcPrChen();
        Pr_Chen.LaunchNpc();
        GameLoop();
    }

    public void GameLoop()
    {
        while (isRunning)
        {
            Console.Clear();
            map.Draw();
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.Clear();

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.P:
                    MenuPause menuPause = new MenuPause();
                    menuPause.Stop();
                    break;

                case ConsoleKey.UpArrow:
                    if (map.map[playerPos[0] - 1,playerPos[1]] != '#' && map.map[playerPos[0] - 1, playerPos[1]] != '\u00A9'/*©*/ && map.map[playerPos[0] - 1, playerPos[1]] != '\u00B6'/*¶*/)
                    {
                        if(map.map[playerPos[0] - 1, playerPos[1]] == '\u256C'/*╬*/)
                        {
                            pokeCenter.Interface();
                        }
                        else if(map.map[playerPos[0] - 1, playerPos[1]] == '\u2302'/*⌂*/)
                        {
                            home.Equipe();
                        }
                        else if(map.map[playerPos[0] - 1, playerPos[1]] == '\u00A5'/*¥*/)
                        {

                        }
                        else if(map.map[playerPos[0] - 1, playerPos[1]] == '\u263C'/*☼*/)
                        {
                            map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                            map.map[playerPos[0] - 1, playerPos[1]] = '0';
                            playerPos[0] -= 1;
                        }
                        else if(map.map[playerPos[0] - 1, playerPos[1]] == 'O')
                        {

                        }
                        else
                        {
                            map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                            map.map[playerPos[0] - 1, playerPos[1]] = '0';
                            playerPos[0] -= 1;
                        }
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (map.map[playerPos[0] + 1,playerPos[1]] != '#' && map.map[playerPos[0] + 1, playerPos[1]] != '\u00A9'/*©*/ && map.map[playerPos[0] + 1, playerPos[1]] != '\u00B6'/*¶*/)
                    {
                        if(map.map[playerPos[0] + 1, playerPos[1]] == '\u256C'/*╬*/)
                        {
                            pokeCenter.Interface();
                        }
                        else if(map.map[playerPos[0] + 1, playerPos[1]] == '\u2302'/*⌂*/)
                        {
                            home.Equipe();
                        }
                        else if(map.map[playerPos[0] + 1, playerPos[1]] == '\u00A5'/*¥*/)
                        {

                        }
                        else if(map.map[playerPos[0] + 1, playerPos[1]] == '\u263C'/*☼*/)
                        {
                            map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                            map.map[playerPos[0] + 1, playerPos[1]] = '0';
                            playerPos[0] += 1;
                        }
                        else if(map.map[playerPos[0] + 1, playerPos[1]] == 'O')
                        {

                        }
                        else
                        {
                            map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                            map.map[playerPos[0] + 1, playerPos[1]] = '0';
                            playerPos[0] += 1;
                        }
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (map.map[playerPos[0], playerPos[1] - 1] != '#' && map.map[playerPos[0], playerPos[1] - 1] != '\u00A9'/*©*/ && map.map[playerPos[0], playerPos[1] - 1] != '\u00B6'/*¶*/)
                    {
                        if (map.map[playerPos[0], playerPos[1] - 1] == '\u256C'/*╬*/)
                        {
                            pokeCenter.Interface();
                        }
                        else if (map.map[playerPos[0], playerPos[1] - 1] == '\u2302'/*⌂*/)
                        {
                            home.Equipe();
                        }
                        else if (map.map[playerPos[0], playerPos[1] - 1] == '\u00A5'/*¥*/)
                        {

                        }
                        else if (map.map[playerPos[0], playerPos[1] - 1] == '\u263C'/*☼*/)
                        {
                            map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                            map.map[playerPos[0], playerPos[1] - 1] = '0';
                            playerPos[1] -= 1;
                        }
                        else if (map.map[playerPos[0], playerPos[1] - 1] == 'O')
                        {

                        }
                        else
                        {
                            map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                            map.map[playerPos[0], playerPos[1] - 1] = '0';
                            playerPos[1] -= 1;
                        }
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (map.map[playerPos[0], playerPos[1] + 1] != '#' && map.map[playerPos[0], playerPos[1] + 1] != '\u00A9'/*©*/ && map.map[playerPos[0], playerPos[1] + 1] != '\u00B6'/*¶*/)
                    {
                        if (map.map[playerPos[0], playerPos[1] + 1] == '\u256C'/*╬*/)
                        {
                            pokeCenter.Interface();
                        }
                        else if (map.map[playerPos[0], playerPos[1] + 1] == '\u2302'/*⌂*/)
                        {
                            home.Equipe();
                        }
                        else if (map.map[playerPos[0], playerPos[1] + 1] == '\u00A5'/*¥*/)
                        {

                        }
                        else if (map.map[playerPos[0], playerPos[1] + 1] == '\u263C'/*☼*/)
                        {
                            map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                            map.map[playerPos[0], playerPos[1] + 1] = '0';
                            playerPos[1] += 1;
                        }
                        else if (map.map[playerPos[0], playerPos[1] + 1] == 'O')
                        {

                        }
                        else
                        {
                            map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                            map.map[playerPos[0], playerPos[1] + 1] = '0';
                            playerPos[1] += 1;
                        }
                    }
                    break;

                case ConsoleKey.I:
                    player.Inventory.OpenInventory();
                    break;

                default:
                    break;

            }
            if (map.copy_map[playerPos[0], playerPos[1]] == '"')
            {
                Random random = new Random();
                int random_number = random.Next(1, 7);
                if (random_number == 1)
                {
                    int random_pokemon = random.Next(1, 1061);
                    // Combat entre les deux dresseurs

                    Battle battle = new Battle();
                    int sommeNiveauPokemonPlayer = 0;
                    foreach (var poke in player.Team)
                    {
                        sommeNiveauPokemonPlayer += poke.Level;
                    }
                    int moyenneNiveauxPokemonPlayer = sommeNiveauPokemonPlayer / player.Team.Count;
                    pokemons[random_pokemon].Level = moyenneNiveauxPokemonPlayer;
                    pokemons[random_pokemon].CanLearnNewCapacity();
                    battle.StartBattleVsPokemon(player, pokemons[random_pokemon]);
                  
                    if (!player.TeamIsAlive())
                    {
                        map.map[playerPos[0], playerPos[1]] = map.copy_map[playerPos[0], playerPos[1]];
                        playerPos[0] = 3;
                        playerPos[1] = 4;
                        map.map[playerPos[0], playerPos[1]] = '0';
                    }
                    Console.Clear();
                }
            }
        }
    }
}