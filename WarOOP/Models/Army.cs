using System.Collections;
using System.Threading.Channels;
using WarOOP.Abstractions;

namespace WarOOP.Models;

public class Army : IArmy
{
    public List<Warrior> Units;

    public bool HasUnits
    {
        get
        {
            if (!Units[0].IsAlive)
            {
                Units.Remove(Units[0]);
            }
            return Units.Count > 0;
        }
    }

    public Army()
    {
        Units = new List<Warrior>();
    }
    
    public void AddUnits(string type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Units.Add(Warrior.CreateWarrior(type));
        }
    }

    public Warrior GetUnit() => Units[0];

}