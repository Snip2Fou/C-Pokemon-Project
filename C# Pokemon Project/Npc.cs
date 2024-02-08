using System;
using System.Collections.Generic;
using System.Media;
using System.Security.Policy;

public class Npc
{
    public string Name { get; set; }
    public List<string> dialogue = new List<string>();
    public int dialogue_act = 0;
    public bool is_terminated = false;
    public int[] NpcPos;

    virtual public void LaunchNpc()
    {
        while (dialogue_act < dialogue.Count && !is_terminated) 
        {
            PlayDialogue();
        }
        if (dialogue_act >= dialogue.Count && !is_terminated)
        {
            dialogue_act = 0;
        }
    }
   virtual public void PlayDialogue(){}

    public void AfficheDialogue()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(Name);
        Console.ResetColor();
        Console.WriteLine($" : {dialogue[dialogue_act]}");
    }

    public void SkipDialogue()
    {
        Console.Write("\nAppuyer pour passer...");
        Console.ReadKey();
    }

    public string YesOrNo(string question)
    {
        Event yes_no_event = new Event();
        bool yes_no_choice = false;
        while (!yes_no_choice)
        {
            Console.Clear();
            Console.WriteLine(question);
            if (yes_no_event.action_count == 0)
            {
                Console.WriteLine("> Oui");
                Console.WriteLine("  Non");
            }
            else if (yes_no_event.action_count == 1)
            {
                Console.WriteLine("  Oui");
                Console.WriteLine("> Non");
            }

            yes_no_choice = yes_no_event.ChoiceEvent(2);
        }
        if(yes_no_event.action_count == 0)
        {
            return "yes";
        }
        else
        {
            return "no";
        }
    }
}

public class NpcPrChen : Npc
{
    public NpcPrChen()
    {
        Name = "Pr. Chen";
        dialogue.Add("Bien le bonjour! Bienvenue dans le monde magique des Pokemon! Mon nom est Chen! ");
        dialogue.Add("Les gens souvent m'appellent le Prof Pokemon! Ce monde est peuple de creatures du nom de Pokemon!");
        dialogue.Add("Pour certains, les Pokemon sont des animaux domestiques, pour d'autres, ils sont un moyen de combattre.");
        dialogue.Add("Pour ma part... L'etude des Pokemon est ma profession. Tout d'abord, quel est ton nom? ");
        dialogue.Add("TakeName");
        dialogue.Add("OK! Ton nom est donc");
        dialogue.Add("Ta quete des Pokemon est sur le point de commencer! Un tout nouveau monde de reves, d'aventures et de Pokemon t'attend! Dingue!");
        dialogue.Add("Mais d'abord sache que des Pokemon sauvages vivent dans les hautes herbes et peuvent surgir a tout instant!");
        dialogue.Add("Tu as besoin d'un Pokemon pour te proteger! Allez, suis-moi! Viens par la!");
        dialogue.Add("ChooseStarterPokemon");
    }

    override public void PlayDialogue()
    {
        if (dialogue[dialogue_act+1] == "TakeName")
        {
            AfficheDialogue();
            string player_name = "";
            while (player_name.Length <= 2)
            {
                AfficheDialogue();
                Console.Write("Nom : ");
                player_name = Console.ReadLine();
            }
            Game.Instance.player.Name = player_name;
            dialogue_act++;
            dialogue_act++;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Name);
            Console.ResetColor();
            Console.WriteLine($" : {dialogue[dialogue_act]} {Game.Instance.player.Name}");
            SkipDialogue();
            dialogue_act++;
        }
        else if(dialogue[dialogue_act + 1] == "ChooseStarterPokemon")
        {
            AfficheDialogue();
            SkipDialogue();
            dialogue_act++;
            ChooseStaterPokemon();
            dialogue_act++;
        }
        else
        {
            AfficheDialogue();
            SkipDialogue();
            dialogue_act++;
        }
        if(dialogue_act >= dialogue.Count - 1)
        {
            is_terminated = true;
        }
    }

    public void ChooseStaterPokemon()
    {
        Event event_choice = new Event();
        bool choice = false;
        while(!choice)
        {
            Console.Clear();
            Console.WriteLine($"{Game.Instance.player.Name}, quel Pokémon choisis-tu ?");
            if (event_choice.action_count == 0)
            {
                Console.WriteLine("> Bulbizarre");
                Console.WriteLine("  Salamèche");
                Console.WriteLine("  Carapuce");
            }
            else if (event_choice.action_count == 1)
            {
                Console.WriteLine("  Bulbizarre");
                Console.WriteLine("> Salamèche");
                Console.WriteLine("  Carapuce");
            }
            else if (event_choice.action_count == 2)
            {
                Console.WriteLine("  Bulbizarre");
                Console.WriteLine("  Salamèche");
                Console.WriteLine("> Carapuce");
            }

            choice = event_choice.ChoiceEvent(3);
            string yes_no = "";
            if (choice)
            {
                if (event_choice.action_count == 0)
                {
                    yes_no = YesOrNo("Veux-tu le Pokémon des plantes, Bulbizarre?");
                    if (yes_no == "no")
                    {
                        choice = false;
                    }
                    else
                    {
                        Game.Instance.player.AddPokemon(Game.Instance.pokemons[0]);
                    }
                }
                else if (event_choice.action_count == 1)
                {
                    yes_no = YesOrNo("Veux-tu le Pokémon de feu, Salamèche ? ");
                    if (yes_no == "no")
                    {
                        choice = false;
                    }
                    else
                    {
                        Game.Instance.player.AddPokemon(Game.Instance.pokemons[3]);
                    }
                }
                else if (event_choice.action_count == 2)
                {
                    yes_no = YesOrNo("Veux-tu le Pokémon de l'eau, Carapuce?");
                    if (yes_no == "no")
                    {
                        choice = false;
                    }
                    else
                    {
                        Game.Instance.player.AddPokemon(Game.Instance.pokemons[6]);
                    }
                }
            }
        }
    }
}


