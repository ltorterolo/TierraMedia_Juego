using Program;

public static class juego
{
    public static void Main()
    {
        Item Cortauñas = new Item("Cortauñas", 7, 0, 10);
        Item bufanda = new Item("Bufanda", 0, 5, 0);
        Item latigo = new Item("látigo", 15, 0, 0);
        Item almohadon = new Item("almohadoón", 2, 10, 5);
        Spell fireball = new Spell("fireball", 40, 0,0);
        Spell awa = new Spell("awa", 2, 2, 2);
        Grimoire.SetSpells(fireball);
        Grimoire.SetSpells(awa);
        Wizard pepito = new Wizard("Pepito");
        pepito.AddItem(Cortauñas);
        pepito.AddItem(bufanda);
        pepito.ReadGrimoire();
        pepito.ReadGrimoire();
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
        
        Dwarve juancho = new Dwarve("Juancho");
        juancho.AddItem(latigo);
        juancho.AddItem(almohadon);
        
        Console.WriteLine(juancho.GetAttack());
        Console.WriteLine(juancho.GetDefense());

        juancho.ReceiveAttack(pepito.getAttack());
        
        Console.WriteLine(juancho.Life);
        
        juancho.Heal();
        
        Console.WriteLine(juancho.Life);
        
    }
}