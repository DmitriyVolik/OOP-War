using System.Collections;
using System.Threading.Channels;
using WarOOP.Abstractions;

namespace WarOOP.Models;

public class Army : IArmy
{
    private readonly List<Warrior> _units;

    public bool HasUnits => _units.Any(x => x.IsAlive);
    
    private int _currentUnit;

    public Army()
    {
        _currentUnit=0;
        _units = new List<Warrior>();
    }

    public void PrepareUnitsForBattle(bool isStraightFight = false)
    {
        if (!isStraightFight)
        {
            SetUnitsBehind();
        }
        
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
            _units.Add(unit);
        }
    }

    private void SetUnitsBehind()
    {
        if (_units.All(x => x.UnitBehind == null))
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

    public void ResetCurrentUnit()
    {
        _currentUnit = 0;
    }
}