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

    protected internal override int GetDamageFrom(Warrior enemy)
    {
        if (enemy.Attack > Defense)
        {
            var damage = enemy.Attack - Defense;
            CurrentHealth -= damage;

            return enemy.CurrentHealth < 0 ? damage + enemy.CurrentHealth : damage;
        }

        return 0;
    }
}