namespace Program;

public class Grimoire
{
    private static int _attack = 0;
    
    private static int _defense = 0;
    
    private static int _healing = 0;
        
    public static List<Spell> _spells = new List<Spell>();

    public static List<Spell> _knownSpells = new List<Spell>();

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
        _attack = 0;
        foreach (var hechizo in _knownSpells)
        {
            _attack += hechizo.Attack;
        }

        return _attack;
    }
    public static int GetDefense()
    {
        _defense = 0;
        foreach (var hechizo in _knownSpells)
        {
            _defense += hechizo.Defense;
        }

        return _defense;
    }
    public static int gethealing()
    {
        _healing = 0;
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
    public static int ResetGrimoire()
    {
        int total = 0;
        foreach (var hechizo in Grimoire._spells)
        {
            if (Grimoire._knownSpells.Contains(hechizo))
            {
                total += 1;
                Grimoire._knownSpells.Remove(hechizo);
            }
        }

        return total;
    }
}