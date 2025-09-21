using System.Runtime.CompilerServices;

namespace RoleplayGame;
//Todos los nombres que empiezan con ( _ ), fueron refactorizados por recomendación de Rider

public class Elves
{
    //Establezco los atributos básicos
    public string Name { get; }

    private static int _maxLife = 100;

    private static int _maxMagic = 80;

    private int _attack = 5;

    private int _defense = 10;

    public int Magic = _maxMagic;

    public int Life = _maxLife;

    private List<Item> _items = new List<Item>();
    
    public  Elves (string name)
    {
        Name = name;
        
    }

    public int GetAttack() //Calcula el ataque total, al sumar los datos de los items agregados
    {
        foreach (Item objeto in _items)
        {
            _attack += objeto.Attack;
        }

        return _attack;
    }

    public int GetDefense() //Calcula la defensa total, al sumar los datos de los items agregados
    {
        foreach (Item objeto in _items)
        {
            _defense += objeto.Defense;
        }

        return _defense;
    }

    public int RecieveAttack(int ataque) //Calcula, desde cada personaje, el ataque que se les inflige
    {
        return Life -= ataque + GetDefense();
    }

    public void Heal() //Atributo innato exclusivo de elfos y magos
    {
        Life += _maxLife / 2;
        foreach (Item objeto in _items)
        {
            Life += objeto.Healing;
        }

        if (Life > _maxLife) //Corroboro que no se exceda de la máxima vida establecida
        {
            Life = _maxLife;
        }
    }

    public void AddItem(Item objeto)
    {
        _items.Add(objeto);
    }
    
    public void RemoveItem(Item objeto)
    {
        _items.Remove(objeto);
    }
    
}