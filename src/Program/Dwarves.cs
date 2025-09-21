namespace Program;

public class Dwarve
{
    public string Name { get; }

    private static int MaxLife = 120;

    // Ataque y defensa base 
    private int Attack = 40;
    private int Defense = 75;
    
    public int Life = MaxLife;

    // lista de items que tiene el enano
    private List<Item> Items = new List<Item>();

    // Constructor de Enano
    public  Dwarve (string name)
    {
        Name = name;
    }
    
    // Calcula los puntos de ataque, sumando los valores de ataque de los items que tiene el personaje
    public int GetAttack()
    {
        foreach (Item objeto in Items)
        {
            Attack += objeto.Attack;
        }

        return Attack;
    }

    // Calcula los puntos de defensa, sumando los valores de defensa de los items que tiene el personaje
    public int GetDefense()
    {
        foreach (Item objeto in Items)
        {
            Defense += objeto.Defense;
        }

        return Defense;
    }

    // Recibe un ataque y ajusta la vida
    public int ReceiveAttack(int ataque)
    {
        return Life -= ataque - GetDefense();
    }

    // Cura al enano sumando el "Healing" de cada ítem
    public void Heal()
    {
        foreach (Item objeto in Items)
        {
            Life += objeto.Healing;
        }
        // Tope por si se pasa de la vida máxima
        if (Life > MaxLife)
        {
            Life = MaxLife;
        }
    }

    // Agrega un ítem a la lista de elementos
    public void AddItem(Item objeto)
    {
        Items.Add(objeto);
    }

    // Saca un ítem de la lista de elementos
    public void RemoveItem(Item objeto)
    {
        Items.Remove(objeto);
    }
}