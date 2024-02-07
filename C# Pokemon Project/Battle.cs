using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

public class Battle
{
    private int Mud_Sport { get; set; }
    private int Water_Sport { get; set; }
    private int Nb_Fuite { get; set; }
    private Trainer PlayerBattle { get; set; }
    private Pokemon PokemonBattle { get; set; }
    private Trainer EnemyBattle { get; set; }
    private Pokemon ActivePokemon1 { get; set; }
    private Pokemon ActivePokemon2 { get; set; }
    private string NextAction1 { get; set; }
    private string NextAction2 { get; set; }


    public int GetDamage(Pokemon attack_pokemon, Capacity attack_capacity, Pokemon defense_pokemon)
    {
        int level = attack_pokemon.Level;
        int damage = (level * 2 / 5) + 2;
        int power = GetPower(attack_capacity);
        damage *= power;
        int attack = GetAttack(attack_capacity, attack_pokemon);
        damage *= attack;
        float damage_temp = damage / 50;
        damage = (int)Math.Round(damage_temp);
        damage_temp = damage / GetDefense(attack_capacity, defense_pokemon);
        damage = (int)Math.Round(damage_temp);
        damage += 2;
        damage *= GetCritical(attack_capacity);
        damage *= GetRandom();
        damage_temp = damage / 100;
        damage = (int)Math.Round(damage_temp);
        damage_temp = damage * GetStab(attack_capacity, attack_pokemon);
        damage = (int)Math.Round(damage_temp);
        double damage_temp2 = damage * GetType(attack_capacity, defense_pokemon);
        damage = (int)Math.Round(damage_temp2);
        return Math.Max(1, damage);
    }
    public int GetPower(Capacity capacity_attack)
    {
        float MS = 1;
        if (Mud_Sport != 0 && capacity_attack.Type == "Electric")
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
        int attack = pokemon.GetStatByFormule(pokemon.Attack);
        if (capacity_attack.Type == "Special")
        {
            attack = pokemon.GetStatByFormule(pokemon.AttackSpecial);
        }
        return attack;
    }

    public int GetDefense(Capacity capacity_attack, Pokemon pokemon)
    {
        int defense = pokemon.GetStatByFormule(pokemon.Defense);
        if (capacity_attack.Type == "Special")
        {
            defense = pokemon.GetStatByFormule(pokemon.DefenseSpecial);
        }
        return defense;
    }

    public int GetCritical(Capacity capacity_attack)
    {
        int critical = 1;
        if (capacity_attack.Critical)
        {
            Random R = new Random();
            if (R.Next(1, 4) == 1)
            {
                critical = 2;
            }
        }
        else
        {
            Random R = new Random();
            if (R.Next(1, 16) == 1)
            {
                critical = 2;
            }
        }
        return critical;
    }

    public int GetRandom()
    {
        Random R = new Random();
        int random = (R.Next(217, 255) * 100) / 255;
        return random;
    }

    public float GetStab(Capacity capacity_attack, Pokemon pokemon)
    {
        float stab = 1;
        if (capacity_attack.Type.Equals(pokemon.TypeOne) || capacity_attack.Type.Equals(pokemon.TypeTwo))
        {
            stab = 1.5f;
        }
        return stab;
    }

    public double GetType(Capacity capacity_attack, Pokemon pokemon)
    {
        int index_type = Game.Instance.type_list.IndexOf(capacity_attack.Type.ToLower());
        return Game.Instance.type_chart[pokemon.TypeOne.ToLower()][pokemon.TypeTwo.ToLower()][index_type];
    }

    public string UseCapacity(Pokemon pokemon_attack, Capacity capacity_attack, Pokemon pokemon_defense)
    {
        Nb_Fuite = 0;
        if (capacity_attack.Category == "Status")
        {
            return "";
        }
        else
        {
            int damage = GetDamage(pokemon_attack, capacity_attack, pokemon_defense);
            pokemon_defense.TakeDamage(damage);
            return  $"{pokemon_attack.Name} utilise {capacity_attack.Name} et inflige {damage} Ã  {pokemon_defense} !";
        }
    }

