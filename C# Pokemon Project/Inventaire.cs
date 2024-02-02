using System;
using System.Collections.Generic;

class Inventaire 
{
    private List<Object> _inventaire = new List<Object>();

    public Inventaire() { }

    public void UsingObject(Object obj, Pokemon pokemon) 
    {
        obj.UseThis(pokemon);
        _inventaire.Remove(obj);
    }
    public void AddObject(Object obj) { _inventaire.Add(obj);}

    public void DisplayInventory()
    {
        foreach (var obj in _inventaire)
        {
            Console.WriteLine($"{obj.Name} : {obj.Description}");
        }
    }
}