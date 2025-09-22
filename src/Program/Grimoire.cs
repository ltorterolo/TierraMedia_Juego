namespace Program;

public class Grimoire
{
    private static List<Spell> spells = new List<Spell>();

    private static List<Spell> KnownSpells = new List<Spell>();

    public static void AddSpell()
    {
        int flag = 1;
        foreach (var hechizo in spells)
        {
            if (flag > 0 && !KnownSpells.Contains(hechizo))
            {
                KnownSpells.Add(hechizo);
                flag = 0;
            }
        }
    } 

    public static void SetSpells(Spell hechizo)
    {
        spells.Add(hechizo);
    }
}