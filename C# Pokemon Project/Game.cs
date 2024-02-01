using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

class Game
{
    public bool isRunning = true;
    protected Map map = new Map();
    protected int[] playerPos = new int[2];

    public Game()
    {
        playerPos[0] = map.size_x / 2;
        playerPos[1] = map.size_y / 2;
        map.map.SetValue('0', playerPos[0], playerPos[1]);
        map.Draw();
    }

    public void GameLoop()
    {
        string filePath = "C:\\Users\\ddeoliveira\\Desktop\\C-Pokemon-Project\\pokemon.csv";

        List<Pokemon> pokemons = new List<Pokemon>();

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    Pokemon pokemon = new Pokemon(values[1], values[2], values[3], int.Parse(values[4]), int.Parse(values[5]), int.Parse(values[6]),
                        int.Parse(values[7]), int.Parse(values[8]), int.Parse(values[9]), int.Parse(values[10]), int.Parse(values[11]), bool.Parse(values[12]));

                    pokemons.Add(pokemon);
                }
            }
        }   
        else
        {
            Console.WriteLine("Le fichier n'existe pas.");
        }

        // Création de deux dresseurs
        Trainer ash = new Trainer("Ash");
        Trainer gary = new Trainer("Gary");

        /*ash.AddPokemon("Kakuna");
        gary.AddPokemon("Metapod");*/


        while (isRunning)
        {

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.Clear();

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Escape:
                    isRunning = false;
                    break;

                case ConsoleKey.UpArrow:
                    if (playerPos[0] - 1 > 0)
                    {
                        if(playerPos[0] - 1 == 1 && playerPos[1] == 35)
                        {
                            Console.WriteLine("PNJ");
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
                    // Combat entre les deux dresseurs
                    Battle.StartBattle(ash, gary);
                }
            }
             map.Draw();
        }
    }
}