namespace Program;

public class Item
{
    public string Name { get; }
    public int Attack { get; }
    
    public int Defense { get; }
    
    public int Healing { get; set; }

    public Item (String name, int attack, int defense, int healing)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
        Healing = healing;
    }
    
}