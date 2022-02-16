namespace WarOOP.Models;

public class Warrior
{
    public bool IsAlive => CurrentHealth > 0;

    public int StartHealth { get; protected set; }

    public int CurrentHealth { get; protected internal set; }

    public int Attack { get; protected set; }

    public Warrior UnitBehind { get; private set; }

    public Warrior()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 5;
    }

    public virtual void PrepareForBattle()
    {
        
    }

    protected internal virtual int GetDamageFrom(Hit hit)
    {
        if (IsAlive)
        {
            CurrentHealth -= hit.Damage;
            return CurrentHealth < 0 ? hit.Enemy.Attack + CurrentHealth : hit.Enemy.Attack;
        }

        return 0;
    }

    protected internal virtual void Action(Warrior warrior)
    {
        UnitBehind?.Action(this);
    }

    public virtual void AttackTo(Warrior enemy)
    {
        if (IsAlive)
        {
            enemy.GetDamageFrom(new Hit(Attack,this));
            Action(this);
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