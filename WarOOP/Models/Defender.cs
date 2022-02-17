namespace WarOOP.Models;

public class Defender : Warrior
{
    private int _defense;

    public int Defense
    {
        get
        {
            if (_defense + Equipment.Defense < 0)
            {
                return 0;
            }

            return _defense + Equipment.Defense;
        }
        
        private set => _defense = value;
    }

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