using System;

public class Object
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Effect { get; set; }

    public Object(string name, string description) 
    { 
        Name = name;
        Description = description;
    }
    public Object() { }

    virtual public void UseObject() { }
    
}

public class PokeBall : Object
{
    public PokeBall() 
    {
        Name = "PokeBall";
        Description = "Iconique objet de la franchise, il permet de capturer le Pok�mon sauvage en face de vous. L'objet est le plus standard qui soit, d'une couleur rouge et blanche et poss�de un Bonus Ball de x1.";
        Effect = 1;
    }

    override public void UseObject()
    {
        
    }
}

public class SuperBall : Object
{
    public SuperBall() 
    {
        Name = "SuperBall";
        Description = "La Super Ball est plus performante que la Pok� Ball, dot�e d'une couleur bleue et blanche avec des petites barres rouges sur le haut de la Ball. Cette derni�re a un Bonus Ball de x1.5.";
        Effect = 1.5;
    }

    override public void UseObject()
    {

    }
}

public class HyperBall : Object
{
    public HyperBall()
    {
        Name = "HyperBall";
        Description = "L'Hyper Ball est la plus performante des Balls standards (hors Master Ball) que vous pouvez obtenir dans le jeu via la boutique. Elle est de couleur noire et blanche avec un H dor� sur la partie sombre. Cette derni�re a un Bonus Ball de x2.";
        Effect = 2;
    }

    override public void UseObject()
    {

    }
}

public class Potion : Object
{
    public Potion()
    {
        Name = "Potion";
        Description = "Restore 20 HP.";
        Effect = 20;
    }

    override public void UseObject()
    {

    }
}

public class SuperPotion : Object
{
    public SuperPotion()
    {
        Name = "SuperPotion";
        Description = "Restore 60 HP.";
        Effect = 60;
    }

    override public void UseObject()
    {

    }
}

public class HyperPotion : Object
{
    public HyperPotion()
    {
        Name = "HyperPotion";
        Description = "Restore 120 HP.";
        Effect = 120;
    }

    override public void UseObject()
    {

    }
}