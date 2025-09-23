using Program;

public static class Juego
{
    public static void Main()
    {
        Item cortauñas = new Item("Cortauñas", 7, 0, 10);
        Item bufanda = new Item("Bufanda", 0, 5, 0);
        Item latigo = new Item("látigo", 15, 0, 0);
        Item almohadon = new Item("almohadón", 2, 10, 5);
        Spell fireball = new Spell("fireball", 40, 0,0);
        Spell awa = new Spell("awa", 2, 2, 2);
        Grimoire.SetSpells(fireball);
        Grimoire.SetSpells(awa);
        Wizard pepito = new Wizard("Pepito");
        pepito.AddItem(cortauñas);
        pepito.AddItem(bufanda);
        pepito.ReadGrimoire();
        pepito.ReadGrimoire();
        Console.WriteLine(pepito.GetAttack());
        Console.WriteLine(pepito.GetDefense());
        foreach (var objeto in pepito.Items)
        {
            Console.WriteLine(objeto.Name);
        }

        pepito.RemoveItem(cortauñas);
        foreach (var objeto in pepito.Items)
        {
            Console.WriteLine(objeto.Name);
        }
        
        Dwarve juancho = new Dwarve("Juancho");
        juancho.AddItem(latigo);
        juancho.AddItem(almohadon);
        
        Console.WriteLine(juancho.GetAttack());
        Console.WriteLine(juancho.GetAttack());
        Console.WriteLine(juancho.GetAttack());
        Console.WriteLine(juancho.GetDefense());

        juancho.ReceiveAttack(pepito.GetAttack());
        
        Console.WriteLine(juancho.Life);
        
        juancho.Heal();
        
        Console.WriteLine(juancho.Life);
        
    }
}