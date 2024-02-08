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
    public Object Objectvente = new Object();

    public PokeCenter(Trainer player)
    {
        Player = player;
    }
    

    public void Interface()
    {
        Event event_choice = new Event();

        Console.Clear();
        
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

        while (!exitBoutique)
        {
            bool choice_event = event_choice.ChoiceEvent(3);
            if (choice_event)
            {
                Console.Clear();
                if (event_choice.action_count == 0)
                {
                    Console.WriteLine($"> {pokeBall.Name} {pokeBall.Price} pokemoney");
                    Console.WriteLine($"  {superBall.Name} {superBall.Price} pokemoney");
                    Console.WriteLine($"  {hyperBall.Name} {hyperBall.Price} pokemoney");
                    Console.WriteLine($"  {potion.Name} {potion.Price} pokemoney");
                    Console.WriteLine($"  {superPotion.Name} {superPotion.Price} pokemoney");
                    Console.WriteLine($"  {hyperPotion.Name} {hyperPotion.Price} pokemoney");
                    Console.WriteLine("  Quitter");

                    bool choice_boutique = event_boutique.ChoiceEvent(7);
                    if (choice_boutique)
                    {

                        if (event_choice.action_count == 0)
                        {
                            bool quantity = event_boutique.QuantityEvent(Player.PokeMoney, pokeBall.Price);

                            Console.Clear();

                            Console.WriteLine("\u005E ");
                            Console.WriteLine($"{event_boutique.action_count}");
                            Console.WriteLine("\u142F");
                                                        
                            if (quantity)
                            {
                                Console.WriteLine($"tu a achete {event_boutique.action_count}x{pokeBall.Name}");
                                Player.Inventory.AddObject(pokeBall, event_boutique.action_count);
                            }
                            else
                            {
                                Console.WriteLine("tu n'a pas assez d'argents");
                            }
                        }
                        else if (event_choice.action_count == 1)
                        {
                            bool quantity = event_boutique.QuantityEvent(Player.PokeMoney, superBall.Price);

                            Console.Clear();

                            Console.WriteLine("\u005E ");
                            Console.WriteLine($"{event_boutique.action_count}");
                            Console.WriteLine("\u142F");

                            if (quantity)
                            {
                                Console.WriteLine($"tu a achete {event_boutique.action_count}x{superBall.Name}");
                                Player.Inventory.AddObject(superBall, event_boutique.action_count);
                            }
                            else
                            {
                                Console.WriteLine("tu n'a pas assez d'argents");
                            }
                        }
                        else if (event_choice.action_count == 2)
                        {
                            bool quantity = event_boutique.QuantityEvent(Player.PokeMoney, hyperBall.Price);

                            Console.Clear();

                            Console.WriteLine("\u005E ");
                            Console.WriteLine($"{event_boutique.action_count}");
                            Console.WriteLine("\u142F");

                            if (quantity)
                            {
                                Console.WriteLine($"tu a achete {event_boutique.action_count}x{hyperBall.Name}");
                                Player.Inventory.AddObject(hyperBall, event_boutique.action_count);
                            }
                            else
                            {
                                Console.WriteLine("tu n'a pas assez d'argents");
                            }
                        }
                        else if (event_choice.action_count == 3)
                        {
                            
                            bool quantity = event_boutique.QuantityEvent(Player.PokeMoney, potion.Price);

                            Console.Clear();

                            Console.WriteLine("\u005E ");
                            Console.WriteLine($"{event_boutique.action_count}");
                            Console.WriteLine("\u142F");

                            if (quantity)
                            {
                                Console.WriteLine($"tu a achete {event_boutique.action_count}x{potion.Name}");
                                Player.Inventory.AddObject(potion, event_boutique.action_count);
                            }
                            else
                            {
                                Console.WriteLine("tu n'a pas assez d'argents");
                            }
                        }
                        else if (event_choice.action_count == 4)
                        {
                            bool quantity = event_boutique.QuantityEvent(Player.PokeMoney, superPotion.Price);

                            Console.Clear();

                            Console.WriteLine("\u005E ");
                            Console.WriteLine($"{event_boutique.action_count}");
                            Console.WriteLine("\u142F");

                            if (quantity)
                            {
                                Console.WriteLine($"tu a achete {event_boutique.action_count}x{superPotion.Name}");
                                Player.Inventory.AddObject(superPotion, event_boutique.action_count);
                            }
                            else
                            {
                                Console.WriteLine("tu n'a pas assez d'argents");
                            }
                        }
                        else if (event_choice.action_count == 5)
                        {
                            bool quantity = event_boutique.QuantityEvent(Player.PokeMoney, hyperPotion.Price);

                            Console.Clear();

                            Console.WriteLine("\u005E ");
                            Console.WriteLine($"{event_boutique.action_count}");
                            Console.WriteLine("\u142F");

                            if (quantity)
                            {
                                Console.WriteLine($"tu a achete {event_boutique.action_count}x{hyperPotion.Name}");
                                Player.Inventory.AddObject(hyperPotion, event_boutique.action_count);
                            }
                            else
                            {
                                Console.WriteLine("tu n'a pas assez d'argents");
                            }
                        }
                        else if (event_choice.action_count == 6)
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    
                    if (event_choice.action_count == 0)
                    {
                        Console.WriteLine($"> {pokeBall.Name} {pokeBall.Price} pokemoney");
                        Console.WriteLine($"  {superBall.Name} {superBall.Price} pokemoney");
                        Console.WriteLine($"  {hyperBall.Name} {hyperBall.Price} pokemoney");
                        Console.WriteLine($"  {potion.Name} {potion.Price} pokemoney");
                        Console.WriteLine($"  {superPotion.Name} {superPotion.Price} pokemoney");
                        Console.WriteLine($"  {hyperPotion.Name} {hyperPotion.Price} pokemoney");
                        Console.WriteLine("  Quitter");
                    }
                    else if (event_choice.action_count == 1)
                    {
                        Console.WriteLine($"  {pokeBall.Name} {pokeBall.Price} pokemoney");
                        Console.WriteLine($"> {superBall.Name} {superBall.Price} pokemoney");
                        Console.WriteLine($"  {hyperBall.Name} {hyperBall.Price} pokemoney");
                        Console.WriteLine($"  {potion.Name} {potion.Price} pokemoney");
                        Console.WriteLine($"  {superPotion.Name} {superPotion.Price} pokemoney");
                        Console.WriteLine($"  {hyperPotion.Name} {hyperPotion.Price} pokemoney");
                        Console.WriteLine("  Quitter");
                    }
                    else if (event_choice.action_count == 2)
                    {
                        Console.WriteLine($"  {pokeBall.Name} {pokeBall.Price} pokemoney");
                        Console.WriteLine($"  {superBall.Name} {superBall.Price} pokemoney");
                        Console.WriteLine($"> {hyperBall.Name} {hyperBall.Price} pokemoney");
                        Console.WriteLine($"  {potion.Name} {potion.Price} pokemoney");
                        Console.WriteLine($"  {superPotion.Name} {superPotion.Price} pokemoney");
                        Console.WriteLine($"  {hyperPotion.Name} {hyperPotion.Price} pokemoney");
                        Console.WriteLine("  Quitter");
                    }
                    else if (event_choice.action_count == 3)
                    {
                        Console.WriteLine($"  {pokeBall.Name} {pokeBall.Price} pokemoney");
                        Console.WriteLine($"  {superBall.Name} {superBall.Price} pokemoney");
                        Console.WriteLine($"  {hyperBall.Name} {hyperBall.Price} pokemoney");
                        Console.WriteLine($"> {potion.Name} {potion.Price} pokemoney");
                        Console.WriteLine($"  {superPotion.Name} {superPotion.Price} pokemoney");
                        Console.WriteLine($"  {hyperPotion.Name} {hyperPotion.Price} pokemoney");
                        Console.WriteLine("  Quitter");
                    }
                    else if (event_choice.action_count == 4)
                    {
                        Console.WriteLine($"  {pokeBall.Name} {pokeBall.Price} pokemoney");
                        Console.WriteLine($"  {superBall.Name} {superBall.Price} pokemoney");
                        Console.WriteLine($"  {hyperBall.Name} {hyperBall.Price} pokemoney");
                        Console.WriteLine($"  {potion.Name} {potion.Price} pokemoney");
                        Console.WriteLine($"> {superPotion.Name} {superPotion.Price} pokemoney");
                        Console.WriteLine($"  {hyperPotion.Name} {hyperPotion.Price} pokemoney");
                        Console.WriteLine("  Quitter");
                    }
                    else if (event_choice.action_count == 5)
                    {
                        Console.WriteLine($"  {pokeBall.Name} {pokeBall.Price} pokemoney");
                        Console.WriteLine($"  {superBall.Name} {superBall.Price} pokemoney");
                        Console.WriteLine($"  {hyperBall.Name} {hyperBall.Price} pokemoney");
                        Console.WriteLine($"  {potion.Name} {potion.Price} pokemoney");
                        Console.WriteLine($"  {superPotion.Name} {superPotion.Price} pokemoney");
                        Console.WriteLine($"> {hyperPotion.Name} {hyperPotion.Price} pokemoney");
                        Console.WriteLine("  Quitter");
                    }
                    else if (event_choice.action_count == 6)
                    {
                        Console.WriteLine($"  {pokeBall.Name} {pokeBall.Price} pokemoney");
                        Console.WriteLine($"  {superBall.Name} {superBall.Price} pokemoney");
                        Console.WriteLine($"  {hyperBall.Name} {hyperBall.Price} pokemoney");
                        Console.WriteLine($"  {potion.Name} {potion.Price} pokemoney");
                        Console.WriteLine($"  {superPotion.Name} {superPotion.Price} pokemoney");
                        Console.WriteLine($"  {hyperPotion.Name} {hyperPotion.Price} pokemoney");
                        Console.WriteLine("> Quitter");
                    }
                }
                else if (event_choice.action_count == 1)
                {
                    Objectvente = Player.Inventory.OpenInventoryDuringBattle();
                    bool vente = event_boutique.QuantityEventSale(Objectvente.Quantity);

                    Console.Clear();

                    Console.WriteLine("\u005E ");
                    Console.WriteLine($"{event_boutique.action_count}");
                    Console.WriteLine("\u142F");

                    if (vente)
                    {
                        int money = ((Objectvente.Price * event_boutique.action_count) / 2);
                        Player.PokeMoney += money;
                        Console.WriteLine($"tu a gagne {money} pokemoney");
                    }
                    else
                    {
                        Console.WriteLine("Tu n'aa pas autant d'object de ce type");
                    }
                }
                else if (event_choice.action_count == 2)
                {
                    exitBoutique = true;
                    Console.WriteLine("Merci de votre visite");
                }
            }
            Console.Clear();
            Console.WriteLine("Bienvenue dans la boutique\n");
            if (event_choice.action_count == 0)
            {
                Console.WriteLine("> Achete des objects");
                Console.WriteLine("  Vendre des objects");
                Console.WriteLine("  Sortir de la boutique");
            }
            else if (event_choice.action_count == 1)
            {
                Console.WriteLine("  Achete des objects");
                Console.WriteLine("> Vendre des objects");
                Console.WriteLine("  Sortir de la boutique");
            }
            else if (event_choice.action_count == 2)
            {
                Console.WriteLine("  Achete des objects");
                Console.WriteLine("  Vendre des objects");
                Console.WriteLine("> Sortir de la boutique");
            }
        }
    }

    public void Equipe()
    {
        Event event_equipe = new Event();

        while (!exitEquipe)
        {
            Console.Clear();
            Console.WriteLine("Voici tout vos pokemon :\n");
            if (event_equipe.action_count == 0)
            {
                Console.WriteLine($"> {Player.Pokedex[event_equipe.action_count]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 1]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 2]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 3]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 4]}");
            }
            else if (event_equipe.action_count == 1)
            {
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 1]}");
                Console.WriteLine($"> {Player.Pokedex[event_equipe.action_count]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 1]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 2]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 3]}");
            }
            else if (event_equipe.action_count <= Player.Pokedex.Count - 3)
            {
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 2]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 1]}");
                Console.WriteLine($"> {Player.Pokedex[event_equipe.action_count]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 1]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 2]}");
            }
            else if (event_equipe.action_count <= Player.Pokedex.Count - 2)
            {
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 3]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 2]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 1]}");
                Console.WriteLine($"> {Player.Pokedex[event_equipe.action_count]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count + 1]}");
            }
            else if (event_equipe.action_count >= Player.Pokedex.Count - 1)
            {
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 4]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 3]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 2]}");
                Console.WriteLine($"  {Player.Pokedex[event_equipe.action_count - 1]}");
                Console.WriteLine($"> {Player.Pokedex[event_equipe.action_count]}");
            }

            exitEquipe = event_equipe.ListPokedex(Player.Pokedex);
        }
    }

}

