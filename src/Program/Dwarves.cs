using System.Runtime.CompilerServices;

namespace Program;

public class Dwarve
{
    public string Name { get; }

    private static int MaxLife = 150;

    private int Attack = 75;

    private int Defense = 75;
    
    public int Life = MaxLife;

    private List<Item> Items = new List<Item>();

    public  Dwarve (string name)
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
}