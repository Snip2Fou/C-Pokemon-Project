using System;

/*class Program
{
    static void Main()
    {
        // Création de quelques Pokémon
        Pokemon pikachu = new Pokemon("Pikachu", 20, 10, 5);
        Pokemon bulbasaur = new Pokemon("Bulbasaur", 25, 8, 5);
        Pokemon chu = new Pokemon("Chu", 20, 12, 5);
        Pokemon saur = new Pokemon("Saur", 25, 13, 5);

        // Création de deux dresseurs
        Trainer ash = new Trainer("Ash");
        Trainer gary = new Trainer("Gary");

        // Dresseurs ajoutent des Pokémon à leur équipe
        ash.AddPokemon(pikachu);
        gary.AddPokemon(bulbasaur);
        ash.AddPokemon(chu);
        gary.AddPokemon(saur);

        // Combat entre les deux dresseurs
        Battle.StartBattle(ash, gary);
    }
}*/

/*class Battle
{
    public static void StartBattle(Trainer trainer1, Trainer trainer2)
    {
        Console.WriteLine($"Battle between {trainer1.Name} and {trainer2.Name} begins!");

        // Boucle de combat
        while (trainer1.Team.Count > 0 || trainer2.Team.Count > 0)
        {
            // Chaque dresseur choisit son Pokémon actif
            Pokemon activePokemonTrainer1 = ChooseActivePokemon(trainer1);
            Pokemon activePokemonTrainer2 = ChooseActivePokemon(trainer2);

            // Combat entre les Pokémon actifs
            BattleRound(trainer1, activePokemonTrainer1, trainer2, activePokemonTrainer2);
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
        Console.WriteLine($"{trainer1.Name}'s {activePokemonTrainer1.Name} vs {trainer2.Name}'s {activePokemonTrainer2.Name}");
        Console.WriteLine($"{trainer1.Name}, choose your active :");
        Console.WriteLine($"1. attaque / 2. Soin / 3. Fuite");

        int choice = !int.TryParse(Console.ReadLine(), out choice);

        switch (choice)
        {
            case 1:
                int damageToTrainer2 = Math.Max(0, activePokemonTrainer1.Attack - activePokemonTrainer2.Defense);
                activePokemonTrainer2.TakeDamage(damageToTrainer2);
                Console.WriteLine($"{trainer2.Name}'s {activePokemonTrainer2.Name} takes {damageToTrainer2} damage!");
                break;
            case 2:
                Console.WriteLine($"use popo");
                break;
            case 3:
                Console.WriteLine($"fuit");
                break;
        }

        Console.WriteLine($"{trainer2.Name}, choose your active :");
        Console.WriteLine($"1. attaque / 2. Soin / 3. Fuite");

        int choice = Console.ReadLine();

        switch (choice)
        {
            case 1:
                int damageToTrainer1 = Math.Max(0, activePokemonTrainer2.Attack - activePokemonTrainer1.Defense);
                activePokemonTrainer1.TakeDamage(damageToTrainer1);
                Console.WriteLine($"{trainer1.Name}'s {activePokemonTrainer1.Name} takes {damageToTrainer1} damage!");
                break;
            case 2:
                Console.WriteLine($"use popo");
                break;
            case 3:
                Console.WriteLine($"fuit");
                break;
        }*/

        /*// Logique du combat (simplifiée)
        int damageToTrainer2 = Math.Max(0, activePokemonTrainer1.Attack - activePokemonTrainer2.Defense);
        activePokemonTrainer2.TakeDamage(damageToTrainer2);
        Console.WriteLine($"{trainer2.Name}'s {activePokemonTrainer2.Name} takes {damageToTrainer2} damage!");

        int damageToTrainer1 = Math.Max(0, activePokemonTrainer2.Attack - activePokemonTrainer1.Defense);
        activePokemonTrainer1.TakeDamage(damageToTrainer1);

        Console.WriteLine($"{trainer1.Name}'s {activePokemonTrainer1.Name} takes {damageToTrainer1} damage!");*/

        // Vérifier si le Pokémon de trainer2 est vaincu
      /*  if (activePokemonTrainer2.Health <= 0)
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
}*/