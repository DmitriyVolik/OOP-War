namespace OOP_War.Models;

public static class Game
{
    public static bool Fight(Warrior knight, Warrior warrior)
    {
        while (true)
        {
            knight.AttackTo(warrior);
            if (!knight.IsAlive)
            {
                return false;
            }

            warrior.AttackTo(knight);
            if (!warrior.IsAlive)
            {
                return true;
            }
        }
    }
}
