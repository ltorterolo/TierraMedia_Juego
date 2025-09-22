using System.Runtime.CompilerServices;

namespace Program;

public class Wizard
{
    public string Name { get; }

    private static int _maxLife = 50;

    private static int _maxMagic = 100;

    private int _attack = 50;

    private int _defense = 5;

    public int Magic = _maxMagic;

    public int Life = _maxLife;

    public List<Item> Items = new List<Item>();
    
    public  Wizard (string name)
    {
        Name = name;
        
    }

    public int GetAttack()
    {
        if (Life > 0)
        {
            foreach (Item objeto in Items)
            {
                _attack += objeto.Attack;
            }

            if (Magic >= 5)
            {
                _attack += Grimoire.GetAttack();
                Magic -= 5;
            }

            return _attack;
        }

        return 0;
    }

    public int GetDefense()
    {
        if (Life > 0)
        {
            foreach (Item objeto in Items)
            {
                _defense += objeto.Defense;
            }

            if (Magic >= 5)
            {
                _defense += Grimoire.GetDefense();
                Magic -= 5;
            }

            return _defense;
        }

        return 0;
    }

    public int RecieveAttack(int ataque)
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
        if (Magic >= 10)
        {
            Life += _maxLife / 2;
            Life += Grimoire.gethealing();
            Magic -= 10;
        }

        foreach (Item objeto in Items)
        {
            Life += objeto.Healing;
        }
        
    }

    public void AddItem(Item objeto)
    {
        Items.Add(objeto);
    }
    
    public void RemoveItem(Item objeto)
    {
        Items.Remove(objeto);
    }

    public void ReadGrimoire()
    {
        if (Magic > 10)
        {
            Grimoire.AddSpell();
            Magic -= 10;
        }
    }
    
}