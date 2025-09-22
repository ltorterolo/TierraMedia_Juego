using Program;

public static class juego
{
    public static void Main()
    {
        Item Cortauñas = new Item("Cortauñas", 7, 0, 10);
        Item bufanda = new Item("Bufanda", 0, 5, 0);
        Wizard pepito = new Wizard("Pepito");
        pepito.AddItem(Cortauñas);
        pepito.AddItem(bufanda);
        Console.WriteLine(pepito.getAttack());
        Console.WriteLine(pepito.getDefense());
        foreach (var objeto in pepito.Items)
        {
            Console.WriteLine(objeto.Name);
        }

        pepito.RemoveItem(Cortauñas);
        foreach (var objeto in pepito.Items)
        {
            Console.WriteLine(objeto.Name);
        }

    }
}