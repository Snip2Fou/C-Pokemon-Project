using System;
using System.Collections.Generic;

public class Inventory 
{
    private List<Object> _inventory = new List<Object>();

    public Inventory() { }

    public void UsingObject(Object obj, Pokemon pokemon) 
    {
        obj.UseThis(pokemon);
        _inventory.Remove(obj);
    }
    public void AddObject(Object obj) { _inventory.Add(obj);}

    public void DisplayInventory()
    {
        foreach (var obj in _inventory)
        {
            Console.WriteLine($"{obj.Name} : {obj.Description}");
        }
    }
    public List<Object> SaveInventory()
    {
        return _inventory;
    }
}