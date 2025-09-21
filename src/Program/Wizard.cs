using System.Runtime.CompilerServices;

namespace Program;

public class Wizard
{
    public string Name { get; }

    private static int MaxLife = 50;

    private static int MaxMagic = 100;

    private int Attack = 10;

    private int Defense = 5;

    public int Magic = MaxMagic;

    public int Life = MaxLife;

    private List<Item> Items = new List<Item>();
    
    public  Wizard (string name)
    {
        Name = name;
        
    }

    public int getAttack()
    {
        foreach (Item objeto in Items)
        {
            Attack += objeto.Attack;
        }

        return Attack;
    }

    public int getDefense()
    {
        foreach (Item objeto in Items)
        {
            Defense += objeto.Defense;
        }

        return Defense;
    }

    public int RecieveAttack(int ataque)
    {
        return Life -= ataque + getDefense();
    }

    public void Heal()
    {
        Life += MaxLife / 2;
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

    public void ReadGrimoire(int magic)
    {
        if (magic > 10)
        {
            Grimoire.AddSpell();
            Magic -= 10;
        }
    }
    
}