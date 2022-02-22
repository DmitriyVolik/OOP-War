
using System.Threading.Channels;
using WarOOP.Models;

namespace WarOOP;

public static class Program
{
    public static void Main()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warlord>(1);
        army1.AddUnits<Warrior>(2);
        army1.AddUnits<Lancer>(2);
        army1.AddUnits<Healer>(2);
        army2.AddUnits<Gunner>(1);
        army2.AddUnits<Warlord>(1);
        army2.AddUnits<Vampire>(1);
        army2.AddUnits<Healer>(2);
        army2.AddUnits<Knight>(2);
        
        Console.WriteLine(Battle.Fight(army1, army2));

    }
}