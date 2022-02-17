namespace WarOOP.Models;

public class Warrior
{
    private int _attack;
    
    private int _startHealth;
    
    private int _currentHealth;
    public bool IsAlive => CurrentHealth > 0;

    public int StartHealth
    {
        get => _startHealth + Equipment.Health; 
        protected set => _startHealth = value;
    }

    public int CurrentHealth
    {
        get => _currentHealth + Equipment.Health; 
        protected internal set => _currentHealth  = value - Equipment.Health;
    }

    public int Attack
    {
        get => _attack + Equipment.Attack; 
        protected set => _attack = value;
    }
    
    public Equipment Equipment { get; protected set; }

    public Warrior UnitBehind { get; private set; }

    public Warrior()
    {
        Equipment = new Equipment();
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
            Console.WriteLine("-----------");
            Console.WriteLine(hit.Damage);
            Console.WriteLine(CurrentHealth);
            CurrentHealth -= hit.Damage;
            Console.WriteLine(CurrentHealth);
            Console.WriteLine("-----------");
            
            return hit.Enemy.Attack;
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