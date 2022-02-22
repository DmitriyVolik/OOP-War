namespace WarOOP.Models;

public class Warlord : Defender
{
    public Warlord()
    {
        CurrentHealth = 100;
        StartHealth = CurrentHealth;
        Attack = 4;
        Defense = 2;
    }
    
    public List<Warrior> MoveUnits(List<Warrior> units, Army enemies)
    {
        var newUnits = new List<Warrior>();

        var dead = units.Where(x => !x.IsAlive);
        var lancers = units.Where(x => x.IsAlive && x is Lancer).ToList();
        var warriors = units.Where(x =>
            x.IsAlive && x.Attack > 0 && x is not Warlord && x is not Lancer && x is not Gunner).ToList();
        var healers = units.Where(x => x.IsAlive && x is Healer).ToList();
        var gunners = units.Where(x => x.IsAlive && x is Gunner).ToList();
        var warlord = units.First(x => x is Warlord);
        if (!enemies.HasWarlord && enemies.GetUnit() is Gunner)
        {
            
            var lowHealth = warriors.MinBy(x => x.CurrentHealth);
            newUnits.Add(lowHealth);
            warriors.Remove(lowHealth);
        }
        
        newUnits.AddRange(dead);

        if (lancers.Count > 0)
        {
            newUnits.Add(lancers.First());
            lancers.Remove(lancers.First());
            newUnits.AddRange(healers);
            newUnits.AddRange(lancers);
            newUnits.AddRange(warriors);
        }
        else if (warriors.Count > 0)
        {
            newUnits.Add(warriors.First());
            warriors.Remove(warriors.First());
            newUnits.AddRange(healers);
            newUnits.AddRange(warriors);
        }
        else
        {
            newUnits.AddRange(healers);
        }
        
        newUnits.AddRange(gunners);
        newUnits.Add(warlord);
        return newUnits;
    }
}