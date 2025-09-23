using System.Runtime.CompilerServices;

namespace Program;
//Todos los nombres que empiezan con ( _ ), fueron refactorizados por recomendación de Rider

public class Elves
{
    //Establezco los atributos básicos
    public string Name { get; }

    private static int _maxLife = 100;

    private static int _maxMagic = 80;

    private static int _initialAttack = 10;
    private int _attack = _initialAttack;
    private static int _initialDefense = 20;
    private int _defense = _initialDefense;

    public int Magic = _maxMagic;

    public int Life = _maxLife;

    private List<Item> _items = new List<Item>();

    public Elves(string name)
    {
        Name = name;

    }

    public int GetAttack() //Calcula el ataque total, al sumar los datos de los items agregados
    {
        if (Life > 0)
        {
            _attack = _initialAttack;
            foreach (Item objeto in _items)
            {
                _attack += objeto.Attack * 3 / 2;
            }

            return _attack;
        }

        return 0;
    }

    //Calcula la defensa total, al sumar los datos de los items agregados
    public int GetDefense()
    {
        if (Life > 0)
        {
            _defense = _initialDefense;
            foreach (Item objeto in _items)
            {
                _defense += objeto.Defense;
            }

            return _defense;
        }

        return 0;
    }

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

    public void Heal()
    {
        if (Life > 0)
        {
            if (Magic >= 5)
            {
                Life += _maxLife / 2;
            }
            foreach (Item objeto in _items)
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
    public void AddItem(Item objeto)
    {
        if (Life > 0)
        {
            _items.Add(objeto);
        }
    }
    
    public void RemoveItem(Item objeto)
    {
        if (Life > 0)
        {

            _items.Remove(objeto);
        }
    }
    
}