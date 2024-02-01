using System;
using System.Diagnostics.Eventing.Reader;

class Battle
{

    public static void StartBattleVsPokemon(Trainer player, Pokemon pokemon)
    { 
        Event event_choice = new Event();
        bool fuite = false;
        Console.WriteLine($"{player.Name} VS {pokemon.Name}");
        Console.WriteLine("Choisissez votre action :");
        bool action_selected = false;
        Console.WriteLine("> Selectionnez un Pokemon");
        Console.WriteLine("  Fuire");
        while (!action_selected)
        {
            bool choice_event = event_choice.ChoiceEvent(2);
            if (choice_event) {
                action_selected = true;
                if (event_choice.action_count == 0)
                {
                    if (player.Team.Count > 0)
                    {
                        Pokemon activePokemonTrainer1 = ChooseActivePokemon(player);
                    }
                }
                else if (event_choice.action_count == 1) {
                    fuite = true;
                }
            }
            Console.Clear();
            Console.WriteLine("Choisissez votre action :");
            if (event_choice.action_count == 0)
            {
                Console.WriteLine("> Selectionnez un Pokemon");
                Console.WriteLine("  Fuire");
            }
            else if (event_choice.action_count == 1)
            {
                Console.WriteLine("  Selectionnez un Pokemon");
                Console.WriteLine("> Fuire");
            }
        }
        Console.Clear();
        Console.WriteLine("Choisissez votre action :");
        Console.WriteLine("> Attaquer");
        Console.WriteLine("  Changer de Pokemon");
        Console.WriteLine("  Utiliser un objet");
        Console.WriteLine("  Fuire");
        event_choice.action_count = 0;
        while (player.Team.Count > 0 && pokemon.IsAlive() && fuite != true)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (event_choice.action_count - 1 < 0)
                    {
                        event_choice.action_count = 3;
                    }
                    else
                    {
                        event_choice.action_count--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (event_choice.action_count + 1 == 4)
                    {
                        event_choice.action_count = 0;
                    }
                    else
                    {
                        event_choice.action_count++;
                    }
                    break;

                case ConsoleKey.Enter:
                    if(event_choice.action_count == 0)
                    {

                    }
                    event_choice.action_count = 0;
                    break;

                default:
                    break;
            }
            Console.Clear();
            Console.WriteLine("Choisissez votre action :");
            if (event_choice.action_count == 0)
            {
                Console.WriteLine("> Attaquer");
                Console.WriteLine("  Changer de Pokemon");
                Console.WriteLine("  Utiliser un objet");
                Console.WriteLine("  Fuire");
            }
            else if (event_choice.action_count == 1)
            {
                Console.WriteLine("  Attaquer");
                Console.WriteLine("> Changer de Pokemon");
                Console.WriteLine("  Utiliser un objet");
                Console.WriteLine("  Fuire");
            }
            else if (event_choice.action_count == 2)
            {
                Console.WriteLine("  Attaquer");
                Console.WriteLine("  Changer de Pokemon");
                Console.WriteLine("> Utiliser un objet");
                Console.WriteLine("  Fuire");
            }
            else if (event_choice.action_count == 3)
            {
                Console.WriteLine("  Attaquer");
                Console.WriteLine("  Changer de Pokemon");
                Console.WriteLine("  Utiliser un objet");
                Console.WriteLine("> Fuire");
            }
        }
    }
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
        Console.WriteLine("Choisissez votre Pokemon actif :");
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