using System.Collections;
using System.Threading.Channels;
using WarOOP.Abstractions;

namespace WarOOP.Models;

public class Army : IArmy
{
    private List<Warrior> _units;

    public bool HasUnits => _units.Any(x => x.IsAlive);
    
    public bool HasWarlord => _units.Any(x => x.IsAlive && x is Warlord);
    
    private int _currentUnit;

    public Army()
    {
        _currentUnit=0;
        _units = new List<Warrior>();
    }

    public void PrepareForFight(bool isStraightFight = false)
    {
        if (!isStraightFight)
        {
            SetUnitsBehind();
        }

        PrepareForStraightFight();
    }

    public void PrepareForStraightFight()
    {
        foreach (var unit in _units)
        {
            unit.PrepareForBattle();
        }
    }

    public void AddUnits<T>(int count) where T : Warrior, new()
    {
        for (int i = 0; i < count; i++)
        {
            var unit = Warrior.CreateWarrior<T>();
            if (unit is Warlord && HasWarlord)
            {
                continue;
            }
            _units.Add(unit);
        }
    }

    private void SetUnitsBehind()
    {
        if (_units.All(x => x.UnitBehind == null) || HasWarlord)
        {
            for (int i = 0; i < _units.Count - 1; i++)
            {
                _units[i].SetUnitBehind(_units[i+1]);
            }
        }
    }

    public void SetNextUnit()
    {
        _currentUnit++;
    }

    public Warrior GetUnit()
    {
        if (_currentUnit< _units.Count)
        {
            return _units[_currentUnit];
        }
        return null!;
    }

    public IEnumerable<Warrior> AllAlive()
    {
        return _units.Where(x => x.IsAlive);
    }

    public void MoveUnits()
    {
        if (HasWarlord)
        {
            var newArmy = new List<Warrior>();

            var dead = _units.Where(x => !x.IsAlive);
            var lancers = _units.Where(x => x.IsAlive && x is Lancer).ToList();
            var warriors = _units.Where(x => x.IsAlive && x.Attack > 0 && x is not Warlord && x is not Lancer).ToList();
            var healers = _units.Where(x => x.IsAlive && x is Healer).ToList();
            var warlord = _units.First(x => x is Warlord);

            newArmy.AddRange(dead);

            if (lancers.Count > 0)
            {
                newArmy.Add(lancers.First());
                lancers.Remove(lancers.First());
                newArmy.AddRange(healers);
                newArmy.AddRange(lancers);
                newArmy.AddRange(warriors);
            }
            else if (warriors.Count > 0)
            {
                newArmy.Add(warriors.First());
                warriors.Remove(warriors.First());
                newArmy.AddRange(healers);
                newArmy.AddRange(warriors);
            }
            else
            {
                newArmy.AddRange(healers);
            }
            
            newArmy.Add(warlord);
            _units = newArmy;
        }
    }
}