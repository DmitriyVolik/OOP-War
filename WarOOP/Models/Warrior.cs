namespace WarOOP.Models;

public class Warrior
{
    public bool IsAlive => CurrentHealth > 0;

    public int StartHealth { get; protected set;}

    public int CurrentHealth { get; protected set; }

    public int Attack { get; protected set; }

    public Warrior()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 5;
    }

    protected virtual void GetDamageFrom(Warrior enemy) => CurrentHealth -= enemy.Attack;

    public void AttackTo(Warrior enemy) => enemy.GetDamageFrom(this);
}
