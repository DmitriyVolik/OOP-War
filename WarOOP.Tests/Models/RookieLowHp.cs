using WarOOP.Models;

namespace WarOOP.Tests.Models;

public class RookieLowHp : Warrior
{
    public RookieLowHp()
    {
        CurrentHealth = 2;
        StartHealth = CurrentHealth;
        Attack = 1;
    }
}