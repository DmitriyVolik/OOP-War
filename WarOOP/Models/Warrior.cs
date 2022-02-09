namespace WarOOP.Models;

public class Warrior
{
    public bool IsAlive => CurrentHealth > 0;

    public int StartHealth { get; protected set; }

    public int CurrentHealth { get; protected set; }

    public int Attack { get; protected set; }

    public Warrior()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 5;
    }

    protected internal virtual int GetDamageFrom(Warrior enemy)
    {
        if (IsAlive)
        {
            CurrentHealth -= enemy.Attack;
            
            return enemy.CurrentHealth < 0 ? enemy.Attack + enemy.CurrentHealth : enemy.Attack;
        }

        return 0;
    }

    public virtual void AttackTo(Warrior enemy)
    {
        if (IsAlive)
        {
            enemy.GetDamageFrom(this);
        }
    }

    public static Warrior CreateWarrior<T>() where T : Warrior, new()
    {
        return new T();
    }
}