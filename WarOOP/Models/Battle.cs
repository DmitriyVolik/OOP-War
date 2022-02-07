using System.Diagnostics;

namespace WarOOP.Models;

public static class Battle
{
    public static bool Fight(Warrior warrior1, Warrior warrior2)
    {
        while (true)
        {
            warrior1.AttackTo(warrior2);
            if (!warrior2.IsAlive)
            {
                return true;
            }

            warrior2.AttackTo(warrior1);
            if (!warrior1.IsAlive)
            {
                return false;
            }
        }
    }

    public static bool Fight(Army army1, Army army2)
    {
        if (army1 == null || army2 == null)
        {
            throw new NullReferenceException("Army can not be null");
        }
        
        if (!army1.HasUnits() && !army2.HasUnits())
        {
            throw new Exception("Armies can not be empty");
        }

        if (!army1.HasUnits())
        {
            return false;
        }
        
        if (!army2.HasUnits())
        {
            return true;
        }
        
        while (true)
        {
            army1.GetUnit().AttackTo(army2.GetUnit());
            if (!army2.HasUnits())
            {
                return true;
            }

            army2.GetUnit().AttackTo(army1.GetUnit());
            if (!army1.HasUnits())
            {
                return false;
            }
        }
    }
}
