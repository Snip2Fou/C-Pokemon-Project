using System;

class Npc
{
   public bool ennemy = true;

    public void NpcEnnemy(Trainer player)
    {
        Trainer ennemy = new Trainer("Carle");
        Console.WriteLine("zzz zzz zzz");
        Console.WriteLine("Hmm... Baille... Je suppose qu'un combat de Pokémon m'aiderait à rester éveillé.");
        Battle.StartBattle(player, ennemy);
        Console.WriteLine("Rien contre toi, gamin, tu te bats comme un champion, mais je suis... Baille... Je m'assoupis...");
    }

    public void NpcHelp() 
    { 
        Console.WriteLine("Quand un pokemon n'a plus beaucoup de vie il est plus facile a capture."); 
    }
}