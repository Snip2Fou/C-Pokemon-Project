using System;
using System.Diagnostics.Eventing.Reader;

class Battle
{
    private int Mud_Sport { get; set; }
    private int Water_Sport { get; set; }

    public int GetPower(Capacity capacity_attack,Capacity capacity_defense)
    {
        float MS = 1;
        if(Mud_Sport != 0 && capacity_attack.Type == "Electric")
        {
            MS = 0.5f;
        }
        float WS = 1;
        if (Water_Sport != 0 && capacity_attack.Type == "Fire")
        {
            MS = 0.5f;
        }
        float power = capacity_attack.Power * MS * WS;
        return (int)Math.Round(power);
    }

    public int GetAttack(Capacity capacity_attack, Pokemon pokemon)
    {
        int attack = pokemon.Attack;
        if(capacity_attack.Type == "Special")
        {
            attack = pokemon.AttackSpecial;
        }
        return attack;
    }

    public int GetDefense(Capacity capacity_attack, Pokemon pokemon)
    {
        int defense = pokemon.Defense;
        if (capacity_attack.Type == "Special")
        {
            defense = pokemon.DefenseSpecial;
        }
        return defense;
    }

    public void StartBattleVsPokemon(Trainer player, Pokemon pokemon)
    { 
        Mud_Sport = 0;
        Water_Sport = 0;
        Event event_choice = new Event();
        bool fuite = false;
        Console.WriteLine($"{player.Name} VS {pokemon.Name}");
        Console.WriteLine("Choisissez votre action :");
        bool action_selected = false;
        Console.WriteLine("> Selectionnez un Pokemon");
        Console.WriteLine("  Fuire");
        Pokemon activePokemon1 = null;
        while (!action_selected)
        {
            bool choice_event = event_choice.ChoiceEvent(2);
            if (choice_event) {
                action_selected = true;
                if (event_choice.action_count == 0)
                {
                    if (player.Team.Count > 0)
                    {
                        activePokemon1 = ChooseActivePokemon(player);
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
            bool choice_event = event_choice.ChoiceEvent(4);
            if (choice_event)
            {
                if(event_choice.action_count == 0)
                {
                    /*BattleRound(activePokemon1, pokemon);*/
                }
                else if(event_choice.action_count == 1)
                {
                    activePokemon1 = ChooseActivePokemon(player);
                }
                else if (event_choice.action_count == 2)
                {

                }
                else if (event_choice.action_count == 3)
                {
                    fuite = true;
                }
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
                        Pokemon activePokemon1 = ChooseActivePokemon(trainer1);
                        Pokemon activePokemon2 = ChooseActivePokemon(trainer2);

                        // Combat entre les Pokémon actifs
                        /*BattleRound(trainer1, activePokemon1, trainer2, activePokemon2);*/
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

    public void BattleRound(Pokemon activePokemon1, Pokemon activePokemon2)
    {
        Event event_choice = new Event();
        int nb_event = 0;
        Console.WriteLine("Choisissez votre attaque :");
        if(activePokemon1.Capacity1 != null)
        {
            Console.WriteLine($">  {activePokemon1.Capacity1.Name} | {activePokemon1.Capacity1.Type} | {activePokemon1.Capacity1.Category} | {activePokemon1.Capacity1.Category} | {activePokemon1.Capacity1.Power} | {activePokemon1.Capacity1.Accuracy}");
            nb_event++;
        }
        else if(activePokemon1.Capacity2 != null)
        {
            Console.WriteLine($"  {activePokemon1.Capacity2.Name} | {activePokemon1.Capacity2.Type} | {activePokemon1.Capacity1.Category} | {activePokemon1.Capacity2.Power} | {activePokemon1.Capacity2.Accuracy}");
            nb_event++;
        }
        else if (activePokemon1.Capacity3 != null)
        {
            Console.WriteLine($"  {activePokemon1.Capacity3.Name} | {activePokemon1.Capacity3.Type} | {activePokemon1.Capacity3.Category} | {activePokemon1.Capacity3.Power} | {activePokemon1.Capacity3.Accuracy}");
            nb_event++;
        }
        bool choice_event = false;
        while (!choice_event) {
            choice_event = event_choice.ChoiceEvent(nb_event);

            Console.Clear();
            Console.WriteLine("Choisissez votre attaque :");
            if (event_choice.action_count == 0)
            {
                if (activePokemon1.Capacity1 != null)
                {
                    Console.WriteLine($"> {activePokemon1.Capacity1.Name} | {activePokemon1.Capacity1.Type} | {activePokemon1.Capacity1.Category} | {activePokemon1.Capacity1.Power} | {activePokemon1.Capacity1.Accuracy}");
                }
                else if (activePokemon1.Capacity2 != null)
                {
                    Console.WriteLine($"  {activePokemon1.Capacity2.Name} | {activePokemon1.Capacity2.Type} | {activePokemon1.Capacity2.Category} | {activePokemon1.Capacity2.Power} | {activePokemon1.Capacity2.Accuracy}");
                }
                else if (activePokemon1.Capacity3 != null)
                {
                    Console.WriteLine($"  {activePokemon1.Capacity3.Name} | {activePokemon1.Capacity3.Type} | {activePokemon1.Capacity3.Category} | {activePokemon1.Capacity3.Power} | {activePokemon1.Capacity3.Accuracy}");
                }
            }
            else if (event_choice.action_count == 1)
            {
                if (activePokemon1.Capacity1 != null)
                {
                    Console.WriteLine($"  {activePokemon1.Capacity1.Name} | {activePokemon1.Capacity1.Type} | {activePokemon1.Capacity1.Category} | {activePokemon1.Capacity1.Power} | {activePokemon1.Capacity1.Accuracy}");
                }
                else if (activePokemon1.Capacity2 != null)
                {
                    Console.WriteLine($"> {activePokemon1.Capacity2.Name} | {activePokemon1.Capacity2.Type} | {activePokemon1.Capacity2.Category} | {activePokemon1.Capacity2.Power} | {activePokemon1.Capacity2.Accuracy}");
                }
                else if (activePokemon1.Capacity3 != null)
                {
                    Console.WriteLine($"  {activePokemon1.Capacity3.Name} | {activePokemon1.Capacity3.Type} | {activePokemon1.Capacity3.Category} | {activePokemon1.Capacity3.Power} | {activePokemon1.Capacity3.Accuracy}");
                }
            }
            else if (event_choice.action_count == 2)
            {
                Console.WriteLine($"  Attack | Normal | {activePokemon1.Attack} | 100%");
                if (activePokemon1.Capacity1 != null)
                {
                    Console.WriteLine($"  {activePokemon1.Capacity1.Name} | {activePokemon1.Capacity1.Type} | {activePokemon1.Capacity1.Category} | {activePokemon1.Capacity1.Power} | {activePokemon1.Capacity1.Accuracy}");
                }
                else if (activePokemon1.Capacity2 != null)
                {
                    Console.WriteLine($"  {activePokemon1.Capacity2.Name} | {activePokemon1.Capacity2.Type} | {activePokemon1.Capacity2.Category} | {activePokemon1.Capacity2.Power} | {activePokemon1.Capacity2.Accuracy}");
                }
                else if (activePokemon1.Capacity3 != null)
                {
                    Console.WriteLine($"> {activePokemon1.Capacity3.Name} | {activePokemon1.Capacity3.Type} | {activePokemon1.Capacity3.Category} | {activePokemon1.Capacity3.Power} | {activePokemon1.Capacity3.Accuracy}");
                }
            }
        }

        Capacity capacity_random = activePokemon2.Capacity1;

        if(event_choice.action_count == 0)
        {
            int level = activePokemon1.Level;
            int damage = (level * 2 / 5) + 2;
            int power = GetPower(activePokemon1.Capacity1, capacity_random);
            damage *= power;
            int attack = GetAttack(activePokemon1.Capacity1, activePokemon1);
            damage *= attack;
            float damage_temp = damage / 50;
            damage = (int)Math.Round(damage_temp);

              /*  Math.Max(0, activePokemon1.Attack - activePokemon2.Defense);*/
            activePokemon2.TakeDamage(damageToTrainer2);
            /*Console.WriteLine($"{trainer2.Name}'s {activePokemon2.Name} takes {damageToTrainer2} damage!");*/
        }
        else if(event_choice.action_count == 1) 
        {
            float damageToTrainer2 = Math.Max(0, activePokemon1.AttackSpecial - activePokemon2.Defense);
            activePokemon2.TakeDamage(damageToTrainer2);
           /* Console.WriteLine($"{trainer2.Name}'s {activePokemon2.Name} takes {damageToTrainer2} damage!");*/
        }

        float damageToTrainer1 = Math.Max(0, activePokemon2.Attack - activePokemon1.Defense);
        activePokemon1.TakeDamage(damageToTrainer1);

        /*Console.WriteLine($"{trainer1.Name}'s {activePokemon1.Name} takes {damageToTrainer1} damage!");

        // Vérifier si le Pokémon de trainer2 est vaincu
        if (activePokemon2.Health <= 0)
      {
          Console.WriteLine($"{trainer2.Name}'s {activePokemon2.Name} faints!");
          trainer2.Team.Remove(activePokemon2);
      }

      // Vérifier si le Pokémon de trainer1 est vaincu
      if (activePokemon1.Health <= 0)
      {
          Console.WriteLine($"{trainer1.Name}'s {activePokemon1.Name} faints!");
          trainer1.Team.Remove(activePokemon1);
      }*/
    }
}