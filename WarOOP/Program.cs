using WarOOP.Models;

namespace WarOOP;

public static class Program
{
    public static void Main()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(7);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Defender>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Healer>(1);
        army2.AddUnits<Vampire>(6);
        army2.AddUnits<Lancer>(4);
        
        var result = Battle.StraightFight(army1, army2);

        foreach (var item in army1.AllAlive())
        {
            Console.WriteLine(item.GetType().Name + "-" + item.CurrentHealth);
        }

    }
}