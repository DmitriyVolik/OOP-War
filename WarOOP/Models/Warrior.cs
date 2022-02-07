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

    protected virtual void GetDamageFrom(Warrior enemy)
    {
        if (IsAlive) CurrentHealth -= enemy.Attack;
    }

    public void AttackTo(Warrior enemy)
    {
        if (IsAlive) enemy.GetDamageFrom(this);
    }

    public static Warrior CreateWarrior(string type)
    {
        switch (type)
        {
            case "Warrior":
                return new Warrior();
            case "Knight":
                return new Knight();
            case "Defender":
                return new Defender();
            default:
                throw new Exception("Type not found");
        }
    }
}