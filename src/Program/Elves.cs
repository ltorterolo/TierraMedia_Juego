namespace Program;

public class Elves
{
    public string Name { get; }
    public int Life { get; }
    private int Magic { get; }
    public Elves (string nombre, int vida, int magia)
    {
        Name = nombre;
        Life = vida;
        Magic = magia;
    }
}