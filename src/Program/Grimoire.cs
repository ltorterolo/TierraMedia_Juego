namespace Program;

public class Grimoire
{
    private static int attack = 0;
    
    private static int defense = 0;
    
    private static int healing = 0;
        
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

    public static int getAttack()
    {
        foreach (var hechizo in KnownSpells)
        {
            attack += hechizo.Attack;
        }

        return attack;
    }
    public static int getDefense()
    {
        foreach (var hechizo in KnownSpells)
        {
            defense += hechizo.Defense;
        }

        return defense;
    }
    public static int gethealing()
    {
        foreach (var hechizo in KnownSpells)
        {
            healing += hechizo.Healing;
        }

        return healing;
    }
    

    public static void SetSpells(Spell hechizo)
    {
        spells.Add(hechizo);
    }
}