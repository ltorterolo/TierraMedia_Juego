namespace Program;

public class Grimoire
{
    private static int _attack = 0;
    
    private static int _defense = 0;
    
    private static int _healing = 0;
        
    private static List<Spell> _spells = new List<Spell>();

    private static List<Spell> _knownSpells = new List<Spell>();

    public static void AddSpell()
    {
        int flag = 1;
        foreach (var hechizo in _spells)
        {
            if (flag > 0 && !_knownSpells.Contains(hechizo))
            {
                _knownSpells.Add(hechizo);
                flag = 0;
            }
        }
    }

    public static int GetAttack()
    {
        foreach (var hechizo in _knownSpells)
        {
            _attack += hechizo.Attack;
        }

        return _attack;
    }
    public static int GetDefense()
    {
        foreach (var hechizo in _knownSpells)
        {
            _defense += hechizo.Defense;
        }

        return _defense;
    }
    public static int gethealing()
    {
        foreach (var hechizo in _knownSpells)
        {
            _healing += hechizo.Healing;
        }

        return _healing;
    }
    

    public static void SetSpells(Spell hechizo)
    {
        _spells.Add(hechizo);
    }
}