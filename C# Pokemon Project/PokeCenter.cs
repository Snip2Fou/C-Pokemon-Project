using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class PokeCenter
{
    public bool exit = false;
    public bool exitBoutique = false;
    public bool exitEquipe = false;
    public Trainer Player = new Trainer("");
    public PokeBall pokeBall = new PokeBall();
    public SuperBall superBall = new SuperBall();
    public HyperBall hyperBall = new HyperBall();
    public Potion potion = new Potion();
    public SuperPotion superPotion = new SuperPotion();
    public HyperPotion hyperPotion = new HyperPotion();

    public PokeCenter(Trainer player)
    {
        Player = player;
    }
    

    public void Interface()
    {
        Event event_choice = new Event();
        
        Console.WriteLine("Bienvenue au Centre Pokemon\n");
        Console.WriteLine("> Soigne tes Pokemon");
        Console.WriteLine("  Boutique");
        Console.WriteLine("  Equipe Pokemon");
        Console.WriteLine("  Sortir du Centre");

        while (!exit)
        {
            bool choice_event = event_choice.ChoiceEvent(4);
            if (choice_event)
            {
                if (event_choice.action_count == 0)
                {
                    foreach (var poke in Player.Team)
                    {
                        poke.Pv = poke.PvMax;
                    }
                    Console.WriteLine("Vos Pokemon sont soignée");
                }
                else if (event_choice.action_count == 1)
                {
                    Boutique();
                }
                else if (event_choice.action_count == 2)
                {
                    Equipe();
                }
                else if (event_choice.action_count == 3)
                {
                    exit = true;
                    Console.WriteLine("Merci de votre visite");
                }
            }
            Console.Clear();
            Console.WriteLine("Bienvenue au Centre Pokemon\n");
            if (event_choice.action_count == 0)
            {
                Console.WriteLine("> Soigne tes Pokemon");
                Console.WriteLine("  Boutique");
                Console.WriteLine("  Equipe Pokemon");
                Console.WriteLine("  Sortir du Centre"); ;
            }
            else if (event_choice.action_count == 1)
            {
                Console.WriteLine("  Soigne tes Pokemon");
                Console.WriteLine("> Boutique");
                Console.WriteLine("  Equipe Pokemon");
                Console.WriteLine("  Sortir du Centre");
            }
            else if (event_choice.action_count == 2)
            {
                Console.WriteLine("  Soigne tes Pokemon");
                Console.WriteLine("  Boutique");
                Console.WriteLine("> Equipe Pokemon");
                Console.WriteLine("  Sortir du Centre");
            }
            else if (event_choice.action_count == 3)
            {
                Console.WriteLine("  Soigne tes Pokemon");
                Console.WriteLine("  Boutique");
                Console.WriteLine("  Equipe Pokemon");
                Console.WriteLine("> Sortir du Centre");
            }
        }
    }

    public void Boutique()
    {
        Event event_choice = new Event();
        Event event_boutique = new Event();

        Console.WriteLine("Bienvenue dans la boutique\n");
        Console.WriteLine("> Achete des objects");
        Console.WriteLine("  Vendre des objects");
        Console.WriteLine("  Sortir de la boutique");

        while (!exit)
        {
            bool choice_event = event_choice.ChoiceEvent(3);
            if (choice_event)
            {
                if (event_choice.action_count == 0)
                {
                    Console.WriteLine($"> {pokeBall.Name}");
                    Console.WriteLine($"  {superBall.Name}");
                    Console.WriteLine($"  {hyperBall.Name}");
                    Console.WriteLine($"  {potion.Name}");
                    Console.WriteLine($"  {superPotion.Name}");
                    Console.WriteLine($"  {hyperPotion.Name}");

                    bool choice_boutique = event_boutique.ChoiceEvent(7);
                    if (choice_boutique)
                    {
                        if (event_choice.action_count == 0)
                        {
                            Player.Inventory.AddObject(pokeBall);
                        }
                        else if (event_choice.action_count == 1)
                        {
                            Player.Inventory.AddObject(superBall);
                        }
                        else if (event_choice.action_count == 2)
                        {
                            Player.Inventory.AddObject(hyperBall);
                        }
                        else if (event_choice.action_count == 3)
                        {
                            Player.Inventory.AddObject(potion);
                        }
                        else if (event_choice.action_count == 4)
                        {
                            Player.Inventory.AddObject(superPotion);
                        }
                        else if (event_choice.action_count == 5)
                        {
                            Player.Inventory.AddObject(hyperPotion);
                        }
                        else if (event_choice.action_count == 6)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Vos Pokemon sont soignée");
                }
                else if (event_choice.action_count == 1)
                {
                    
                }
                else if (event_choice.action_count == 2)
                {
                    exitBoutique = true;
                    Console.WriteLine("Merci de votre visite");
                }
            }
        }
    }

    public void Equipe()
    {

    }

}