public class NpcMaman : Npc
{
    public NpcMaman() 
    {
        Name = "Maman";
        dialogue.Add("Mais qu'est-ce qu'il t'est arrivé, ");
        dialogue.Add("Ton/Tes Pokémon et toi avez l'air épuisés ! Vous devriez faire une sieste. (Elle soigne l'équipe du joueur)");
        dialogue.Add("HealAllPokemon");
        dialogue.Add("Ah ! Ton/tes Pokémon et toi avez l'air d'aller mieux !");
        dialogue.Add($"Bonjour, {Game.Instance.player.Name}. Bonne chance pour ton aventure !");
        NpcPos = new int[2];
        NpcPos[0] = 15;
        NpcPos[1] = 33;
    }

    override public void LaunchNpc()
    {
        int sommePvTeam = 0;
        int sommePvMax = 0;
        foreach (var poke in Game.Instance.player.Team)
        {
            sommePvTeam += poke.Pv;
            sommePvMax += poke.PvMax;
        }
        if (sommePvTeam > sommePvMax / 2)
        {
            dialogue_act = 4;
        }
        while (dialogue_act < dialogue.Count && !is_terminated)
        {
            PlayDialogue();
        }
        if (dialogue_act >= dialogue.Count && !is_terminated)
        {
            dialogue_act = 0;
        }
    }
    override public void PlayDialogue()
    {
        if(dialogue_act == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Name);
            Console.ResetColor();
            Console.WriteLine($" : {dialogue[dialogue_act]}{Game.Instance.player.Name}");
            SkipDialogue();
            dialogue_act++;
        }
        else if (dialogue_act == 3)
        {
            AfficheDialogue();
            SkipDialogue();
            dialogue_act = 5;
        }
        else if (dialogue_act == 4)
        {
            AfficheDialogue();
            SkipDialogue();
            dialogue_act++;
        }
        else if (dialogue[dialogue_act + 1] == "HealAllPokemon")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Name);
            Console.ResetColor();
            Console.WriteLine($" : {dialogue[dialogue_act]}");
            SkipDialogue();
            foreach (var poke in Game.Instance.player.Team)
            {
                poke.Pv = poke.PvMax;
            }
            dialogue_act += 2;
        }
        else
        {
            AfficheDialogue();
            SkipDialogue();
            dialogue_act++;
        }
    }
}


public class NpcAlex : Npc
{
    public Trainer trainer = new Trainer("Alex");
    public NpcAlex() 
    {
        dialogue.Add("Salut, dresseur ! Je suis un passionne de Pokémon. Tu cherches un combat pour tester la force de tes Pokemon ?");
        dialogue.Add("ChoiceYesNo");
        dialogue.Add("Genial ! On va voir ça.");
        dialogue.Add("Bien joue, dresseur ! Tes Pokémon sont vraiment forts. Ça a ete un super combat");
        dialogue.Add("Oh, pas de problème ! Si jamais tu changes d'avis, n'hésite pas à revenir. On se croisera peut-être sur la route, prêts à en découdre !");
    }

    override public void PlayDialogue()
    {
        if (dialogue[dialogue_act + 1] == "ChoiceYesNo")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Name);
            Console.ResetColor();
            string yesno = YesOrNo($" : {dialogue[dialogue_act]}");
            if(yesno == "yes")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Name);
                Console.ResetColor();
                Console.WriteLine($" {dialogue[dialogue_act]}");
            }
            dialogue_act += 2;
        }
        else
        {
            AfficheDialogue();
            SkipDialogue();
            dialogue_act++;
        }
    }

}