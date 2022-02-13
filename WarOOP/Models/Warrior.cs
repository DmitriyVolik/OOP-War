namespace WarOOP.Models;

public class Warrior
{
    public bool IsAlive => CurrentHealth > 0;

    public int StartHealth { get; protected set; }

    public int CurrentHealth { get; protected set; }

    public int Attack { get; protected set; }

    public Warrior UnitBehind { get; private set; }

    public Warrior()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 5;
    }

    protected internal virtual int GetDamageFrom(Hit hit)
    {
        if (IsAlive)
        {
            CurrentHealth -= hit.Damage;
            return hit.Enemy.CurrentHealth < 0 ? hit.Enemy.Attack + hit.Enemy.CurrentHealth : hit.Enemy.Attack;
        }

        return 0;
    }

    public virtual void AttackTo(Warrior enemy)
    {
        if (IsAlive)
        {
            enemy.GetDamageFrom(new Hit(Attack,this));
        }
    }

    public static Warrior CreateWarrior<T>() where T : Warrior, new()
    {
        return new T();
    }

    public void SetUnitBehind(Warrior unit)
    {
        if (UnitBehind == null)
        {
            UnitBehind = unit;
        }
    }
}