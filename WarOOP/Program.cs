using WarOOP.Models;

namespace WarOOP;

public static class Program
{
    public static void Main()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(1);
        army2.AddUnits<Defender>(2);
        army2.SetUnitsBehind();

        var lancer = army1.GetUnit();
        lancer.AttackTo(army2.GetUnit());
        var defender = (Defender)army2.GetUnit();
        var defenderBehind = (Defender)army2.GetUnit().UnitBehind;
        var defenderExpectedHealth = defender.StartHealth - (lancer.Attack - defender.Defense);
        var defenderBehindExpectedHealth = defenderBehind.StartHealth - (lancer.Attack / 2 - defender.Defense);
        Console.WriteLine(defenderExpectedHealth);
        Console.WriteLine(defenderBehindExpectedHealth);
        
    }
}