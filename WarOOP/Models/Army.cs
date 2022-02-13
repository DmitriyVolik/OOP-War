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
            var unit = Warrior.CreateWarrior<T>();
            _units.Add(unit);
            if (_units.Count > 1)
            {
                _units[^2].SetUnitBehind(unit);
            }
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