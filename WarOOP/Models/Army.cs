using System.Collections;
using System.Threading.Channels;
using WarOOP.Abstractions;

namespace WarOOP.Models;

public class Army : IArmy
{
    private List<Warrior> Units;

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
        Warrior warrior;
        
        switch (type)
        {
            case "Warrior":
                warrior = new Warrior();
                break;
            case "Knight":
                warrior = new Knight();
                break;
            case "Defender":
                warrior = new Defender();
                break;
            default:
                throw new Exception("Type not found");
        }

        for (int i = 0; i < count; i++)
        {
            Units.Add(warrior);
        }
    }

    public Warrior GetUnit()
    {
        return Units[0];
    }
}