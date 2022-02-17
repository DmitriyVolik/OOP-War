namespace WarOOP.Models;

public class Warlord : Defender
{
    public Warlord()
    {
        CurrentHealth = 100;
        StartHealth = CurrentHealth;
        Attack = 4;
        Defense = 2;
    }
}