namespace Program;

public class Knight
{
    public string Name { get; } // lo adquiere una vez que el usuario le pone nombre
    private static int _maxLife = 160; 
    public int Life = _maxLife; //cambia vida para cada personaje / arranca con el max

    // Ataque y defensa base 
    private int _attack = 20;
    private int _defense = 80;
    
    public List<Item> Items = new List<Item>(); // se van agregando los elementos que tiene 
    
    public  Knight (string name)
    {
        Name = name;
    }
    
    // Calcula los puntos de ataque, sumando los valores de ataque de los items que tiene el personaje
    public int GetAttack()
    {
        if (Life > 0) // chequea que esté vivo
        {
            foreach (Item objeto in Items) // recorre la lista de elementos que tiene el caballero, y se queda con el ataque de cada uno
            {
                _attack += objeto.Attack; // a el ataque incial se le suma el ataque de cada elemento que tenga el caballero
            } 
            return _attack;
        }
        return 0;
    }
    
    public int GetDefense()
    {
        if (Life > 0)
        {
            foreach (Item objeto in Items) // lo mimso que el ataque , pero con la defensa
            {
                _defense += objeto.Defense*3/2; // extra defensa por ser un caballero
            }
            return _defense;
        }
        return 0;
    }

    // Recibe un ataque y ajusta la vida
    public int ReceiveAttack(int ataque)
    {
        Life -= ataque - GetDefense(); // cuando atacan al personaje, el valor de vida, corresponde al ataque dado, pero a eso se le contrarresta la defesa (propia + elementos)
        
        if (Life <= 0)
        {
            Console.WriteLine($"{this.Name} ha muerto :("); // si la vida queda igual o por debajo de 0 significa que el personaje murioó
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
    
    public void Heal() // lo mismo que con ataque y defensa, el caballero no tiene magia por si solo, entonces solo se cura con ayuda de los objetos
    {
        if (Life > 0)
        {
            foreach (Item objeto in Items)
            {
                Life += objeto.Healing;
            }
            
            if (Life > _maxLife) // Tope por si se pasa de la vida máxima
            {
                Life = _maxLife;
            }
        }
    }
    
    public void AddItem(Item objeto) // adquiere un elemento y lo agrega a la lista que se evalua
    {
        if (Life > 0)
        {
            Items.Add(objeto);
        }
    }

    public void RemoveItem(Item objeto) // en caso de perder un objeto, lo quita paa dehjar de tomar en cuenta sus beneificos
    {
        if (Life > 0)
        {

           Items.Remove(objeto);
        }
    }
}