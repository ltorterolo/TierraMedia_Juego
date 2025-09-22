namespace Program;

public class Spell 
{
    // todos los datos de esta clase son creados por el usuario entonces "get" a partir de lo que se diga 
    public string Name { get; } 
    public int Attack { get; }
    public int Defense { get; }
    public int Healing { get;  }
    
    // creo un contructor para que cada spell tenga su propia informacion 
    public Spell(string nombre, int attack, int defense, int healing)
    {
        Name = nombre;
        Attack = attack;
        Defense = defense;
        Healing = healing;
    }
}