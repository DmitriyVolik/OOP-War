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

    protected internal override int GetDamageFrom(Hit hit)
    {
        if (hit.Damage > Defense)
        {
            var damage = hit.Damage - Defense;
            CurrentHealth -= damage;

            return hit.Enemy.CurrentHealth < 0 ? damage + hit.Enemy.CurrentHealth : damage;
        }

        return 0;
    }
}