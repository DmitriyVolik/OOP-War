
using WarOOP.Models;

namespace WarOOP;

public static class Program
{
    public static void Main()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Gunner>(1);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Vampire>(4);
        army2.AddUnits<Warlord>(1);
        army1.PrepareForFight();
        army2.PrepareForFight();
        army2.MoveUnits(army1);
        
    }
}