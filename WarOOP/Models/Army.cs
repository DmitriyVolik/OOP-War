using System.Collections;
using System.Threading.Channels;
using WarOOP.Abstractions;

namespace WarOOP.Models;

public class Army : IArmy
{
    private List<Warrior> Units;

    public Army()
    {
        Units = new List<Warrior>();
    }
    
    public void AddUnits(Type type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Units.Add(Warrior.CreateWarrior(type));
        }
    }
    
    public bool HasUnits()
    {
        if (Units.Count > 0 && !Units[0].IsAlive)
        {
            Units.Remove(Units[0]);
        }
        return Units.Count > 0;
    }

    public Warrior GetUnit()
    {
        if (HasUnits())
        {
            return Units[0];
        }
        return null!;
    }
}