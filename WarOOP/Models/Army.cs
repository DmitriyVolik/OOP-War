using System.Collections;
using System.Threading.Channels;
using WarOOP.Abstractions;

namespace WarOOP.Models;

public class Army : IArmy
{
    private readonly List<Warrior> _units;

    public bool HasUnits => _units.Count > _currentUnit ;
    
    private int _currentUnit;

    public Army()
    {
        _currentUnit=0;
        _units = new List<Warrior>();
    }
    
    public void AddUnits<T>(int count) where T : Warrior, new()
    {
        for (int i = 0; i < count; i++)
        {
            _units.Add(Warrior.CreateWarrior<T>());
        }
    }

    public void SetUnitsBehind()
    {
        for (int i = 0; i < _units.Count-1; i++)
        {
            _units[i].SetUnitBehind(_units[i+1]);
        }
    }

    public void SetNextUnit()
    {
        if (HasUnits)
        {
            _currentUnit++;
        }
    }

    public Warrior GetUnit()
    {
        if (HasUnits)
        {
            return _units[_currentUnit];
        }
        return null!;
    }
}