namespace Program;

public class Dwarve
{
    public string Name { get; }

    private static int _maxLife = 120;

    // Ataque y defensa base 
    private static int _initialAttack = 40;
    private int _attack = _initialAttack;
    private static int _initialDefense = 75;
    private int _defense = _initialDefense;
    
    
    public int Life = _maxLife;

    // lista de items que tiene el enano
    public List<Item> Items = new List<Item>();

    // Constructor de Enano
    public  Dwarve (string name)
    {
        Name = name;
    }
    
    // Calcula los puntos de ataque, sumando los valores de ataque de los items que tiene el personaje
    public int GetAttack()
    {
        if (Life > 0)
        {
            _attack = _initialAttack;
            foreach (Item objeto in Items)
            {
                _attack += objeto.Attack*3/2; 
            } 
            return _attack;
        }
        return 0;
    }

    // Calcula los puntos de defensa, sumando los valores de defensa de los items que tiene el personaje
    public int GetDefense()
    {
        if (Life > 0)
        {
            _defense = _initialDefense;
            foreach (Item objeto in Items)
            {
                _defense += objeto.Defense;
            }

            return _defense;
        }
        return 0;
    }

    // Recibe un ataque y ajusta la vida
    public int ReceiveAttack(int ataque)
    {
        Life -= ataque - GetDefense();
        
        if (Life <= 0)
        {
            Console.WriteLine($"{this.Name} ha muerto :(");
            Life = 0;
        }
        else
        {
            if (Life >= _maxLife)
            {
                Life = _maxLife;
            }

            
        }
        return Life;
        
    }

    // Cura al enano sumando el "Healing" de cada ítem
    public void Heal()
    {
        if (Life > 0)
        {
            foreach (Item objeto in Items)
            {
                Life += objeto.Healing;
            }

            // Tope por si se pasa de la vida máxima
            if (Life > _maxLife)
            {
                Life = _maxLife;
            }
        }
    }

    // Agrega un ítem a la lista de elementos
    public void AddItem(Item objeto)
    {
        if (Life > 0)
        {
            Items.Add(objeto);
        }
    }

    // Saca un ítem de la lista de elementos
    public void RemoveItem(Item objeto)
    {
        if (Life > 0)
        {

           Items.Remove(objeto);
        }
    }
}