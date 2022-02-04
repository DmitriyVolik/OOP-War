namespace OOP_War.Models;

public class Warrior
{
    public bool IsAlive => CurrentHealth > 0;
    
    public int StartHealth { get; protected init; }

    public int CurrentHealth { get; protected set; }

    public int Attack { get; protected init; }

    public Warrior()
    {
        StartHealth = CurrentHealth;
        CurrentHealth = 50;
        Attack = 5;
    }

    private void GetDamageFrom(Warrior enemy) => CurrentHealth -= enemy.Attack;

    public void AttackTo(Warrior enemy) => enemy.GetDamageFrom(this);
}