    public bool GetCanEscape()
    {
        Nb_Fuite++;
        double enemy_speed = ActivePokemon2.Speed / 4;
        int b = (int)Math.Round(enemy_speed);
        int fuite = (((ActivePokemon1.Speed * 32) / b) + 30) * Nb_Fuite;
        if (b == 0 || fuite > 255)
        {
            return true;
        }
        else
        {
            Random R = new Random();
            if (R.Next(0, 255) <= fuite)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public Capacity GetCapacityRandom()
    {
        
        if (EnemyBattle != null || ActivePokemon2.Level > 25)
        {
            List<Capacity> validCapacity = new List<Capacity>();
            double efficacity1 = 0;
            double efficacity2 = 0;
            double efficacity3 = 0;
            if (ActivePokemon2.Capacity1 != null)
            {
                int index_type1 = Game.Instance.type_list.IndexOf(ActivePokemon2.Capacity1.Type.ToLower());
                efficacity1 = Game.Instance.type_chart[ActivePokemon1.TypeOne.ToLower()][ActivePokemon1.TypeTwo.ToLower()][index_type1];
                validCapacity[0] = ActivePokemon2.Capacity1;
            }
            if (ActivePokemon2.Capacity2 != null)
            {
                int index_type2 = Game.Instance.type_list.IndexOf(ActivePokemon2.Capacity2.Type.ToLower());
                efficacity2 = Game.Instance.type_chart[ActivePokemon1.TypeOne.ToLower()][ActivePokemon1.TypeTwo.ToLower()][index_type2];
                if (efficacity2 > efficacity1)
                {
                    validCapacity[0] = ActivePokemon2.Capacity2;
                }
                else if (efficacity2 == efficacity1)
                {
                    validCapacity[1] = ActivePokemon2.Capacity2;
                }
            }
            if (ActivePokemon2.Capacity3 != null)
            {
                int index_type3 = Game.Instance.type_list.IndexOf(ActivePokemon2.Capacity3.Type.ToLower());
                efficacity3 = Game.Instance.type_chart[ActivePokemon1.TypeOne.ToLower()][ActivePokemon1.TypeTwo.ToLower()][index_type3];
                if (validCapacity[0] == ActivePokemon2.Capacity1)
                {
                    if (efficacity3 > efficacity1)
                    {
                        validCapacity.Clear();
                        validCapacity[0] = ActivePokemon2.Capacity3;
                    }
                    else if (efficacity3 == efficacity1)
                    {
                        if (validCapacity.Count == 2)
                        {
                            validCapacity[2] = ActivePokemon2.Capacity3;
                        }
                        else
                        {
                            validCapacity[1] = ActivePokemon2.Capacity3;
                        }
                    }
                }
                else
                {
                    if (efficacity3 > efficacity2)
                    {
                        validCapacity[0] = ActivePokemon2.Capacity3;
                    }
                    else if (efficacity3 == efficacity2)
                    {
                        validCapacity[1] = ActivePokemon2.Capacity3;
                    }
                }
            }

            Random random = new Random();
            return validCapacity[random.Next(validCapacity.Count)];
        }
        else
        {
            Random random = new Random();
            List<Capacity> validCapacity = new List<Capacity>();
            if (ActivePokemon2.Capacity1 != null)
            {
                validCapacity.Add(ActivePokemon2.Capacity1);
            }
            else if(ActivePokemon2.Capacity2 != null)
            {
                validCapacity.Add(ActivePokemon2.Capacity2);
            }
            else if( ActivePokemon2.Capacity3 != null)
            {
                validCapacity.Add(ActivePokemon2.Capacity3);
            }
            return validCapacity[random.Next(validCapacity.Count)];
        }
    }

    public void AffichageVs()
    {
        string versus = "";
        for(int i = 0; i < 20-PlayerBattle.Name.Length; i++) 
        {
            versus += ' ';
        }
        versus += "*** ";
        Console.Write(versus);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{PlayerBattle.Name}");
        Console.ResetColor();
        Console.Write(" VS ");
        Console.ForegroundColor = ConsoleColor.Red;
        if (PokemonBattle == null)
        {
            Console.Write($"{EnemyBattle.Name}");
        } 
        else
        {
            Console.Write($"{PokemonBattle.Name}");
        }
        Console.ResetColor();
        Console.WriteLine(" ***");
        Console.WriteLine();
    }

    public void AffichageHUD()
    {
        string pokemon_name = "     ";
        int size_pokemon_name = ActivePokemon1.Name.Length;
        int size_pokemon_name_space = (15 - ActivePokemon1.Name.Length) /2;
        for (int i = 0; i < size_pokemon_name_space; i++)
        {
            pokemon_name += " ";
            size_pokemon_name++;
        }
        Console.Write(pokemon_name);
        Console.Write(ActivePokemon1.Name);
        pokemon_name = "";
        for (int i = 0; i < 15 - size_pokemon_name; i++)
        {
            pokemon_name += " ";
        }
        Console.Write(pokemon_name);
        pokemon_name = "";
        int size_space = 6 + PlayerBattle.Name.Length;
        if(PokemonBattle == null)
        {
            size_space += EnemyBattle.Name.Length;
        }
        else
        {
            size_space += PokemonBattle.Name.Length;
        }
        for (int i = 0; i < size_space; i++)
        {
            pokemon_name += " ";
        }
        Console.Write(pokemon_name);
        pokemon_name = "";
        size_pokemon_name_space = (15 - ActivePokemon2.Name.Length) / 2;
        for (int i = 0; i < size_pokemon_name_space; i++)
        {
            pokemon_name += " ";
        }
        Console.Write(pokemon_name);
        Console.WriteLine(ActivePokemon2.Name);
        string lifebar = "     ";
        double nb_pv = (ActivePokemon1.Pv * 15 / ActivePokemon1.PvMax);
        for( int i = 0; i < nb_pv; i++ )
        {
            lifebar += "\u2588";
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(lifebar);
        lifebar = "";
        float nb_pv_moins = ((ActivePokemon1.PvMax - ActivePokemon1.Pv) * 15) / ActivePokemon1.PvMax;
        for (int i = 0; i < nb_pv_moins; i++)
        {
            lifebar += "\u2588";
        }
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(lifebar);
        pokemon_name = "";
        for (int i = 0;i < size_space; i++)
        {
            pokemon_name += " ";
        }
        Console.Write(pokemon_name);
        lifebar = "";
        nb_pv = (ActivePokemon2.Pv * 15 / ActivePokemon2.PvMax) ;
        for (int i = 0; i < nb_pv; i++)
        {
            lifebar += "\u2588";
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(lifebar);
        lifebar = "";
        
        nb_pv_moins = ((ActivePokemon2.PvMax - ActivePokemon2.Pv) * 15 ) / ActivePokemon2.PvMax;
        for (int i = 0; i < nb_pv_moins; i++)
        {
            lifebar += "\u2588";
        }
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(lifebar);
        Console.ResetColor();
        pokemon_name = "     ";
        Console.Write(pokemon_name);
        string space = "";
        int size_pv = (15 - (6 + ActivePokemon1.Pv.ToString().Length + ActivePokemon1.PvMax.ToString().Length)) / 2;
        for (int i = 0; i < size_pv; i++)
        {
            space += " ";
        }
        Console.Write(space);
        space = "";
        Console.Write($"{ActivePokemon1.Pv} / {ActivePokemon1.PvMax} PV");
        size_pv = (15 - (6 + ActivePokemon1.Pv.ToString().Length + ActivePokemon1.PvMax.ToString().Length)) - size_pv + size_space;
        for (int i = 0; i < size_pv; i++)
        {
            space += " ";
        }
        Console.Write(space);
        space = "";
        size_pv = (15 - (6 + ActivePokemon2.Pv.ToString().Length + ActivePokemon2.PvMax.ToString().Length)) / 2;
        for (int i = 0; i < size_pv; i++)
        {
            space += " ";
        }
        Console.Write(space);
        Console.WriteLine($"{ActivePokemon2.Pv} / {ActivePokemon2.PvMax} PV");
        Console.WriteLine();
    }

    public void StartBattleVsPokemon(Trainer player, Pokemon pokemon)
    { 
        Mud_Sport = 0;
        Water_Sport = 0;
        player.BattleTeam = player.Team.GetRange(0,player.Team.Count);
        Event event_choice = new Event();
        bool fuite = false;
        PlayerBattle = player;
        PokemonBattle = pokemon;
        ActivePokemon2 = pokemon;
        Pokemon activePokemon1 = ChooseActivePokemon(player);
        event_choice.action_count = 0;
        while (player.BattleTeam.Count > 0 && pokemon.IsAlive() && fuite != true)
        {
            Console.Clear();
            AffichageVs();
            AffichageHUD();
            if (NextAction1 != null)
            {
                Console.WriteLine($"{NextAction1}\n");
            }
            if (NextAction2 != null)
            {
                Console.WriteLine($"{NextAction2}\n");
            }
            Console.WriteLine("Choisissez votre action :");
            if (event_choice.action_count == 0)
            {
                Console.WriteLine("> Attaquer");
                Console.WriteLine("  Changer de Pokemon");
                Console.WriteLine("  Utiliser un objet du sac");
                Console.WriteLine("  Fuir");
            }
            else if (event_choice.action_count == 1)
            {
                Console.WriteLine("  Attaquer");
                Console.WriteLine("> Changer de Pokemon");
                Console.WriteLine("  Utiliser un objet du sac");
                Console.WriteLine("  Fuir");
            }
            else if (event_choice.action_count == 2)
            {
                Console.WriteLine("  Attaquer");
                Console.WriteLine("  Changer de Pokemon");
                Console.WriteLine("> Utiliser un objet du sac");
                Console.WriteLine("  Fuir");
            }
            else if (event_choice.action_count == 3)
            {
                Console.WriteLine("  Attaquer");
                Console.WriteLine("  Changer de Pokemon");
                Console.WriteLine("  Utiliser un objet du sac");
                Console.WriteLine("> Fuir");
            }
            bool choice_event = event_choice.ChoiceEvent(4);
            if (choice_event)
            {
                if (event_choice.action_count == 0)
                {
                    BattleRound(activePokemon1, pokemon);
                    if(!activePokemon1.IsAlive() && player.BattleTeam.Count > 0)
                    {
                        activePokemon1 = ChooseActivePokemon(player);
                    }
                }
                else if (event_choice.action_count == 1)
                {
                    activePokemon1 = ChooseActivePokemon(player);
                }
                else if (event_choice.action_count == 2)
                {

                }
                else if (event_choice.action_count == 3)
                {
                    fuite = GetCanEscape();
                }
            }
        }
    }

    public Pokemon ChooseActivePokemon(Trainer trainer)
    {
        bool first = false;
        int nb_event = 0;
        Console.Clear();
        AffichageVs();
        Console.WriteLine("Choisissez votre Pokemon actif :");
        Console.WriteLine("  Nom | Niveau | Type1 | Type2 | Pv/PvMax | Attack | Defense | AttackSpecial | DefenseSpecial");
        for (int i = 0; i < trainer.BattleTeam.Count; i++)
        {
            if(!first && trainer.BattleTeam[i].IsAlive())
            {
                Console.WriteLine($"> {trainer.BattleTeam[i].Name} | {trainer.BattleTeam[i].Level} | {trainer.BattleTeam[i].TypeOne} | {trainer.BattleTeam[i].TypeTwo} | {trainer.BattleTeam[i].Pv} / {trainer.BattleTeam[i].PvMax} PV | {trainer.BattleTeam[i].Attack} | {trainer.BattleTeam[i].Defense} | {trainer.BattleTeam[i].AttackSpecial} | {trainer.BattleTeam[i].DefenseSpecial}");
                first = true;
                nb_event++;
            }
            else if(trainer.BattleTeam[i].IsAlive())
            {
                Console.WriteLine($"  {trainer.BattleTeam[i].Name} | {trainer.BattleTeam[i].Level} | {trainer.BattleTeam[i].TypeOne} | {trainer.BattleTeam[i].TypeTwo} | {trainer.BattleTeam[i].Pv} / {trainer.BattleTeam[i].PvMax} PV | {trainer.BattleTeam[i].Attack} | {trainer.BattleTeam[i].Defense} | {trainer.BattleTeam[i].AttackSpecial} | {trainer.BattleTeam[i].DefenseSpecial}");
                nb_event++;
            }
        }

        bool choice = false;
        Event event_choice = new Event();
        while (!choice)
        {
            choice = event_choice.ChoiceEvent(nb_event);

            Console.Clear();
            AffichageVs();
            Console.WriteLine("Choisissez votre Pokemon actif :");
            Console.WriteLine("  Nom | Niveau | Type1 | Type2 | Pv/PvMax | Attack | Defense | AttackSpecial | DefenseSpecial");
            for (int i = 0; i < trainer.BattleTeam.Count; i++)
            {
                if (event_choice.action_count == i && trainer.BattleTeam[i].IsAlive())
                {
                    Console.WriteLine($"> {trainer.BattleTeam[i].Name} | {trainer.BattleTeam[i].Level} | {trainer.BattleTeam[i].TypeOne} | {trainer.BattleTeam[i].TypeTwo} | {trainer.BattleTeam[i].Pv}/{trainer.BattleTeam[i].PvMax} | {trainer.BattleTeam[i].Attack} | {trainer.BattleTeam[i].Defense} | {trainer.BattleTeam[i].AttackSpecial} | {trainer.BattleTeam[i].DefenseSpecial}");
                }
                else if (trainer.BattleTeam[i].IsAlive())
                {
                    Console.WriteLine($"  {trainer.BattleTeam[i].Name} | {trainer.BattleTeam[i].Level} | {trainer.BattleTeam[i].TypeOne} | {trainer.BattleTeam[i].TypeTwo} | {trainer.BattleTeam[i].Pv}/{trainer.BattleTeam[i].PvMax} | {trainer.BattleTeam[i].Attack} | {trainer.BattleTeam[i].Defense} | {trainer.BattleTeam[i].AttackSpecial} | {trainer.BattleTeam[i].DefenseSpecial}");
                }
            }
        }
        ActivePokemon1 = trainer.BattleTeam[event_choice.action_count];
        return trainer.BattleTeam[event_choice.action_count];
    }

    public void BattleRound(Pokemon activePokemon1, Pokemon activePokemon2)
    {
        Event event_choice = new Event();
        int nb_event = 0;
        Console.Clear();
        AffichageVs();
        AffichageHUD();
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

        Capacity capacity_random = GetCapacityRandom();

        Random random_first = new Random();
        int first = random_first.Next(2); 

        if(ActivePokemon1.Speed > ActivePokemon2.Speed || first == 0)
        {
            if (event_choice.action_count == 0)
            {
               NextAction1 = UseCapacity(activePokemon1, activePokemon1.Capacity1, activePokemon2);
            }
            else if (event_choice.action_count == 1)
            {
                NextAction1 = UseCapacity(activePokemon1, activePokemon1.Capacity2, activePokemon2);
            }
            else if (event_choice.action_count == 2)
            {
                NextAction1 = UseCapacity(activePokemon1, activePokemon1.Capacity3, activePokemon2);
            }
            if (activePokemon2.IsAlive())
            {
                NextAction2 = UseCapacity(activePokemon2, capacity_random, activePokemon1);
                if (!activePokemon1.IsAlive())
                {
                    PlayerBattle.BattleTeam.Remove(activePokemon1);
                }
            }
        }
        else if(ActivePokemon2.Speed > ActivePokemon1.Speed || first == 1)
        {
            NextAction1 = UseCapacity(activePokemon2, capacity_random, activePokemon1);
            if (activePokemon1.IsAlive())
            {
                if (event_choice.action_count == 0)
                {
                    NextAction2 = UseCapacity(activePokemon1, activePokemon1.Capacity1, activePokemon2);
                }
                else if (event_choice.action_count == 1)
                {
                    NextAction2 = UseCapacity(activePokemon1, activePokemon1.Capacity2, activePokemon2);
                }
                else if (event_choice.action_count == 2)
                {
                    NextAction2 = UseCapacity(activePokemon1, activePokemon1.Capacity3, activePokemon2);
                }
            }
            else
            {
                PlayerBattle.BattleTeam.Remove(activePokemon1);
            }
        }
    }
}