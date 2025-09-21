namespace RoleplayGame;

public class Dwarves
{
    public string name { get; }
    private int MaxLife = 150;
    private int Life = 150;
    
    private int Attack = 50;
    private int Defense = 75;
    
    private List<Item> Items = new List<Item>();
    
    public int getAttack()
    {
        foreach (Item objeto in Items)
        {
            Attack += Item.Attack();
        }

        return Attack;
    }
    public int getDefense()
    {
        foreach (Item objeto in Items)
        {
            Defense += Item.Defense();
        }

        return Defense;
    }

    public int reciveAttack(int ataque)
    {
        return Life -= ataque + getDefense();
    }

    public void heal()
    {
        Life += MaxLife / 2;
        foreach (Item objeto in Items)
        {
            Life += Item.Healing();
        }
    }
    public void addItem(Item objeto)
    {
        Items.Add(objeto);
    }

    public void removeItem(Item objeto)
    {
        Items.Remove(objeto);
    }
}
