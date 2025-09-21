namespace RoleplayGame;

public class Knight
{
    public string Name { get; } // lo adquiere una vez que el usuario le pone nombre
    private static int MaxLife = 160;
    public int Life = MaxLife; //cambia vida para cada personaje / arranca con el max
    
    private int Attack = 20; // cambia x perosnaje / ataque por su cuenta, sin elemento
    private int Defense = 80; // cambia x personaje / defensa por su cuenta, sin elemento
    
    private List<Item> Items = new List<Item>(); // se van agregando los elementos que tiene 

    public Knight(string name)
    {
        Name = name; 
    }
    public int GetAttack()
    {
        foreach (Item objeto in Items) // recorre la lista de elementos que tiene el caballero, 
        {                              // y se queda con el ataque de cada uno
            Attack += objeto.Attack(); // a el ataque incial se le suma el ataque de cada elemento que tenga el caballero
        }

        return Attack;
    }
    public int GetDefense()
    {
        foreach (Item objeto in Items) 
        {
            Defense += objeto.Defense(); // lo mimso que el ataque , pero con la defensa
        }

        return Defense;
    }

    public int ReciveAttack(int ataque)
    {
        return Life -= ataque + GetDefense(); // cuando atacan al personaje, el valor de vida, corresponde al ataque dado, 
    }                                           // pero a eso se le contrarresta la defesa (propia + elementos)

    public void Heal()      
    {
        foreach (Item objeto in Items) // lo mismo que con ataque y defensa, el caballero no tiene magia por si solo, 
        {                              // entonces solo se cura con ayuda de los objetos
            Life += objeto.Healing();
        }
        if (Life > MaxLife)
        {
            Life = MaxLife;
        }
        else
        {
            Life = Life;
        }
    }
    
    public void AddItem(Item objeto) // adquiere un elemento y lo agrega a la lista que se evalua 
    {
        Items.Add(objeto);
    }

    public void RemoveItem(Item objeto) // en caso de perder un objeto, lo quita paa dehjar de tomar en cuenta sus beneificos 
    {
        Items.Remove(objeto);
    }
}