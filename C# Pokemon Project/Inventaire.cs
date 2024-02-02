using System.Collections.Generic;

class Inventaire
{
    private List<Object> _inventaire = new List<Object>();

    public void UsingObject(Object obj) {  _inventaire.Remove(obj);}
    public void AddObject(Object obj) { _inventaire.Add(obj);}
}