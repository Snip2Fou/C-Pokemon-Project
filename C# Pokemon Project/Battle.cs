using System;

class Battle
{
    public static void StartBattle(Trainer trainer1, Trainer trainer2)
    {
        bool fuite = false;
        Console.WriteLine($"Battle between {trainer1.Name} and {trainer2.Name} begins!");
        while (trainer1.Team.Count > 0 && trainer2.Team.Count > 0 && fuite != true)
        {
            Console.WriteLine($"{trainer1.Name}, choose your action");
            Console.WriteLine($"A - Select Pokemon / B - Heal Pokemon / C - Fuire");

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.A:
                    while (trainer1.Team.Count > 0 || trainer2.Team.Count > 0)
                    {
                        // Chaque dresseur choisit son Pokémon actif
                        Pokemon activePokemonTrainer1 = ChooseActivePokemon(trainer1);
                        Pokemon activePokemonTrainer2 = ChooseActivePokemon(trainer2);

                        // Combat entre les Pokémon actifs
                        BattleRound(trainer1, activePokemonTrainer1, trainer2, activePokemonTrainer2);
                    }
                    break;
                case ConsoleKey.B:
                    Pokemon healPokemon = ChooseActivePokemon(trainer1);
                    healPokemon.Heal();
                    Console.WriteLine($"heal {healPokemon.Name}");
                    break;
                case ConsoleKey.C:
                    Console.WriteLine("Fuit");
                    fuite = true;
                    break;
            }
            Console.WriteLine(fuite); 
        }

        // Annonce du gagnant
        if (trainer1.Team.Count > 0)
        {
            Console.WriteLine($"{trainer1.Name} wins!");
        }
        else
        {
            Console.WriteLine($"{trainer2.Name} wins!");
        }
    }

    public static Pokemon ChooseActivePokemon(Trainer trainer)
    {
        Console.WriteLine($"{trainer.Name}, choose your active Pokémon:");
        for (int i = 0; i < trainer.Team.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {trainer.Team[i].Name} (Health: {trainer.Team[i].Health})");
        }

        int choice;
        do
        {
            Console.Write("Enter the number of your choice: ");
        } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > trainer.Team.Count);

        return trainer.Team[choice - 1];
    }

    public static void BattleRound(Trainer trainer1, Pokemon activePokemonTrainer1, Trainer trainer2, Pokemon activePokemonTrainer2)
    {

        Console.WriteLine($"{trainer1.Name}, choose the action of your pokemon");
        Console.WriteLine($"W - Attack / X - Attack Special");

        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

        switch (consoleKeyInfo.Key)
        {
            case ConsoleKey.W:
                float damageToTrainer2 = Math.Max(0, activePokemonTrainer1.Attack - activePokemonTrainer2.Defense);
                activePokemonTrainer2.TakeDamage(damageToTrainer2);
                Console.WriteLine($"{trainer2.Name}'s {activePokemonTrainer2.Name} takes {damageToTrainer2} damage!");
                break;
            case ConsoleKey.X:
                damageToTrainer2 = Math.Max(0, activePokemonTrainer1.AttackSpecial - activePokemonTrainer2.Defense);
                activePokemonTrainer2.TakeDamage(damageToTrainer2);
                Console.WriteLine($"{trainer2.Name}'s {activePokemonTrainer2.Name} takes {damageToTrainer2} damage!");
                break;
        }

        float damageToTrainer1 = Math.Max(0, activePokemonTrainer2.Attack - activePokemonTrainer1.Defense);
        activePokemonTrainer1.TakeDamage(damageToTrainer1);

        Console.WriteLine($"{trainer1.Name}'s {activePokemonTrainer1.Name} takes {damageToTrainer1} damage!");

        // Vérifier si le Pokémon de trainer2 est vaincu
        if (activePokemonTrainer2.Health <= 0)
      {
          Console.WriteLine($"{trainer2.Name}'s {activePokemonTrainer2.Name} faints!");
          trainer2.Team.Remove(activePokemonTrainer2);
      }

      // Vérifier si le Pokémon de trainer1 est vaincu
      if (activePokemonTrainer1.Health <= 0)
      {
          Console.WriteLine($"{trainer1.Name}'s {activePokemonTrainer1.Name} faints!");
          trainer1.Team.Remove(activePokemonTrainer1);
      }
    }
}