namespace OOP_War.Models;

public class Knight : Warrior
{
    public Knight()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 7;
    }
}