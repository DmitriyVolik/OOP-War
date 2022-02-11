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

    private static bool FightArmyWarriors(Army army1, Army army2)
    {
        while (true)
        {
            army1.GetUnit().AttackTo(army2);
            if (!army2.GetUnit().IsAlive)
            {
                return true;
            }

            army2.GetUnit().AttackTo(army1);
            if (!army1.GetUnit().IsAlive)
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
        
        if (!army1.HasUnits && !army2.HasUnits)
        {
            throw new Exception("Armies can not be empty");
        }

        if (!army1.HasUnits)
        {
            return false;
        }
        
        if (!army2.HasUnits)
        {
            return true;
        }
        
        while (true)
        {
            var fightResult=FightArmyWarriors(army1, army2);

            if (fightResult)
            {
                army2.SetNextUnit();
                if (!army2.HasUnits)
                {
                    return true;
                }
            }
            else
            {
                army1.SetNextUnit();
                if (!army1.HasUnits)
                {
                    return false;
                }
            }
            

            /*if (army1.GetUnit().GetType() == typeof(Lancer))
            {
                var lancer = (Lancer)army1.GetUnit();
                lancer.AttackTo(army2.GetUnit(), army2.GetNextUnit());
            }
            else
            {
                army1.GetUnit().AttackTo(army2.GetUnit());
            }
            
            if (!army2.GetUnit().IsAlive)
            {
                army2.SetNextUnit();
            }
            if (!army2.HasUnits)
            {
                return true;
            }

            if (army2.GetUnit().GetType() == typeof(Lancer))
            {
                var lancer = (Lancer)army2.GetUnit();
                lancer.AttackTo(army1.GetUnit(), army1.GetNextUnit());
            }
            else
            {
                army2.GetUnit().AttackTo(army1.GetUnit());
            }

            if (!army1.GetUnit().IsAlive)
            {
                army1.SetNextUnit();
            }
            if (!army1.HasUnits)
            {
                return false;
            }*/
        }
    }
}
