
using WarOOP.Models;

namespace WarOOP;

public static class Program
{
    public static void Main()
    {
        var gunner = new Gunner();
        var warrior = new Warrior();
        
        gunner.AttackTo(warrior);
        var result = gunner.CurrentHealth == 1 && warrior.CurrentHealth == 30;

        Console.WriteLine(gunner.CurrentHealth);
        Console.WriteLine(warrior.CurrentHealth);
    }
}