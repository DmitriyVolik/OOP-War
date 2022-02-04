namespace WarOOP.Models;

public static class Game
{
    public static bool Fight(Warrior warrior1, Warrior warrior2)
    {
        while (true)
        {
            warrior1.AttackTo(warrior2);
            if (!warrior1.IsAlive)
            {
                return false;
            }

            warrior2.AttackTo(warrior1);
            if (!warrior2.IsAlive)
            {
                return true;
            }
        }
    }
}
