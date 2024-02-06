using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading;


public class Game
{
    private static Game instance;

    public bool isRunning = true;
    protected Map map = new Map();
    protected int[] playerPos = new int[2];
    public List<Pokemon> pokemons = new List<Pokemon>();
    public List<string> type_list = new List<string>();
    public Dictionary<string, Dictionary<string, List<double>>> type_chart = new Dictionary<string, Dictionary<string, List<double>>>();
    protected Inventory inventory = new Inventory();
    protected Object potion = new Object("potion de soin", "ajoute 5 point de vie au pokemon");
    protected Object pokeball = new Object("pokeball", "chance de capature de certain pokemon");
    protected Pokemon poke = new Pokemon("poketest");
    string namePlayer;

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
                                  for(int i = 3; i < values_all_capacity.Length; i++)
                                  {
                                      if (values_all_capacity[i].Contains("Start"))
                                      {
                                          bool pokemon_capacity_found = false;
                                          string[] parts = values_all_capacity[i].Split('-');
                                          if (File.Exists(filePathCapacity))
                                          {
                                              using (StreamReader readerCapacity = new StreamReader(filePathCapacity))
                                              {
                                                  readerCapacity.ReadLine();

                                                  while (!pokemon_capacity_found && !readerCapacity.EndOfStream)
                                                  {
                                                      string line_capacity = readerCapacity.ReadLine();
                                                      string[] values_capacity = line_capacity.Split(',');

                                                      if (parts[1].Contains(values_capacity[1]))
                                                      {
                                                          pokemon.CreateCapacity(values_capacity, type_list);
                                                          pokemon_capacity_found = true;
                                                      }
                                                  }
                                              }
                                          }
                                          else
                                          {
                                              Console.WriteLine("Le fichier n'existe pas.");
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
        
      playerPos[0] = map.size_x / 2;
      playerPos[1] = map.size_y / 2;
      map.map.SetValue('0', playerPos[0], playerPos[1]);
      map.Draw();
      pokemons.Add(poke);
    }

    public void SaveGame()
    {
        SaveData game_save = new SaveData(namePlayer, pokemons, inventory.SaveInventory(), playerPos);
        string jsonData = JsonConvert.SerializeObject(game_save);
        File.WriteAllText("Save/game_save.json", jsonData);
        Console.WriteLine("sauvegarde");
    }

    public void LoadGame()
    {
        try
        {
            string jsonData = File.ReadAllText("Save/game_save.json");
            SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonData);
            namePlayer = data.NamePlayer;
            pokemons = data.Pokemons;
            foreach(var obj in data.Inventory)
            {
                inventory.AddObject(obj);
            }
            Console.WriteLine("Partie chargée");
        }
        catch (FieldAccessException) 
        {
            Console.WriteLine("Aucun sauvegarde trouvé");
        }
        catch (JsonException)
        {
            Console.WriteLine("Erreur lors de la lecture du fichier de sauvegarde");
        }
    }

    public void GameLoop()
    {
        Console.WriteLine("entre votre pseudo :");
        namePlayer = Console.ReadLine();

        // Création de deux dresseurs
        Trainer player = new Trainer(namePlayer);
        Trainer gary = new Trainer("Gary");

        inventory.AddObject(potion);
        inventory.AddObject(potion);
        inventory.AddObject(pokeball);
        inventory.DisplayInventory();

        while (isRunning)
        {

            Trainer ash = new Trainer("Ash");
            ash.AddPokemon(pokemons[0]);
            ash.AddPokemon(pokemons[52]);

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.Clear();

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Escape:
                    MenuPause menuPause = new MenuPause(this);
                    menuPause.Stop();
                    break;

                case ConsoleKey.S:
                    SaveGame();
                    break;

                case ConsoleKey.K:
                    LoadGame();
                    break;

                case ConsoleKey.UpArrow:
                    if (playerPos[0] - 1 > 0)
                    {
                        if(playerPos[0] - 1 == 1 && playerPos[1] == 35)
                        {
                            Console.WriteLine("PNJ");
                            if (map.npc.ennemy)
                            {
                                map.npc.NpcEnnemy(player);
                            }
                            else
                            {
                                map.npc.NpcHelp();
                            }
                        }
                        else
                        {
                            if ((playerPos[0] >= 10 && playerPos[0] <= 13 && playerPos[1] >= 10 && playerPos[1] <= 13) || (playerPos[0] >= 1 && playerPos[0] <= 6 && playerPos[1] >= 40 && playerPos[1] <= 48) || (playerPos[0] >= 12 && playerPos[0] <= 14 && playerPos[1] >= 28 && playerPos[1] <= 34) || (playerPos[0] >= 1 && playerPos[0] <= 4 && playerPos[1] >= 15 && playerPos[1] <= 17) || (playerPos[0] >= 3 && playerPos[0] <= 4 && playerPos[1] >= 18 && playerPos[1] <= 19))
                            {
                                map.map[playerPos[0], playerPos[1]] = '"';
                                map.map[playerPos[0] - 1, playerPos[1]] = '0';

                            }
                            else
                            {
                                map.map[playerPos[0], playerPos[1]] = ' ';
                                map.map[playerPos[0] - 1, playerPos[1]] = '0';
                            }
                            playerPos[0] -= 1;
                        }
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (playerPos[0] + 1 < map.size_x - 1)
                    {
                        if (playerPos[0] + 1 == 1 && playerPos[1] == 35)
                        {
                            Console.WriteLine("PNJ");
                            if (map.npc.ennemy)
                            {
                                map.npc.NpcEnnemy(player);
                            }
                            else
                            {
                                map.npc.NpcHelp();
                            }
                        }
                        else
                        {
                            if ((playerPos[0] >= 10 && playerPos[0] <= 13 && playerPos[1] >= 10 && playerPos[1] <= 13) || (playerPos[0] >= 1 && playerPos[0] <= 6 && playerPos[1] >= 40 && playerPos[1] <= 48) || (playerPos[0] >= 12 && playerPos[0] <= 14 && playerPos[1] >= 28 && playerPos[1] <= 34) || (playerPos[0] >= 1 && playerPos[0] <= 4 && playerPos[1] >= 15 && playerPos[1] <= 17) || (playerPos[0] >= 3 && playerPos[0] <= 4 && playerPos[1] >= 18 && playerPos[1] <= 19))
                            {
                                map.map[playerPos[0], playerPos[1]] = '"';
                                map.map[playerPos[0] + 1, playerPos[1]] = '0';

                            }
                            else
                            {
                                map.map[playerPos[0], playerPos[1]] = ' ';
                                map.map[playerPos[0] + 1, playerPos[1]] = '0';
                            }
                            playerPos[0] += 1;
                        }
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (playerPos[1] - 1 > 0)
                    {
                        if (playerPos[0] == 1 && playerPos[1] - 1 == 35)
                        {
                            Console.WriteLine("PNJ");
                            if (map.npc.ennemy)
                            {
                                map.npc.NpcEnnemy(player);
                            }
                            else
                            {
                                map.npc.NpcHelp();
                            }
                        }
                        else
                        {
                            if ((playerPos[0] >= 10 && playerPos[0] <= 13 && playerPos[1] >= 10 && playerPos[1] <= 13) || (playerPos[0] >= 1 && playerPos[0] <= 6 && playerPos[1] >= 40 && playerPos[1] <= 48) || (playerPos[0] >= 12 && playerPos[0] <= 14 && playerPos[1] >= 28 && playerPos[1] <= 34) || (playerPos[0] >= 1 && playerPos[0] <= 4 && playerPos[1] >= 15 && playerPos[1] <= 17) || (playerPos[0] >= 3 && playerPos[0] <= 4 && playerPos[1] >= 18 && playerPos[1] <= 19))
                            {
                                map.map[playerPos[0], playerPos[1]] = '"';
                                map.map[playerPos[0], playerPos[1] - 1] = '0';

                            }
                            else
                            {
                                map.map[playerPos[0], playerPos[1]] = ' ';
                                map.map[playerPos[0], playerPos[1] - 1] = '0';
                            }
                            playerPos[1] -= 1;
                        }
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (playerPos[1] + 1 < map.size_y - 1)
                    {
                        if (playerPos[0] == 1 && playerPos[1] + 1 == 35)
                        {
                            Console.WriteLine("PNJ");
                            if (map.npc.ennemy)
                            {
                                map.npc.NpcEnnemy(player);
                            }
                            else
                            {
                                map.npc.NpcHelp();
                            }
                        }
                        else
                        {
                            if ((playerPos[0] >= 10 && playerPos[0] <= 13 && playerPos[1] >= 10 && playerPos[1] <= 13) || (playerPos[0] >= 1 && playerPos[0] <= 6 && playerPos[1] >= 40 && playerPos[1] <= 48) || (playerPos[0] >= 12 && playerPos[0] <= 14 && playerPos[1] >= 28 && playerPos[1] <= 34) || (playerPos[0] >= 1 && playerPos[0] <= 4 && playerPos[1] >= 15 && playerPos[1] <= 17) || (playerPos[0] >= 3 && playerPos[0] <= 4 && playerPos[1] >= 18 && playerPos[1] <= 19))
                            {
                                map.map[playerPos[0], playerPos[1]] = '"';
                                map.map[playerPos[0], playerPos[1] + 1] = '0';

                            }
                            else
                            {
                                map.map[playerPos[0], playerPos[1]] = ' ';
                                map.map[playerPos[0], playerPos[1] + 1] = '0';
                            }
                            playerPos[1] += 1;
                        }
                    }
                    break;


                default:
                    break;

            }
            if ((playerPos[0] >= 10 && playerPos[0] <= 13 && playerPos[1] >= 10 && playerPos[1] <= 13) || (playerPos[0] >= 1 && playerPos[0] <= 6 && playerPos[1] >= 40 && playerPos[1] <= 48) || (playerPos[0] >= 12 && playerPos[0] <= 14 && playerPos[1] >= 28 && playerPos[1] <= 34) || (playerPos[0] >= 1 && playerPos[0] <= 4 && playerPos[1] >= 15 && playerPos[1] <= 17) || (playerPos[0] >= 3 && playerPos[0] <= 4 && playerPos[1] >= 18 && playerPos[1] <= 19))
            {
                Random random = new Random();
                int random_number = random.Next(1, 7);
                if (random_number == 1)
                {
                    int random_pokemon = random.Next(1, 1061);
                    // Combat entre les deux dresseurs

                    Battle battle = new Battle(); 
                    battle.StartBattleVsPokemon(ash, pokemons[random_pokemon]);

                    Console.Clear();
                }
            }
            map.Draw();
        }
    }
}