namespace WarOOP.Models;

public class Defender : Warrior
{
    public int Defense { get; private set; }

    public Defender()
    {
        CurrentHealth = 60;
        StartHealth = CurrentHealth;
        Attack = 3;
        Defense = 2;
    }
    
    protected override void GetDamageFrom(Warrior enemy)
    {
        if (enemy.Attack>Defense)
        {
            CurrentHealth -= enemy.Attack - Defense;
        }
    }
}