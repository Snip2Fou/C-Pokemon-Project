class Object
{
    public string Name { get; set; }
    public object Value { get; set; }
    public string Description { get; set; }

    public Object(string name, object value, string description) 
    { 
        Name = name;
        Value = value;
        Description = description;
    }
}