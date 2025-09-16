namespace RoleplayGame;

public class Elves
{
    public string Name { get; set; }
    public int Life { get; set; }
    private int Magic { get; set; }

    public List<item> Elements { get; set; }
    public Elves (string nombre, int vida, int magia, List<item> inventario)
    {
        Name = nombre;
        Life = vida;
        Magic = magia;
        Elements = inventario;
    }

    public void AddElemento(Item item)
    {
        inventario.Add(item);
    }

    public void SubsElemento(Item item)
    {
        inventario.Pop(item);
    }
}